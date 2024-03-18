using Dekra.Todo.Api.Business.WorkItem.Commands.UpdateWorkItem;
using Dekra.Todo.Api.Data.Contracts.EntityFramework;
using Dekra.Todo.Api.Infrastructure.Config.ApiResponse.Object;
using Dekra.Todo.Api.Infrastructure.Utilities.Extensions;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System.Linq.Expressions;

namespace Dekra.Todo.Api.UnitTests.Business.WorkItem.Commands
{
    public class UpdateWorkItemCommandHandlerTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;

        public UpdateWorkItemCommandHandlerTests() => _unitOfWorkMock = new Mock<IUnitOfWork>();

        [Fact]
        public async Task Handle_UpdatesWorkItem_ForValidRequest()
        {
            // Arrange
            var userId = "958a6f1a-5cef-4344-a23b-d7ae63e2834a";
            var workItemId = Guid.NewGuid();
            var content = "Updated Content";
            var isCompleted = true;
            var workItem = new Data.Entities.WorkItem
            {
                Id = workItemId,
                UserId = userId,
                Content = "Original Content",
                IsCompleted = false
            };

            _unitOfWorkMock.Setup(m => m.GetRepository<Data.Entities.WorkItem>().FirstOrDefaultAsync(
                It.IsAny<Expression<Func<Data.Entities.WorkItem, bool>>>(),
                It.IsAny<Func<IQueryable<Data.Entities.WorkItem>, IOrderedQueryable<Data.Entities.WorkItem>>>(),
                It.IsAny<Func<IQueryable<Data.Entities.WorkItem>, IIncludableQueryable<Data.Entities.WorkItem, object>>>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(workItem);

            _unitOfWorkMock.Setup(m => m.CommitAsync()).ReturnsAsync(1);

            var handler = new UpdateWorkItemCommandHandler(_unitOfWorkMock.Object);

            // Act
            var request = new UpdateWorkItemCommand
            (
                UserId: userId,
                WorkItemId: workItemId,
                WorkItem: new UpdateWorkItemRequestModel
                {
                    Content = content,
                    IsCompleted = isCompleted
                }
            );
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.Value?.Success);
            Assert.Null(result.Value?.ErrorCode);
            Assert.Null(result.Value?.ErrorMessage);
            Assert.Equal(HttpCodeEnum.OK, result.HttpCode);
            Assert.Equal(workItemId, ((Data.Entities.WorkItem)result.Value!.Result!).Id);
            Assert.Equal(content, ((Data.Entities.WorkItem)result.Value!.Result!).Content);
            Assert.Equal(isCompleted, ((Data.Entities.WorkItem)result.Value!.Result!).IsCompleted);
        }

        [Fact]
        public async Task Handle_ReturnsBadRequest_ForEmptyUserIdWorkItemIdOrContent()
        {
            // Arrange
            var request1 = new UpdateWorkItemCommand
            (
                UserId: string.Empty,
                WorkItemId: Guid.NewGuid(),
                WorkItem: new UpdateWorkItemRequestModel()
            );
            var request2 = new UpdateWorkItemCommand
            (
                UserId: Guid.NewGuid().ToString(),
                WorkItemId: Guid.Empty,
                WorkItem: new UpdateWorkItemRequestModel()
            );
            var request3 = new UpdateWorkItemCommand
            (
                UserId: Guid.NewGuid().ToString(),
                WorkItemId: Guid.NewGuid(),
                WorkItem: new UpdateWorkItemRequestModel
                {
                    Content = string.Empty,
                    IsCompleted = true
                }
            );

            var handler = new UpdateWorkItemCommandHandler(Mock.Of<IUnitOfWork>());

            // Act
            var result1 = await handler.Handle(request1, CancellationToken.None);
            var result2 = await handler.Handle(request2, CancellationToken.None);
            var result3 = await handler.Handle(request3, CancellationToken.None);

            // Assert
            Assert.Null(result1.Value!.Result);
            Assert.Null(result2.Value!.Result);
            Assert.Null(result3.Value!.Result);

            Assert.False(result1.Value?.Success);
            Assert.False(result2.Value?.Success);
            Assert.False(result3.Value?.Success);

            Assert.Equal(HttpCodeEnum.BadRequest, result1.HttpCode);
            Assert.Equal(HttpCodeEnum.BadRequest, result2.HttpCode);
            Assert.Equal(HttpCodeEnum.BadRequest, result3.HttpCode);

            Assert.Equal((int)ErrorCodeEnum.BAD_REQUEST, result1.Value!.ErrorCode);
            Assert.Equal((int)ErrorCodeEnum.BAD_REQUEST, result2.Value!.ErrorCode);
            Assert.Equal((int)ErrorCodeEnum.BAD_REQUEST, result3.Value!.ErrorCode);

            Assert.Equal(ErrorCodeEnum.BAD_REQUEST.GetDescription(), result1.Value!.ErrorMessage);
            Assert.Equal(ErrorCodeEnum.BAD_REQUEST.GetDescription(), result2.Value!.ErrorMessage);
            Assert.Equal(ErrorCodeEnum.BAD_REQUEST.GetDescription(), result3.Value!.ErrorMessage);
        }

        [Fact]
        public async Task Handle_ReturnsNotFound_ForNonExistentWorkItem()
        {
            // Arrange
            var userId = "958a6f1a-5cef-4344-a23b-d7ae63e2834a";
            var workItemId = Guid.NewGuid();
            _unitOfWorkMock.Setup(m => m.GetRepository<Data.Entities.WorkItem>().FirstOrDefaultAsync(
                It.IsAny<Expression<Func<Data.Entities.WorkItem, bool>>>(),
                It.IsAny<Func<IQueryable<Data.Entities.WorkItem>, IOrderedQueryable<Data.Entities.WorkItem>>>(),
                It.IsAny<Func<IQueryable<Data.Entities.WorkItem>, IIncludableQueryable<Data.Entities.WorkItem, object>>>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<Data.Entities.WorkItem>(null));

            var handler = new UpdateWorkItemCommandHandler(_unitOfWorkMock.Object);

            // Act
            var request = new UpdateWorkItemCommand
            (
                UserId: userId,
                WorkItemId: workItemId,
                WorkItem: new UpdateWorkItemRequestModel { Content = "Updated Content" }
            );
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Null(result.Value!.Result);
            Assert.False(result.Value?.Success);
            Assert.Equal(HttpCodeEnum.Notfound, result.HttpCode);
            Assert.Equal((int)ErrorCodeEnum.NOT_EXIST_WORK_ITEM_ID, result.Value!.ErrorCode);
            Assert.Equal(ErrorCodeEnum.NOT_EXIST_WORK_ITEM_ID.GetDescription(), result.Value!.ErrorMessage);
        }

        [Fact]
        public async Task Handle_ReturnsSuccessful_ForUpdateWithNoChanges()
        {
            // Arrange
            var userId = "958a6f1a-5cef-4344-a23b-d7ae63e2834a";
            var workItemId = Guid.NewGuid();
            var content = "Original Content";
            var isCompleted = false;
            var workItem = new Data.Entities.WorkItem
            {
                Id = workItemId,
                UserId = userId,
                Content = content,
                IsCompleted = isCompleted
            };

            _unitOfWorkMock.Setup(m => m.GetRepository<Data.Entities.WorkItem>().FirstOrDefaultAsync(
                It.IsAny<Expression<Func<Data.Entities.WorkItem, bool>>>(),
                It.IsAny<Func<IQueryable<Data.Entities.WorkItem>, IOrderedQueryable<Data.Entities.WorkItem>>>(),
                It.IsAny<Func<IQueryable<Data.Entities.WorkItem>, IIncludableQueryable<Data.Entities.WorkItem, object>>>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(workItem);

            _unitOfWorkMock.Setup(m => m.CommitAsync()).ReturnsAsync(1);

            var handler = new UpdateWorkItemCommandHandler(_unitOfWorkMock.Object);

            // Act
            var request = new UpdateWorkItemCommand
            (
                UserId: userId,
                WorkItemId: workItemId,
                WorkItem: new UpdateWorkItemRequestModel { Content = content, IsCompleted = isCompleted }
            );
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert

            Assert.True(result.Value?.Success);
            Assert.Null(result.Value?.ErrorCode);
            Assert.Null(result.Value?.ErrorMessage);
            Assert.Equal(HttpCodeEnum.OK, result.HttpCode);
            Assert.Equal(workItemId, ((Data.Entities.WorkItem)result.Value!.Result!).Id);
            Assert.Equal(content, ((Data.Entities.WorkItem)result.Value!.Result!).Content);
            Assert.Equal(isCompleted, ((Data.Entities.WorkItem)result.Value!.Result!).IsCompleted);
        }

        [Fact]
        public async Task Handle_ReturnsConflict_ForUpdatingDeletedWorkItem()
        {
            // Arrange
            var userId = "958a6f1a-5cef-4344-a23b-d7ae63e2834a";
            var workItemId = Guid.NewGuid();
            var workItem = new Data.Entities.WorkItem
            {
                Id = workItemId,
                UserId = userId,
                Content = "Original Content",
                IsCompleted = false,
                IsDeleted = true
            };

            _unitOfWorkMock.Setup(m => m.GetRepository<Data.Entities.WorkItem>().FirstOrDefaultAsync(
                It.IsAny<Expression<Func<Data.Entities.WorkItem, bool>>>(),
                It.IsAny<Func<IQueryable<Data.Entities.WorkItem>, IOrderedQueryable<Data.Entities.WorkItem>>>(),
                It.IsAny<Func<IQueryable<Data.Entities.WorkItem>, IIncludableQueryable<Data.Entities.WorkItem, object>>>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(workItem);

            var handler = new UpdateWorkItemCommandHandler(_unitOfWorkMock.Object);

            // Act
            var request = new UpdateWorkItemCommand
            (
                UserId: userId,
                WorkItemId: workItemId,
                WorkItem: new UpdateWorkItemRequestModel { Content = "Updated Content" }
            );
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Null(result.Value!.Result);
            Assert.False(result.Value?.Success);
            Assert.Equal(HttpCodeEnum.BadRequest, result.HttpCode);
            Assert.Equal((int)ErrorCodeEnum.DELETED_WORK_ITEM, result.Value!.ErrorCode);
            Assert.Equal(ErrorCodeEnum.DELETED_WORK_ITEM.GetDescription(), result.Value!.ErrorMessage);
        }
    }
}
