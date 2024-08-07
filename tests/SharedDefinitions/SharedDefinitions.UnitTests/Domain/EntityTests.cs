﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedDefinitions.Domain.Common.DDD;

namespace SharedDefinitions.UnitTests.Domain;

public class EntityTests
{
    public class TestEntity(int id) : Entity<int>(id)
    {
        protected override object GetValidator()
        {
            return new TestEntityValidator();
        }
    }

    public class TestEntityValidator : AbstractValidator<TestEntity>
    {
        public TestEntityValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }

    [Fact]
    public void Entity_EqualityOperator_WithSameIds_ReturnsTrue()
    {
        // Arrange
        var entity1 = new TestEntity(1);
        var entity2 = new TestEntity(1);

        // Act
        var result = entity1 == entity2;

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Entity_EqualityOperator_WithDifferentIds_ReturnsFalse()
    {
        // Arrange
        var entity1 = new TestEntity(1);
        var entity2 = new TestEntity(2);

        // Act
        var result = entity1 == entity2;

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void Entity_GetHashCode_ReturnsCorrectHashCode()
    {
        // Arrange
        var entity = new TestEntity(1);
        var expectedHashCode = 1.GetHashCode();

        // Act
        var hashCode = entity.GetHashCode();

        // Assert
        hashCode.Should().Be(expectedHashCode);
    }

    [Fact]
    public void Entity_Equals_WithSameEntity_ReturnsTrue()
    {
        // Arrange
        var entity1 = new TestEntity(1);
        var entity2 = entity1;

        // Act
        var result = entity1.Equals(entity2);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Entity_Equals_WithDifferentEntity_ReturnsFalse()
    {
        // Arrange
        var entity1 = new TestEntity(1);
        var entity2 = new TestEntity(2);

        // Act
        var result = entity1.Equals(entity2);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void Entity_GetId_ReturnsCorrectId()
    {
        // Arrange
        var entity = new TestEntity(1);

        // Act
        var id = entity.GetId();

        // Assert
        id.Should().Be(1);
    }
}