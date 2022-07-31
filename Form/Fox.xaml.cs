using System;
using System.Diagnostics;
using System.Windows;
using Machina.FFXIV;
using Machina.Infrastructure;

namespace WPF_learning_notes.Form;

public partial class Fox
{
    public Fox()
    {
        InitializeComponent();
    }

    private readonly FFXIVNetworkMonitor _monitor = new();
    private readonly FFXIVBundleDecoder _decoder = new();

    private void Frame_OnLoaded(object sender, RoutedEventArgs e)
    {
        _monitor.ProcessID = (uint)GetFfxivProcess().Id;
        _monitor.WindowName = GetFfxivProcess().MainWindowTitle;
        _monitor.OodlePath = GetFfxivProcess().MainModule!.FileName;
        _monitor.MessageReceivedEventHandler = MessageReceived;
        _monitor.MessageSentEventHandler = MessageSent;
        _monitor.Start();
    }

    private void MessageSent(TCPConnection connection, long epoch, byte[] message)
    {
        _decoder.StoreData(message);
    }

    private void MessageReceived(TCPConnection connection, long epoch, byte[] message)
    {
        _decoder.StoreData(message);
    }


    private static Process GetFfxivProcess()
    {
        var processes = Process.GetProcessesByName("ffxiv_dx11");
        return processes[0];
    }

    private void Frame_OnClosed(object? sender, EventArgs e)
    {
        _monitor.Stop();
    }
}