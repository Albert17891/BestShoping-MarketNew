using CleanArchitecture.Test.Core.ProjectAggregate;
using Xunit;

namespace CleanArchitecture.Test.IntegrationTests.Data;
public class EfRepositoryAdd : BaseEfRepoTestFixture
{
  [Fact]
  public async Task AddsProjectAndSetsId()
  {
    var testProjectName = "testProject";
    var testProjectStatus = PriorityStatus.Backlog;
    var repository = GetRepository();
    var project = new Project(testProjectName, testProjectStatus);

    await repository.AddAsync(project);

    var newProject = (await repository.ListAsync())
                    .FirstOrDefault();

    Assert.Equal(testProjectName, newProject?.Name);
    Assert.Equal(testProjectStatus, newProject?.Priority);
    Assert.True(newProject?.Id > 0);
  }
}
