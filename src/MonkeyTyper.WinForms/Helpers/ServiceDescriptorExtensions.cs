using MonkeyTyperServiceDescriptor = MonkeyTyper.Core.Plugins.ServiceDescriptor;
using MicrosoftServiceDescriptor = Microsoft.Extensions.DependencyInjection.ServiceDescriptor;
using ServiceLifetime = Microsoft.Extensions.DependencyInjection.ServiceLifetime;
using System;

namespace MonkeyTyper.WinForms.Helpers
{
    internal static class ServiceDescriptorExtensions
    {
        public static MicrosoftServiceDescriptor Transform(this MonkeyTyperServiceDescriptor serviceDescriptor) => serviceDescriptor switch
        {
            { ImplementationInstance: { } } => new MicrosoftServiceDescriptor(serviceDescriptor.ServiceType, serviceDescriptor.ImplementationInstance),
            { ImplementationFactory: { } } => new MicrosoftServiceDescriptor(serviceDescriptor.ServiceType, serviceDescriptor.ImplementationFactory, (ServiceLifetime)(int)serviceDescriptor.Lifetime),
            { } => new MicrosoftServiceDescriptor(serviceDescriptor.ServiceType, serviceDescriptor.ImplementationType, (ServiceLifetime)(int)serviceDescriptor.Lifetime),
            _ => throw new ArgumentNullException(nameof(serviceDescriptor))
        };
    }
}
