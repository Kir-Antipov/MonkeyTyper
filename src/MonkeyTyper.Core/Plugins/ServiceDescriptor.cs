using System;

namespace MonkeyTyper.Core.Plugins
{
    /// <summary>
    /// Describes a service with its service type, implementation, and lifetime.
    /// </summary>
    public class ServiceDescriptor
    {
        #region Var
        /// <summary>
        /// The <see cref="Type"/> of the service.
        /// </summary>
        public Type ServiceType { get; }

        /// <summary>
        /// The <see cref="Type"/> of service's implementation.
        /// </summary>
        public Type ImplementationType { get; }

        /// <summary>
        /// The <see cref="ServiceLifetime"/> of the service.
        /// </summary>
        public ServiceLifetime Lifetime { get; }

        /// <summary>
        /// The <see cref="Type"/> of service's <see cref="ISettings"/>.
        /// </summary>
        public Type? SettingsType { get; }

        /// <summary>
        /// Service's instance.
        /// </summary>
        public object? ImplementationInstance { get; }

        /// <summary>
        /// Service's factory.
        /// </summary>
        public Func<IServiceProvider, object>? ImplementationFactory { get; }
        #endregion

        #region Init
        /// <summary>
        /// Initialize a new instance of the <see cref="ServiceDescriptor"/> class.
        /// </summary>
        /// <param name="serviceType">The <see cref="Type"/> of the service.</param>
        /// <param name="implementationType">The <see cref="Type"/> of service's implementation.</param>
        /// <param name="lifetime">The <see cref="ServiceLifetime"/> of the service.</param>
        /// <param name="settingsType">The <see cref="Type"/> of service's <see cref="ISettings"/>.</param>
        /// <param name="implementationFactory">Service's factory.</param>
        public ServiceDescriptor(Type serviceType, Type implementationType, ServiceLifetime lifetime, Type? settingsType = null, Func<IServiceProvider, object>? implementationFactory = null)
        {
            ServiceType = serviceType ?? throw new ArgumentNullException(nameof(serviceType));
            ImplementationType = implementationType ?? throw new ArgumentNullException(nameof(implementationType));
            Lifetime = lifetime;
            SettingsType = settingsType;
            ImplementationFactory = implementationFactory;
        }

        /// <inheritdoc cref="ServiceDescriptor(Type, Type, ServiceLifetime, Type, Func{object})"/>
        public ServiceDescriptor(Type implementationType, ServiceLifetime lifetime, Type? settingsType = null, Func<IServiceProvider, object>? implementationFactory = null) : this(implementationType, implementationType, lifetime, settingsType, implementationFactory) { }

        /// <summary>
        /// Initialize a new instance of the <see cref="ServiceDescriptor"/> class.
        /// <para/>
        /// This overload is for <see cref="ServiceLifetime.Singleton"/> service.
        /// </summary>
        /// <param name="serviceType">The <see cref="Type"/> of the service.</param>
        /// <param name="implementationInstance">Service's instance.</param>
        /// <param name="settingsType">The <see cref="Type"/> of service's <see cref="ISettings"/>.</param>
        public ServiceDescriptor(Type serviceType, object implementationInstance, Type? settingsType = null)
        {
            ServiceType = serviceType ?? throw new ArgumentNullException(nameof(serviceType));
            ImplementationInstance = implementationInstance ?? throw new ArgumentNullException(nameof(implementationInstance));
            ImplementationType = implementationInstance.GetType();
            Lifetime = ServiceLifetime.Singleton;
            SettingsType = settingsType;
        }

        /// <inheritdoc cref="ServiceDescriptor(Type, object, Type)"/>
        public ServiceDescriptor(object implementationInstance, Type? settingsType = null) : this(implementationInstance?.GetType() ?? throw new ArgumentNullException(nameof(implementationInstance)), implementationInstance, settingsType) { }
        #endregion
    }
}
