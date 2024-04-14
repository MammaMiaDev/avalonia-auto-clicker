using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using AutoClicker.Desktop.ViewModels;
using AutoClicker.Desktop.Views;
using Avalonia.Media;
using SharpHook;

namespace AutoClicker.Desktop;

public partial class App : Application
{
    public TaskPoolGlobalHook Hook { get; } = new();
    public EventSimulator Simulator { get; } = new();

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (Current is not null)
        {
            Current.Resources["RunningBrush"] = new SolidColorBrush(Colors.ForestGreen);
            Current.Resources["StoppedBrush"] = new SolidColorBrush(Colors.IndianRed);
        }

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            _ = Hook.RunAsync();
            desktop.Exit += OnExit;

            // Line below is needed to remove Avalonia data validation.
            // Without this line you will get duplicate validations from both Avalonia and CT
            BindingPlugins.DataValidators.RemoveAt(0);

            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void OnExit(object? sender, ControlledApplicationLifetimeExitEventArgs e)
    {
        // the hook needs to be disposed, otherwise the application will keep on running
        Hook.Dispose();
    }
}
