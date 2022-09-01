using System;

namespace Microsoft.Extensions.DependencyInjection {

	/// <summary>
	/// To be used with <see cref="ServiceCollectionExtension.AddServicesByAttribute(IServiceCollection, System.Reflection.Assembly)"/>.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public sealed class SingletonServiceAttribute : Attribute {

		/// <summary>
		/// The type which the service implements. It would be the self-type if unspecified.
		/// </summary>
		public Type? Type { get; set; }

		/// <inheritdoc />
		public SingletonServiceAttribute(Type? type = null) { Type = type; }
	}

	/// <summary>
	/// To be used with <see cref="ServiceCollectionExtension.AddServicesByAttribute(IServiceCollection, System.Reflection.Assembly)"/>.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public sealed class ScopedServiceAttribute : Attribute {

		/// <summary>
		/// The type which the service implements. It would be the self-type if unspecified.
		/// </summary>
		public Type? Type { get; set; }

		/// <inheritdoc />
		public ScopedServiceAttribute(Type? type = null) { Type = type; }
	}

	/// <summary>
	/// To be used with <see cref="ServiceCollectionExtension.AddServicesByAttribute(IServiceCollection, System.Reflection.Assembly)"/>.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public sealed class TransientServiceAttribute : Attribute {

		/// <summary>
		/// The type which the service implements. It would be the self-type if unspecified.
		/// </summary>
		public Type? Type { get; set; }

		/// <inheritdoc />
		public TransientServiceAttribute(Type? type = null) { Type = type; }
	}
}
