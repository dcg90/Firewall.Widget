using FirewallWidget.Manager;
using FirewallWidget.Manager.Contracts.Services;
using FirewallWidget.Presentation;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace FirewallWidget
{
    internal static class Program
    {
        public static ServiceProvider Provider { get; private set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            var p = Process.GetCurrentProcess();
            if (Process.GetProcessesByName(p.ProcessName).Length > 1)
            {
                MessageBox.Show("The widget is already running.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var services = new ServiceCollection();
            new Startup().Configure(services);

            Provider = services.BuildServiceProvider();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(
                Provider.GetRequiredService<IFirewallService>(),
                Provider.GetRequiredService<IRuleService>()));
        }
    }
}
