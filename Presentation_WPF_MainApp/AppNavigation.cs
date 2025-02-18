using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using Presentation_WPF_MainApp.ViewModels;

namespace Presentation_WPF_MainApp.Navigation
{
    public class AppNavigation : Interfaces.INavigation
    {
        private readonly IServiceProvider _serviceProvider;

        public AppNavigation(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Hjälpmetod för att byta nuvarande vy i MainWindow.
        /// </summary>
        private void ChangeView(ObservableObject viewModel)
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = viewModel;
        }

        public void ShowProjectList()
        {
            ChangeView(_serviceProvider.GetRequiredService<ProjectListViewModel>());
        }

        /// <summary>
        /// Navigerar asynkront till redigeringssidan för ett projekt.
        /// </summary>
        public async void ShowEditProject(int projectId)
        {
            //var projectEditViewModel = _serviceProvider.GetRequiredService<ProjectEditViewModel>();
            //await projectEditViewModel.LoadProjectAsync(projectId);
            //ChangeView(projectEditViewModel);
            ShowCreateProject();
        }

        public void ShowCreateProject()
        {
            ChangeView(_serviceProvider.GetRequiredService<ProjectCreateViewModel>());
        }
    }
}
