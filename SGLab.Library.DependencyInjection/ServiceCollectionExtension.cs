using System.Collections.Generic;
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
		/// <para>Due to the fundamental restriction of the .NET Core DI, there is no way to apply
		/// <see cref="ServiceProviderExtension.CreateAndInject(System.IServiceProvider, System.Type, object[])"/>
		/// to open generic types; so for such classes you'll have to call
		/// <see cref="ServiceProviderExtension.Inject(System.IServiceProvider, object)" /> in the constructor
		/// in order to use <see cref="InjectAttribute"/>.</para>
		/// </summary>
		/// <param name="services">Service provider</param>
		/// <param name="assm">Assembly to be searched</param>
		/// <returns>The same service provider for method chaining</returns>
		public static IServiceCollection AddServicesByAttribute(this IServiceCollection services, Assembly assm) {
			assm.GetTypes()
				.Where(c => c.IsClass && !c.IsAbstract)
				.ToList()
				.ForEach(c => {
					// Collect all service attributes
					var attributes = new List<BaseServiceAttribute>();
					attributes.AddRange(c.GetCustomAttributes<SingletonServiceAttribute>());
					attributes.AddRange(c.GetCustomAttributes<ScopedServiceAttribute>());
					attributes.AddRange(c.GetCustomAttributes<TransientServiceAttribute>());

					var groups = attributes.GroupBy(att => att.Type ?? c).ToList();
					foreach(var group in groups) {
						// For each type, only register the one with the longest life cycle.

						var att = attributes.FirstOrDefault(a => a is SingletonServiceAttribute);
						if(att != null) {
							services.AddSingleton(group.Key, p => p.CreateAndInject(c));
							continue;
						}

						att = attributes.FirstOrDefault(a => a is ScopedServiceAttribute);
						if(att != null) {
							services.AddScoped(group.Key, p => p.CreateAndInject(c));
							continue;
						}

						att = attributes.FirstOrDefault(a => a is TransientServiceAttribute);
						if(att != null) {
							services.AddTransient(group.Key, p => p.CreateAndInject(c));
						}
					}
				});
			return services;
		}
	}
}
