using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;

namespace EngineSigma.Editor.MVVM.View;

public partial class AboutUsView : UserControl
{
    public AboutUsView()
    {
        InitializeComponent();
    }

    private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        var psi = new ProcessStartInfo()
        {
            FileName = "https://github.com/jonathan-knobling",
            UseShellExecute = true
        };
        Process.Start(psi);
    }

    private void UIElement_OnMouseDown1(object sender, MouseButtonEventArgs e)
    {
        
        var psi = new ProcessStartInfo()
        {
            FileName = "https://github.com/Carloo999",
            UseShellExecute = true
        };
        Process.Start(psi);
    }
}