using System.Windows;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;

namespace JambOfficeApp.Services;

public static class ThemeService
{
    public static bool IsWindowsInDarkMode()
    {
        try
        {
            using var key = Registry.CurrentUser.OpenSubKey(
                @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
            
            var value = key?.GetValue("AppsUseLightTheme");
            if (value is int intValue)
            {
                return intValue == 0;
            }
        }
        catch
        {
            // ignored
        }
        
        return false;
    }

    public static void ApplyTheme(bool isDark)
    {
        var bundledTheme = Application.Current.Resources.MergedDictionaries
            .OfType<BundledTheme>()
            .FirstOrDefault();

        if (bundledTheme == null) return;
        
        bundledTheme.BaseTheme = isDark ? BaseTheme.Dark : BaseTheme.Light;
    }
    
    [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
    private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attribute, ref int pvAttribute, int cbAttribute);

    private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;

    // Applies dark/light title bar chrome to any window, given its handle.
    public static void SetTitleBarTheme(IntPtr hwnd, bool isDark)
    {
        if (hwnd == IntPtr.Zero) return;

        int useDark = isDark ? 1 : 0;
        DwmSetWindowAttribute(hwnd, DWMWA_USE_IMMERSIVE_DARK_MODE, ref useDark, sizeof(int));
    }
}