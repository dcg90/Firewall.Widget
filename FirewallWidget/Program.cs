using FirewallWidget.ChildForms;
using FirewallWidget.Manager;
using FirewallWidget.Presentation;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace FirewallWidget
{
    internal static class Program
    {
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

            services.AddSingleton<MainForm>();
            services.AddTransient<OptionsForm>();
            services.AddTransient<AddRulesForm>();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(services.BuildServiceProvider().GetRequiredService<MainForm>());
        }
    }
}
