using System.Collections.ObjectModel;
using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Presentation_WPF_MainApp.ViewModels
{
    /// <summary>
    /// ViewModel för att hantera listan av projekt.
    /// Visar övergripande information (Id, Titel, StartDate, EndDate, Customer, Status och User).
    /// </summary>
    public partial class ProjectListViewModel : ObservableObject
    {
        private readonly IProjectService _projectService;
        private readonly Interfaces.INavigation _navigation;

        [ObservableProperty]
        private ObservableCollection<ProjectDto> projects = new();

        [ObservableProperty]
        private bool isLoading;

        public ProjectListViewModel(IProjectService projectService, Interfaces.INavigation navigation)
        {
            _projectService = projectService;
            _navigation = navigation;
            // Initiera laddningen av projekt asynkront
            LoadProjectsAsync();
        }

        /// <summary>
        /// Hämtar alla projekt via business-lagret och uppdaterar ObservableCollection.
        /// </summary>
        private async void LoadProjectsAsync()
        {
            try
            {
                IsLoading = true;
                var projectList = await _projectService.GetAllProjectsAsync();

                if (projectList == null || !projectList.Any())
                {
                    Projects.Clear();
                    Console.WriteLine("Inga projekt hittades!");
                }
                else
                {
                    Console.WriteLine($"Hämtade {projectList.Count()} projekt.");
                    Projects = new ObservableCollection<ProjectDto>(projectList);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid inläsning av projekt: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        /// <summary>
        /// Navigerar till sidan för att skapa ett nytt projekt.
        /// </summary>
        [RelayCommand]
        private void GoToCreateProject()
        {
            _navigation.ShowCreateProject();
        }

        /// <summary>
        /// Navigerar till redigeringssidan för valt projekt.
        /// </summary>
        [RelayCommand]
        private void GoToEditProject(ProjectModel project)
        {
            if (project != null)
            {
                _navigation.ShowEditProject(project.Id);
            }
        }
    }
}
