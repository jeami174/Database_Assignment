using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using Presentation_WPF_MainApplication.ViewModels;

namespace Presentation_WPF_MainApplication;

public partial class MainWindow : Window
{
    public MainWindow(MainViewModel mainViewModel)
    {
        InitializeComponent();
        DataContext = mainViewModel; // Sätter MainViewModel som DataContext
    }

    // Exit-knappens click-hanterare
    private void ExitButton_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    // Gör det möjligt att dra fönstret genom att klicka på Border-området
    private void Border_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
        {
            DragMove();
        }
    }

}
