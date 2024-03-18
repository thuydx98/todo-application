using Dekra.Todo.Api.Business.WorkItem.Commands.CreateWorkItem;
using Dekra.Todo.Api.Data.Contracts.EntityFramework;
using Dekra.Todo.Api.Infrastructure.Config.ApiResponse.Object;
using Dekra.Todo.Api.Infrastructure.Utilities.Extensions;
using Moq;

namespace Dekra.Todo.Api.UnitTests.Business.WorkItem.Commands
{
    public class CreateWorkItemCommandHandlerTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;

        public CreateWorkItemCommandHandlerTests() => _unitOfWorkMock = new Mock<IUnitOfWork>();

        [Fact]
        public async Task Handle_CreatesWorkItem_ForValidRequest()
        {
            // Arrange
            var userId = "958a6f1a-5cef-4344-a23b-d7ae63e2834a";
            var content = "Test Work Item";
            var request = new CreateWorkItemCommand
            (
                WorkItem: new CreateWorkItemRequestModel { Content = content },
                UserId: userId
            );

            _unitOfWorkMock.Setup(m => m.CommitAsync()).ReturnsAsync(1);
            _unitOfWorkMock.Setup(m => m.GetRepository<Data.Entities.WorkItem>())
                .Returns(Mock.Of<IRepository<Data.Entities.WorkItem>>());

            var handler = new CreateWorkItemCommandHandler(_unitOfWorkMock.Object);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.Value?.Success);
            Assert.Null(result.Value?.ErrorCode);
            Assert.Null(result.Value?.ErrorMessage);
            Assert.Equal(HttpCodeEnum.OK, result.HttpCode);
            Assert.Equal(userId, ((Data.Entities.WorkItem)result.Value?.Result!).UserId);
            Assert.Equal(content, ((Data.Entities.WorkItem)result.Value?.Result!).Content);
        }

        [Fact]
        public async Task Handle_ReturnsBadRequest_ForEmptyUserId()
        {
            // Arrange
            var handler = new CreateWorkItemCommandHandler(Mock.Of<IUnitOfWork>());
            var request = new CreateWorkItemCommand(
                WorkItem: new CreateWorkItemRequestModel { Content = "Test Item" },
                UserId: string.Empty);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Null(result.Value!.Result);
            Assert.False(result.Value?.Success);
            Assert.Equal(HttpCodeEnum.BadRequest, result.HttpCode);
            Assert.Equal((int)ErrorCodeEnum.BAD_REQUEST, result.Value!.ErrorCode);
            Assert.Equal(ErrorCodeEnum.BAD_REQUEST.GetDescription(), result.Value!.ErrorMessage);
        }

        [Fact]
        public async Task Handle_ReturnsBadRequest_ForEmptyContent()
        {
            // Arrange
            var userId = "958a6f1a-5cef-4344-a23b-d7ae63e2834a";
            var handler = new CreateWorkItemCommandHandler(Mock.Of<IUnitOfWork>());
            var request = new CreateWorkItemCommand(
                WorkItem: new CreateWorkItemRequestModel { Content = string.Empty },
                UserId: userId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Null(result.Value!.Result);
            Assert.False(result.Value?.Success);
            Assert.Equal(HttpCodeEnum.BadRequest, result.HttpCode);
            Assert.Equal((int)ErrorCodeEnum.MISSING_CONTENT_WORK_ITEM, result.Value!.ErrorCode);
            Assert.Equal(ErrorCodeEnum.MISSING_CONTENT_WORK_ITEM.GetDescription(), result.Value!.ErrorMessage);
        }
    }
}
