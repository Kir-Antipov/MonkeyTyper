using System;

namespace MonkeyTyper.Core.Plugins
{
    /// <summary>
    /// Marks class as a service that needs to be registered.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ServiceAttribute : Attribute
    {
        /// <summary>
        /// The <see cref="Type"/> of the service.
        /// </summary>
        /// <remarks>
        /// If no value was provided, the type to which
        /// this attribute was applied is used.
        /// </remarks>
        public Type? ServiceType { get; set; }

        /// <summary>
        /// The <see cref="Type"/> of service's <see cref="ISettings"/>.
        /// </summary>
        /// <remarks>
        /// You can register a service-related settings type
        /// that implements the <see cref="ISettings"/> interface.
        /// <para/>
        /// The application is obliged to pass an instance of the
        /// specified type to the service constructor.
        /// </remarks>
        /// <example>
        /// <![CDATA[
        /// [Service(SettingsType = typeof(FooSettings))]
        /// public class Foo
        /// {
        ///     private ISettings Settings { get; }
        ///     
        ///     public Foo(FooSettings settings)
        ///     {
        ///         Settings = settings;
        ///     }
        /// }
        /// ]]>
        /// </example>
        public Type? SettingsType { get; set; }

        /// <summary>
        /// The <see cref="ServiceLifetime"/> of the service.
        /// </summary>
        /// <remarks>
        /// Default value is <see cref="ServiceLifetime.Scoped"/>.
        /// </remarks>
        public ServiceLifetime Lifetime { get; set; } = ServiceLifetime.Scoped;
    }
}
