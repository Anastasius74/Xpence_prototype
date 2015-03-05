using System.Windows;
using System.Windows.Threading;
using ABB.AC800PEC.DbConfigTool.Infrastructure.MessagingService;
using ABB.AC800PEC.DbConfigTool.Infrastructure.Utility;
using ABB.AC800PEC.DbConfigTool.Logging;
using ABB.AC800PEC.DbConfigTool.Shared;
using ABB.AC800PEC.DbConfigTool.ViewModels;
using ABB.AC800PEC.DbConfigTool.Views;

namespace ABB.AC800PEC.DbConfigTool
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private MainWindowViewModel viewModel;

        private void AppDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            LogUtil.LogError("App", "AppDispatcherUnhandledException", e.Exception);
            e.Handled = true;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            LogUtil.LogInfo("App", "OnStartup", "Staring up.");
            base.OnStartup(e);
            var window = new Shell();
            // Create the ViewModel to which 
            // the main window binds.
            viewModel = new MainWindowViewModel(MessageServiceFactory.GetMessagingServiceInstance());
            //Set the view models to the windows data context.
            window.DataContext = viewModel;
            window.Show();

            //Register views for secondary windows.
            ModalViewRegistry.Instance.RegisterView(ApplicationConstants.HelpView, typeof(HelpView));
        }

        protected override void OnExit(ExitEventArgs e)
        {
            if (null != viewModel)
                viewModel.Dispose();
            LogUtil.LogInfo("App", "OnExit", "Application closing.");
        }
    }
}
