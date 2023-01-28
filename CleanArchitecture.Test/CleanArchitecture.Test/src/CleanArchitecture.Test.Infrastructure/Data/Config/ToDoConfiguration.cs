﻿using CleanArchitecture.Test.Core.ProjectAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Test.Infrastructure.Data.Config;
public class ToDoConfiguration : IEntityTypeConfiguration<ToDoItem>
{
  public void Configure(EntityTypeBuilder<ToDoItem> builder)
  {
    builder.Property(t => t.Title)
        .IsRequired();
    builder.Property(t => t.ContributorId)
        .IsRequired(false);
  }
}
