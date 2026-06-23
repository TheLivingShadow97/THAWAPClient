using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Serilog;

namespace THAWAPClient;

public partial class THAWControlsWindow : Window
{
    public THAWControlsWindow()
    {
        InitializeComponent();
    }
    protected override void OnClosing(WindowClosingEventArgs e)
    {
        if (IsVisible && !e.IsProgrammatic)
        {
            Hide();
            e.Cancel = true;
        }    
        else
            base.OnClosing(e);
    }
}
