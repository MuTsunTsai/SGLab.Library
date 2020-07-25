using System;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection {

	/// <summary>
	/// Provides extension methods for <see cref="IServiceProvider"/>.
	/// </summary>
	public static class ServiceProviderExtension {

		/// <summary>
		/// Inject dependencies into the properties or fields with <see cref="InjectAttribute"/> or <see cref="OptionalInjectAttribute"/> of the target object.
		/// </summary>
		/// <param name="provider">Service provider</param>
		/// <param name="target">The target object</param>
		public static void Inject(this IServiceProvider provider, object target) {
			var type = target.GetType();
			var props = type.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
			foreach(var prop in props.Where(p => p.SetMethod != null)) {
				if(prop.GetCustomAttribute<InjectAttribute>() != null) {
					prop.SetValue(target, provider.GetRequiredService(prop.PropertyType));
				} else if(prop.GetCustomAttribute<OptionalInjectAttribute>() != null) {
					prop.SetValue(target, provider.GetService(prop.PropertyType));
				}
			}

			var fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
			foreach(var field in fields) {
				if(field.GetCustomAttribute<InjectAttribute>() != null) {
					field.SetValue(target, provider.GetRequiredService(field.FieldType));
				} else if(field.GetCustomAttribute<OptionalInjectAttribute>() != null) {
					field.SetValue(target, provider.GetService(field.FieldType));
				}
			}
		}

		/// <summary>
		/// Calls <see cref="ActivatorUtilities.CreateInstance{T}(IServiceProvider, object[])"/> and <see cref="Inject(IServiceProvider, object)"/> in one step.
		/// </summary>
		/// <typeparam name="T">Type of the instance to be created</typeparam>
		/// <param name="provider">Service provider</param>
		/// <param name="parameters">Additional parameters for the construction</param>
		/// <returns>Created type-T object</returns>
		public static T CreateAndInject<T>(this IServiceProvider provider, params object[] parameters) where T : class {
			var result = ActivatorUtilities.CreateInstance<T>(provider, parameters);
			provider.Inject(result);
			return result;
		}

		/// <summary>
		/// Calls <see cref="ActivatorUtilities.CreateInstance(IServiceProvider, Type, object[])"/> and <see cref="Inject(IServiceProvider, object)"/> in one step.
		/// </summary>
		/// <param name="provider">Service provider</param>
		/// <param name="type">Type of the instance to be created</param>
		/// <param name="parameters">Additional parameters for the construction</param>
		/// <returns>Created object</returns>
		public static object CreateAndInject(this IServiceProvider provider, Type type, params object[] parameters) {
			var result = ActivatorUtilities.CreateInstance(provider, type, parameters);
			provider.Inject(result);
			return result;
		}
	}
}
