using CleanArchitecture.Test.Core.ContributorAggregate;
using CleanArchitecture.Test.SharedKernel;

namespace CleanArchitecture.Test.Core.ProjectAggregate.Events;
public class ContributorAddedToItemEvent : DomainEventBase
{
  public int ContributorId { get; set; }
  public ToDoItem Item { get; set; }

  public ContributorAddedToItemEvent(ToDoItem item, int contributorId)
  {
    Item = item;
    ContributorId = contributorId;
  }
}
