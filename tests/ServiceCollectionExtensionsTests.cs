using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using PowerUtils.BuildingBlocks.Data.Extensions.Microsoft.DI.Tests.Fakes;
using PowerUtils.BuildingBlocks.Data.Repositories;
using PowerUtils.xUnit.Extensions;

namespace PowerUtils.BuildingBlocks.Data.Extensions.Microsoft.DI.Tests;

public class ServiceCollectionExtensionsTests
{
    private readonly IServiceCollection _services;

    public ServiceCollectionExtensionsTests()
        => _services = new ServiceCollection();



    [Fact]
    public void WithoutAssembly_AddRepositories_GreaterThanZero()
    {
        // Arrange & Act
        _services.AddRepositories();


        // Assert
        _services.Should()
            .HaveCountGreaterThan(0);
    }

    [Fact]
    public void WithAssembly_AddRepositories_GreaterThanZero()
    {
        // Arrange & Act
        _services.AddRepositories(typeof(ServiceCollectionExtensionsTests).Assembly);


        // Assert
        _services.Should()
            .HaveCountGreaterThan(0);
    }

    [Fact]
    public void AssemblyWithoutRepositories_AddRepositories_ZeroRepositories()
    {
        // Arrange & Act
        _services.AddRepositories(ServiceLifetime.Singleton, typeof(IRepositoryBase).Assembly);


        // Assert
        _services.Should()
            .HaveCount(0);
    }

    [Fact]
    public void ServiceLifetimeTransient_AddRepositories_GreaterThanZero()
    {
        // Arrange & Act
        _services.AddRepositories(ServiceLifetime.Transient, typeof(ServiceCollectionExtensionsTests).Assembly);


        // Assert
        _services.Should()
            .HaveCountGreaterThan(0);
    }

    [Fact]
    public void ServiceLifetimeScoped_AddRepositories_GreaterThanZero()
    {
        // Arrange & Act
        _services.AddRepositories(ServiceLifetime.Scoped, typeof(ServiceCollectionExtensionsTests).Assembly);


        // Assert
        _services.Should()
            .HaveCountGreaterThan(0);
    }

    [Fact]
    public void ServiceLifetimeSingleton_AddRepositories_GreaterThanZero()
    {
        // Arrange & Act
        _services.AddRepositories(ServiceLifetime.Singleton, typeof(ServiceCollectionExtensionsTests).Assembly);


        // Assert
        _services.Should()
            .HaveCountGreaterThan(0);
    }

    [Fact]
    public void NullAssemblies_AddRepositories_Exception()
    {
        // Arrange & Act
        var act = Record.Exception(() => _services.AddRepositories(ServiceLifetime.Singleton, null));


        // Assert
        act.Should()
            .BeOfType<ArgumentException>();
    }

    [Fact]
    public void EmptyAssemblies_AddRepositories_Exception()
    {
        // Arrange
        var assembly = Array.Empty<Assembly>();


        // Act
        var act = Record.Exception(() => _services.AddRepositories(ServiceLifetime.Singleton, assembly));


        // Assert
        act.Should()
            .BeOfType<ArgumentException>();
    }

    [Fact]
    public void TwicesSameInterface_RegisterService_Exception()
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
