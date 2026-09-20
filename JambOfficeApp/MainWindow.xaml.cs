using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using JambOfficeApp.Services;
using JambOfficeApp.ViewModels;

namespace JambOfficeApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        bool isDark = ThemeService.IsWindowsInDarkMode();
        DarkModeToggle.Checked -= DarkModeToggle_OnToggled;
        DarkModeToggle.Unchecked -= DarkModeToggle_OnToggled;
        DarkModeToggle.IsChecked = isDark;
        DarkModeToggle.Checked += DarkModeToggle_OnToggled;
        DarkModeToggle.Unchecked += DarkModeToggle_OnToggled;

        SourceInitialized += (s, e) => SetTitleBarTheme(isDark);
    }

    private void TabControl_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.Source is TabControl tabControl && tabControl.SelectedIndex == 0 &&
            DashboardTab.DataContext is DashboardViewModel vm)
        {
            vm.RefreshCommand.Execute(null);
        }
    }

    private void DarkModeToggle_OnToggled(object sender, RoutedEventArgs e)
    {
        bool isDark = DarkModeToggle.IsChecked == true;
        ThemeService.ApplyTheme(isDark);
        SetTitleBarTheme(isDark);
    }

    [DllImport("dwmapi.dll")]
    private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attribute, ref int pvAttribute, int cbAttribute);

    private const int DwmaUseImmersiveDarkMode = 20;

    public void SetTitleBarTheme(bool isDark)
    {
        var hwnd = new WindowInteropHelper(this).Handle;
        if (hwnd == IntPtr.Zero) return;

        int useDark = isDark ? 1 : 0;
        DwmSetWindowAttribute(hwnd, DwmaUseImmersiveDarkMode, ref useDark, sizeof(int));
    }
}