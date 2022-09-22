using System;

namespace Microsoft.Extensions.DependencyInjection {

	/// <summary>
	/// Base class of the three ServiceAttributes.
	/// </summary>
	public abstract class BaseServiceAttribute: Attribute {
		/// <summary>
		/// The type which the service implements. It would be the self-type if unspecified.
		/// </summary>
		public Type? Type { get; set; }
	}

	/// <summary>
	/// To be used with <see cref="ServiceCollectionExtension.AddServicesByAttribute(IServiceCollection, System.Reflection.Assembly)"/>.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
	public sealed class SingletonServiceAttribute : BaseServiceAttribute {
		/// <inheritdoc />
		public SingletonServiceAttribute(Type? type = null) { Type = type; }
	}

	/// <summary>
	/// To be used with <see cref="ServiceCollectionExtension.AddServicesByAttribute(IServiceCollection, System.Reflection.Assembly)"/>.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
	public sealed class ScopedServiceAttribute : BaseServiceAttribute {
		/// <inheritdoc />
		public ScopedServiceAttribute(Type? type = null) { Type = type; }
	}

	/// <summary>
	/// To be used with <see cref="ServiceCollectionExtension.AddServicesByAttribute(IServiceCollection, System.Reflection.Assembly)"/>.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
	public sealed class TransientServiceAttribute : BaseServiceAttribute {
		/// <inheritdoc />
		public TransientServiceAttribute(Type? type = null) { Type = type; }
	}
}
