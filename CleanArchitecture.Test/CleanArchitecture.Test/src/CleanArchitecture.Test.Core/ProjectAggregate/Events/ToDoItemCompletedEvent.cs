using CleanArchitecture.Test.SharedKernel;

namespace CleanArchitecture.Test.Core.ProjectAggregate.Events;
public class ToDoItemCompletedEvent : DomainEventBase
{
  public ToDoItem CompletedItem { get; set; }

  public ToDoItemCompletedEvent(ToDoItem completedItem)
  {
    CompletedItem = completedItem;
  }
}
