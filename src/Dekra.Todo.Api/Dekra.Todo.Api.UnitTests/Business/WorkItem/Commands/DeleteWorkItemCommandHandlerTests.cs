using Dekra.Todo.Api.Business.WorkItem.Commands.DeleteWorkItem;
using Dekra.Todo.Api.Data.Contracts.EntityFramework;
using Dekra.Todo.Api.Infrastructure.Config.ApiResponse.Object;
using Dekra.Todo.Api.Infrastructure.Utilities.Extensions;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System.Linq.Expressions;

namespace Dekra.Todo.Api.UnitTests.Business.WorkItem.Commands
{
    public class DeleteWorkItemCommandHandlerTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;

        public DeleteWorkItemCommandHandlerTests() => _unitOfWorkMock = new Mock<IUnitOfWork>();

        [Fact]
        public async Task Handle_DeletesWorkItem_ForValidRequest()
        {
            // Arrange
            var userId = "958a6f1a-5cef-4344-a23b-d7ae63e2834a";
            var workItemId = Guid.NewGuid();
            var workItem = new Data.Entities.WorkItem
            {
                Id = workItemId,
                UserId = userId,
                Content = "Random content",
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
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

            var handler = new DeleteWorkItemCommandHandler(_unitOfWorkMock.Object);

            // Act
            var request = new DeleteWorkItemCommand(UserId: userId, WorkItemId: workItemId);
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.Value?.Success);
            Assert.Null(result.Value?.ErrorCode);
            Assert.Null(result.Value?.ErrorMessage);
            Assert.Equal(HttpCodeEnum.OK, result.HttpCode);
            Assert.Equal(workItemId, ((Data.Entities.WorkItem)result.Value!.Result!).Id);
            Assert.True(((Data.Entities.WorkItem)result.Value!.Result!).IsDeleted);
        }

        [Fact]
        public async Task Handle_ReturnsBadRequest_ForEmptyUserIdOrWorkItemId()
        {
            // Arrange
            var emptyUserIdRequest = new DeleteWorkItemCommand(WorkItemId: Guid.NewGuid(), UserId: string.Empty);
            var emptyWorkItemRequest = new DeleteWorkItemCommand(WorkItemId: Guid.Empty, UserId: Guid.NewGuid().ToString());

            var handler = new DeleteWorkItemCommandHandler(Mock.Of<IUnitOfWork>());

            // Act
            var emptyUserIdResult = await handler.Handle(emptyUserIdRequest, CancellationToken.None);
            var emptyWorkItemResult = await handler.Handle(emptyWorkItemRequest, CancellationToken.None);

            // Assert
            Assert.Null(emptyUserIdResult.Value!.Result);
            Assert.False(emptyUserIdResult.Value?.Success);
            Assert.Equal(HttpCodeEnum.BadRequest, emptyUserIdResult.HttpCode);
            Assert.Equal((int)ErrorCodeEnum.BAD_REQUEST, emptyUserIdResult.Value!.ErrorCode);
            Assert.Equal(ErrorCodeEnum.BAD_REQUEST.GetDescription(), emptyUserIdResult.Value!.ErrorMessage);

            Assert.Null(emptyWorkItemResult.Value!.Result);
            Assert.False(emptyWorkItemResult.Value?.Success);
            Assert.Equal(HttpCodeEnum.BadRequest, emptyWorkItemResult.HttpCode);
            Assert.Equal((int)ErrorCodeEnum.BAD_REQUEST, emptyWorkItemResult.Value!.ErrorCode);
            Assert.Equal(ErrorCodeEnum.BAD_REQUEST.GetDescription(), emptyWorkItemResult.Value!.ErrorMessage);
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

            var handler = new DeleteWorkItemCommandHandler(_unitOfWorkMock.Object);

            // Act
            var request = new DeleteWorkItemCommand(UserId: userId, WorkItemId: workItemId);
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Null(result.Value!.Result);
            Assert.False(result.Value?.Success);
            Assert.Equal(HttpCodeEnum.Notfound, result.HttpCode);
            Assert.Equal((int)ErrorCodeEnum.NOT_EXIST_WORK_ITEM_ID, result.Value!.ErrorCode);
            Assert.Equal(ErrorCodeEnum.NOT_EXIST_WORK_ITEM_ID.GetDescription(), result.Value!.ErrorMessage);
        }
    }
}
