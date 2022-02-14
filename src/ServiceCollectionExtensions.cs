using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PowerUtils.BuildingBlocks.Data.Repositories;

namespace PowerUtils.BuildingBlocks.Data
{
    /// <summary>
    /// Extensions to scan for repositories/UnitOfWork and registers them.
    /// - Scans for any interface implementations and registers them as <see cref="ServiceLifetime.Scoped"/>
    /// After calling AddRepositories you can use the container to resolve direct interfaces implemented.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        private static readonly Type[] _interfaces = new[]
        {
            typeof(IUnitOfWork),
            typeof(IRepositoryBase)
        };

        /// <summary>
        /// Registers Repositories and UnitOfWork types from the specified assemblies (Default assembly currect assembly)
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="lifetime">Life time for repositories and unit of works (Default life time: Scoped)</param>
        /// <returns>Service collection</returns>
        /// <exception cref="AmbiguousMatchException">When ambiguous repository interfaces exist</exception>
        /// <exception cref="ArgumentException">When no assemblies found to scan repositories</exception>
        public static IServiceCollection AddRepositories(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Scoped)
            => _registrarRepositories(services, AppDomain.CurrentDomain.GetAssemblies(), lifetime);

        /// <summary>
        /// Registers Repositories and UnitOfWork types from the specified assemblies (Default life time: Scoped)
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="assemblies">Assemblies to scan</param>
        /// <returns>Service collection</returns>
        /// <exception cref="AmbiguousMatchException">When ambiguous repository interfaces exist</exception>
        /// <exception cref="ArgumentException">When no assemblies found to scan repositories</exception>
        public static IServiceCollection AddRepositories(this IServiceCollection services, params Assembly[] assemblies)
            => _registrarRepositories(services, assemblies, ServiceLifetime.Scoped);

        /// <summary>
        /// Registers Repositories and UnitOfWork types from the specified assemblies
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="lifetime">Life time for repositories and unit of works</param>
        /// <param name="assemblies">Assemblies to scan</param>
        /// <returns>Service collection</returns>
        /// <exception cref="AmbiguousMatchException">When ambiguous repository interfaces exist</exception>
        /// <exception cref="ArgumentException">When no assemblies found to scan repositories</exception>
        public static IServiceCollection AddRepositories(this IServiceCollection services, ServiceLifetime lifetime, params Assembly[] assemblies)
            => _registrarRepositories(services, assemblies, lifetime);

        private static IServiceCollection _registrarRepositories(IServiceCollection services, IEnumerable<Assembly> assemblies, ServiceLifetime lifetime)
        {
            if(assemblies == null || !assemblies.Any())
            {
                throw new ArgumentException("No assemblies found to scan. Supply at least one assembly to scan for repositories.");
            }

            var concreteImplementations = new List<Type>();
            foreach(var type in _interfaces)
            {
                var types = _getConcreteImplementations(assemblies, type);
                concreteImplementations.AddRange(types);
            }


            foreach(var implementation in concreteImplementations)
            {
                var directInterfaces = _getDirectInterfaces(implementation);

                foreach(var @interface in directInterfaces)
                { // Register service in container

                    _registerService(services, lifetime, @interface, implementation);
                }
            }

            return services;
        }

        private static IEnumerable<Type> _getConcreteImplementations(IEnumerable<Assembly> assemblies, Type type)
        { // https://stackoverflow.com/questions/26733/getting-all-types-that-implement-an-interface
            var types = assemblies
                .SelectMany(s => s.GetTypes())
                .Where(w =>
                    type.IsAssignableFrom(w)
                    &&
                    _isConcrete(w)
                );

            return types;
        }

        private static bool _isConcrete(Type type)
            => !type.GetTypeInfo().IsAbstract && !type.GetTypeInfo().IsInterface;

        private static IEnumerable<Type> _getDirectInterfaces(Type type)
        { // https://stackoverflow.com/questions/5318685/get-only-direct-interface-instead-of-all
            var allInterfaces = type.GetInterfaces();

            return allInterfaces
                .Where(w => !allInterfaces.Any(a => a.GetInterfaces().Contains(w)));
        }

        private static void _registerService(IServiceCollection services, ServiceLifetime lifetime, Type @interface, Type implementation)
        {
            if(services.Any(x => x.ServiceType == @interface))
            {
                var currentImplementation = services.Single(x => x.ServiceType == @interface);
                throw new AmbiguousMatchException($"The interface '{@interface.Name}' has a ambiguas implementation. '{currentImplementation.ImplementationType.Name}' and '{implementation.Name}'.");
            }

            switch(lifetime)
            {
                case ServiceLifetime.Transient:
                    services.TryAddTransient(@interface, implementation);
                    break;

                case ServiceLifetime.Scoped:
                    services.TryAddScoped(@interface, implementation);
                    break;

                case ServiceLifetime.Singleton:
                    services.TryAddSingleton(@interface, implementation);
                    break;
            }
        }
    }
}
