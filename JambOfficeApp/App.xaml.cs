using System.Configuration;
using System.Data;
using System.Windows;
using JambOfficeApp.Data;
using Microsoft.EntityFrameworkCore;

namespace JambOfficeApp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        using (var db = new AppDbContext())
        {
            db.Database.Migrate();
        }
        
        ShutdownMode = ShutdownMode.OnExplicitShutdown;
        
        var loginView = new Views.LoginView();
        loginView.ShowDialog();

        if (loginView.LoggedInUser == null)
        {
            Shutdown();
            return;
        }
        
        ShutdownMode = ShutdownMode.OnMainWindowClose;
        
        var mainWindow = new MainWindow();
        MainWindow = mainWindow;
        mainWindow.Show();
    }
}