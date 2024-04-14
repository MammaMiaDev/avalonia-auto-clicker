using System;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;
using SharpHook;
using SharpHook.Native;

namespace AutoClicker.Desktop.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public string Title { get; } = $"{Program.Name} v{Program.VersionString}";

    [ObservableProperty]
    private int _clickInterval = 100;
    [ObservableProperty]
    private bool _clickingStatus;

    [ObservableProperty]
    private Task? _task;
    private CancellationTokenSource? _token;

    partial void OnTaskChanged(Task? value)
    {
        ClickingStatus = value is not null;
    }

    partial void OnClickIntervalChanging(int oldValue, int newValue)
    {
        Console.WriteLine("changing");
    }

    private static App AppCurrent => (App)Application.Current!;

    public MainWindowViewModel()
    {
        AppCurrent.Hook.KeyPressed += OnKeyPressed;
    }

    private void OnKeyPressed(object? sender, KeyboardHookEventArgs e)
    {
        if (e.Data.KeyCode is not KeyCode.VcF6) return;

        // stop clicking
        if (Task is not null)
        {
            _token?.Cancel();
            Task = null;
            return;
        }

        // start clicking
        _token = new CancellationTokenSource();
        Task = Task.Run(async () =>
        {
            while (!_token.IsCancellationRequested)
            {
                AppCurrent.Simulator.SimulateMousePress(MouseButton.Button1);
                AppCurrent.Simulator.SimulateMouseRelease(MouseButton.Button1);
                await Task.Delay(ClickInterval, _token.Token);
            }
        });
    }
}
