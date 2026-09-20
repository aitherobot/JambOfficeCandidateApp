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
}