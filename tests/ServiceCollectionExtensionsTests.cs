using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using PowerUtils.BuildingBlocks.Data.Extensions.Microsoft.DI.Tests.Fakes;
using PowerUtils.BuildingBlocks.Data.Repositories;
using PowerUtils.xUnit.Extensions;

namespace PowerUtils.BuildingBlocks.Data.Extensions.Microsoft.DI.Tests;

[Trait("Extension", "Extensions")]
public class ServiceCollectionExtensionsTests
{
    private readonly IServiceCollection _services;

    public ServiceCollectionExtensionsTests()
        => _services = new ServiceCollection();

    [Fact(DisplayName = "AddRepositories without passing the Assembly - The container should have more zero services")]
    public void AddRepositories_WithoutAssembly_GreaterThanZero()
    {
        // Arrange & Act
        _services.AddRepositories();


        // Assert
        _services.Should()
            .HaveCountGreaterThan(0);
    }

    [Fact(DisplayName = "AddRepositories with passing the Assembly - The container should have more zero services")]
    public void AddRepositories_WithAssembly_GreaterThanZero()
    {
        // Arrange & Act
        _services.AddRepositories(typeof(ServiceCollectionExtensionsTests).Assembly);


        // Assert
        _services.Should()
            .HaveCountGreaterThan(0);
    }

    [Fact(DisplayName = "Try add repositories from an assembly without repositories - Should not add repositories")]
    public void AddRepositories_AssemblyWithoutRepositories_ZeroRepositories()
    {
        // Arrange & Act
        _services.AddRepositories(ServiceLifetime.Singleton, typeof(IRepositoryBase).Assembly);


        // Assert
        _services.Should()
            .HaveCount(0);
    }

    [Fact(DisplayName = "AddRepositories with life time transient - Should add the repository")]
    public void ServiceLifetime_Transient_Added()
    {
        // Arrange & Act
        _services.AddRepositories(ServiceLifetime.Transient, typeof(ServiceCollectionExtensionsTests).Assembly);


        // Assert
        _services.Should()
            .HaveCountGreaterThan(0);
    }

    [Fact(DisplayName = "AddRepositories with life time scoped - Should add the repository")]
    public void ServiceLifetime_Scoped_Added()
    {
        // Arrange & Act
        _services.AddRepositories(ServiceLifetime.Scoped, typeof(ServiceCollectionExtensionsTests).Assembly);


        // Assert
        _services.Should()
            .HaveCountGreaterThan(0);
    }

    [Fact(DisplayName = "AddRepositories with life time singleton - Should add the repository")]
    public void ServiceLifetime_Singleton_Added()
    {
        // Arrange & Act
        _services.AddRepositories(ServiceLifetime.Singleton, typeof(ServiceCollectionExtensionsTests).Assembly);


        // Assert
        _services.Should()
            .HaveCountGreaterThan(0);
    }

    [Fact(DisplayName = "Try add repositories with null assembly - Should return exception")]
    public void AddRepositories_NullAssemblies_Exception()
    {
        // Arrange & Act
        var act = Record.Exception(() => _services.AddRepositories(ServiceLifetime.Singleton, null));


        // Assert
        act.Should()
            .BeOfType<ArgumentException>();
    }

    [Fact(DisplayName = "Try add repositories with empty assembly - Should return exception")]
    public void AddRepositories_EmptyAssemblies_Exception()
    {
        // Arrange
        var assembly = Array.Empty<Assembly>();


        // Act
        var act = Record.Exception(() => _services.AddRepositories(ServiceLifetime.Singleton, assembly));


        // Assert
        act.Should()
            .BeOfType<ArgumentException>();
    }

    [Fact(DisplayName = "Try add repositories twices - Should return exception")]
    public void RegisterService_Twices_Exception()
    {
        // Arrange
        _services.AddRepositories();


        // Act
        var act = Record.Exception(() =>
            ObjectInvoker.Invoke(typeof(ServiceCollectionExtensions), "_registerService", _services, ServiceLifetime.Transient, typeof(IFakeProductsRepository), typeof(IFakeProductsRepository))
        );


        // Assert
        act.Should()
            .BeOfType<AmbiguousMatchException>();
    }
}
