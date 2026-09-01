using System.Configuration;
using System.Data;
using System.Windows;
using JambOffuceApp.Data;
using Microsoft.EntityFrameworkCore;

namespace JambOffuceApp;

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
    }
}