using Microsoft.Extensions.DependencyInjection;
using MonkeyTyper.WinForms.Forms;
using MonkeyTyper.WinForms.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using MonkeyTyper.Core.Format;
using MonkeyTyper.Core.Mail;
using MonkeyTyper.Core.Plugins;
using MonkeyTyper.WinForms.Helpers;

namespace MonkeyTyper.WinForms
{
    /// <summary>
    /// The main entry point container.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            SetHighDpiMode();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(CreateServiceProvider().GetRequiredService<Form>());
        }

        /// <summary>
        /// Analog of the: Application.SetHighDpiMode(HighDpiMode.SystemAware);
        /// </summary>
        /// <returns>Indicates whether the operation was successful.</returns>
        [DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        /// <summary>
        /// Enables high DPI awareness.
        /// </summary>
        private static void SetHighDpiMode()
        {
            if (Environment.OSVersion.Version.Major >= 6)
                SetProcessDPIAware();
        }

        /// <summary>
        /// Creates a new service provider.
        /// </summary>
        /// <returns>Initialized <see cref="IServiceProvider"/> instance.</returns>
        private static IServiceProvider CreateServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            ConfigurePlugins(services);
            ConfigureEntryForm(services);
            ConfigureAppSettings(services);
            return services.BuildServiceProvider();
        }

        /// <summary>
        /// Adds all required services to the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> instance.</param>
        private static void ConfigureServices(IServiceCollection services)
        {
            IEnumerable<Type> forms = Assembly.GetExecutingAssembly()
                .ExportedTypes
                .Where(typeof(Form).IsAssignableFrom)
                .Where(x => !x.IsAbstract);

            foreach (Type form in forms)
                services.AddScoped(form);

            services
                .AddScoped<IProjectProperties, AssemblyProjectProperties>()
                .AddScoped<IStringFormatter, DefaultStringFormatter>()
                .AddScoped<IMailClient, DefaultMailClient>()
                .AddScoped<IMessageBuilder, DefaultMessageBuilder>()
                .AddScoped<IPluginManager, DefaultPluginManager>()
                .AddSingleton<ISettingsProvider>(new DefaultSettingsProvider());
        }

        /// <summary>
        /// Loads all plugins to the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> instance.</param>
        /// <TODO>
        /// Add the ability to load a third party <see cref="IPluginManager"/>.
        /// </TODO>
        private static void ConfigurePlugins(IServiceCollection services)
        {
            IServiceProvider provider = services.BuildServiceProvider();
            IPluginManager manager = provider.GetRequiredService<IPluginManager>();
            ISettingsProvider settingsProvider = provider.GetRequiredService<ISettingsProvider>();

            foreach (var descriptor in manager.Load())
            {
                services.Add(descriptor.Transform());
                if (descriptor.SettingsType is { } settingsType)
                {
                    settingsProvider.Register(settingsType);
                    services.AddScoped(settingsType, provider => provider.GetRequiredService<ISettingsProvider>().Get(settingsType));
                }
            }
        }

        /// <summary>
        /// Adds entry form to the <see cref="IServiceCollection"/>
        /// as <see cref="Form"/> service.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> instance.</param>
        private static void ConfigureEntryForm(IServiceCollection services) => services.AddScoped<Form, MainForm>();

        /// <summary>
        /// Adds <see cref="AppSettings"/> to the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> instance.</param>
        private static void ConfigureAppSettings(IServiceCollection services)
        {
            IServiceProvider provider = services.BuildServiceProvider();
            ISettingsProvider settingsProvider = provider.GetRequiredService<ISettingsProvider>();
            settingsProvider.RegisterAppSettings(typeof(AppSettings));
            services.AddScoped(typeof(AppSettings), provider => provider.GetRequiredService<ISettingsProvider>().Get(typeof(AppSettings)));
        }
    }
}
