using System;

namespace Microsoft.Extensions.DependencyInjection {

	/// <summary>
	/// Similar to the InjectAttribute in Blazor, but works with <see cref="ServiceProviderExtension.Inject(IServiceProvider, object)"/> method.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public sealed class InjectAttribute : Attribute { }

	/// <summary>
	/// Like <see cref="InjectAttribute"/>, but does not strictly requires the corresponding service to be registered.
	/// <para>If the corresponding service is not found, the value will simply be null.</para>
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public sealed class OptionalInjectAttribute : Attribute { }
}
