using Ardalis.Result;
using CleanArchitecture.Test.Core.ProjectAggregate;

namespace CleanArchitecture.Test.Core.Interfaces;
public interface IToDoItemSearchService
{
  Task<Result<ToDoItem>> GetNextIncompleteItemAsync(int projectId);
  Task<Result<List<ToDoItem>>> GetAllIncompleteItemsAsync(int projectId, string searchString);
}
