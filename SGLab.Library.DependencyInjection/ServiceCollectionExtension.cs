using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection {

    /// <summary>
    /// Provides extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
	public static class ServiceCollectionExtension {

        /// <summary>
        /// Search for all classes in the given assembly that has one of the <see cref="SingletonServiceAttribute"/>,
        /// <see cref="ScopedServiceAttribute"/> or <see cref="TransientServiceAttribute"/>, and register them with
        /// <see cref="ServiceProviderExtension.CreateAndInject(System.IServiceProvider, System.Type, object[])"/>
        /// method as implementation factory.
        /// </summary>
        /// <param name="services">Service provider</param>
        /// <param name="assm">Assembly to be searched</param>
        /// <returns>The same service provider for method chaining</returns>
        public static IServiceCollection AddServicesByAttribute(this IServiceCollection services, Assembly assm) {
            assm.GetTypes()
                .Where(c => c.IsClass && !c.IsAbstract)
                .ToList()
                .ForEach(c => {
                    if(c.GetCustomAttribute<SingletonServiceAttribute>() is SingletonServiceAttribute ss) {
                        services.AddSingleton(ss.Type ?? c, p => p.CreateAndInject(c));
                    } else if(c.GetCustomAttribute<ScopedServiceAttribute>() is ScopedServiceAttribute cs) {
                        services.AddScoped(cs.Type ?? c, p => p.CreateAndInject(c));
                    } else if(c.GetCustomAttribute<TransientServiceAttribute>() is TransientServiceAttribute ts) {
                        services.AddTransient(ts.Type ?? c, p => p.CreateAndInject(c));
                    }
                });
            return services;
        }
    }
}
