using Avalonia;
using System;
using System.Reflection;

namespace AutoClicker.Desktop;

sealed class Program
{
    private static Assembly Assembly { get; } = Assembly.GetExecutingAssembly();

    public static string Name { get; } = Assembly.GetName().Name ?? "Avalonia Auto Clicker";

    private static Version Version { get; } = Assembly.GetName().Version ?? new Version(0, 0, 0);

    public static string VersionString { get; } = Version.ToString(3);

    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}
