using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace EngineSigma.Editor.MVVM.View;

public partial class AboutUsView 
{
    public AboutUsView()
    {
        InitializeComponent();
    }

    private void GithubOnMouseDownCarlo(object sender, MouseButtonEventArgs e)
    {
        var psi = new ProcessStartInfo()
        {
            FileName = "https://github.com/Carloo999",
            UseShellExecute = true
        };
        Process.Start(psi);
    }

    private void GithubOnMouseDownJonathan(object sender, MouseButtonEventArgs e)
    {
        var psi = new ProcessStartInfo()
        {
            FileName = "https://github.com/jonathan-knobling",
            UseShellExecute = true
        };
        Process.Start(psi);
    }

    private void EmailOnMouseDownJonathan(object sender, MouseButtonEventArgs e)
    {
        Clipboard.SetText("jonathan@gmail.com");
    }

    private void EmailOnMouseDownCarlo(object sender, MouseButtonEventArgs e)
    {
        Clipboard.SetText("carlo.bene@web.de");
    }

    private void LinkedInOnMouseDownCarlo (object sender, MouseButtonEventArgs e)
    {
        var psi = new ProcessStartInfo()
        {
            FileName = "https://www.linkedin.com/in/carlo-feddeck/",
            UseShellExecute = true
        };
        Process.Start(psi);
    }

    private void LinkedInOnMouseDownJonathan (object sender, MouseButtonEventArgs e)
    {
        var psi = new ProcessStartInfo()
        {
            FileName = "https://www.linkedin.com/in/jonathan-knobling/",
            UseShellExecute = true
        };
        Process.Start(psi);
    }
}