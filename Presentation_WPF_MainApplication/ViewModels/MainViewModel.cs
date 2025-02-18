using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation_WPF_MainApplication.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;

    [ObservableProperty]
    private object _currentViewModel;

    public MainViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        // Sätt standardvyn till ProjectsViewModel när applikationen startar
        CurrentViewModel = _serviceProvider.GetRequiredService<ProjectsViewModel>();
    }
}

