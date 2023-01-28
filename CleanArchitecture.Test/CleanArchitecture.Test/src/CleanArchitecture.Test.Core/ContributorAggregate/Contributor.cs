using Ardalis.GuardClauses;
using CleanArchitecture.Test.SharedKernel;
using CleanArchitecture.Test.SharedKernel.Interfaces;

namespace CleanArchitecture.Test.Core.ContributorAggregate;
public class Contributor : EntityBase, IAggregateRoot
{
  public string Name { get; private set; }

  public Contributor(string name)
  {
    Name = Guard.Against.NullOrEmpty(name, nameof(name));
  }

  public void UpdateName(string newName)
  {
    Name = Guard.Against.NullOrEmpty(newName, nameof(newName));
  }
}
