using Dekra.Todo.Api.Business.WorkItem.Queries.GetWorkItemsByUser;
using Dekra.Todo.Api.Business.WorkItem.ViewModels;
using Dekra.Todo.Api.Data.Contracts.EntityFramework;
using Dekra.Todo.Api.Infrastructure.Config.ApiResponse.Object;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System.Linq.Expressions;

namespace Dekra.Todo.Api.UnitTests.Business.WorkItem.Queries
{
    public class GetWorkItemsByUserQueryHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public GetWorkItemsByUserQueryHandlerTests() => _unitOfWorkMock = new Mock<IUnitOfWork>();

        [Fact]
        public async Task Handle_ReturnsWorkItems_ForValidRequest()
        {
            // Arrange
            var userId = "958a6f1a-5cef-4344-a23b-d7ae63e2834a";
            var workItems = new List<Data.Entities.WorkItem>()
            {
                new() { Id = Guid.NewGuid(), UserId = userId, Content = "Test Item 1", CreatedAt = DateTime.UtcNow },
                new() { Id = Guid.NewGuid(), UserId = userId, Content = "Test Item 2", CreatedAt = DateTime.UtcNow.AddHours(-1) },
            };

            _unitOfWorkMock.Setup(m => m.GetRepository<Data.Entities.WorkItem>().GetListAsync(
                It.IsAny<Expression<Func<Data.Entities.WorkItem, WorkItemViewModel>>>(),
                It.IsAny<Expression<Func<Data.Entities.WorkItem, bool>>>(),
                It.IsAny<Func<IQueryable<Data.Entities.WorkItem>, IOrderedQueryable<Data.Entities.WorkItem>>>(),
                It.IsAny<Func<IQueryable<Data.Entities.WorkItem>, IIncludableQueryable<Data.Entities.WorkItem, object>>>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(workItems.Select(wi => new WorkItemViewModel
                {
                    Id = wi.Id,
                    Content = wi.Content,
                    CreatedAt = wi.CreatedAt,
                    IsCompleted = wi.IsCompleted
                }).ToList());

            var handler = new GetWorkItemsByUserQueryHandler(_unitOfWorkMock.Object);

            // Act
            var request = new GetWorkItemsByUserQuery(UserId: userId);
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.Value?.Success);
            Assert.Null(result.Value?.ErrorCode);
            Assert.Null(result.Value?.ErrorMessage);
            Assert.Equal(HttpCodeEnum.OK, result.HttpCode);
            Assert.Equal(workItems.Count, ((IList<WorkItemViewModel>)result.Value?.Result!).Count);
            Assert.Equal(workItems.OrderByDescending(w => w.CreatedAt).Select(w => w.Id), ((IList<WorkItemViewModel>)result.Value?.Result!).Select(w => w.Id));
        }
    }
}
