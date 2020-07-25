using System;

namespace Microsoft.Extensions.DependencyInjection {

    /// <summary>
    /// To be used with <see cref="ServiceCollectionExtension.AddServicesByAttribute(IServiceCollection, System.Reflection.Assembly)"/>.
    /// </summary>
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class SingletonServiceAttribute : Attribute {
        public Type Type { get; set; }
        public SingletonServiceAttribute(Type type = null) { Type = type; }
    }

    /// <summary>
    /// To be used with <see cref="ServiceCollectionExtension.AddServicesByAttribute(IServiceCollection, System.Reflection.Assembly)"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class ScopedServiceAttribute : Attribute {
        public Type Type { get; set; }
        public ScopedServiceAttribute(Type type = null) { Type = type; }
    }

    /// <summary>
    /// To be used with <see cref="ServiceCollectionExtension.AddServicesByAttribute(IServiceCollection, System.Reflection.Assembly)"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class TransientServiceAttribute : Attribute {
        public Type Type { get; set; }
        public TransientServiceAttribute(Type type = null) { Type = type; }
    }
}
