using Microsoft.Extensions.DependencyInjection;
using MonkeyTyper.WinForms.Forms;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

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
            ConfigureEntryForm(services);
            return services.BuildServiceProvider();
        }

        /// <summary>
        /// Adds all required services to the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> instance.</param>
        private static void ConfigureServices(IServiceCollection services)
        {

        }

        /// <summary>
        /// Adds entry form to the <see cref="IServiceCollection"/>
        /// as <see cref="Form"/> service.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> instance.</param>
        private static void ConfigureEntryForm(IServiceCollection services) => services.AddScoped<Form, MainForm>();
    }
}
