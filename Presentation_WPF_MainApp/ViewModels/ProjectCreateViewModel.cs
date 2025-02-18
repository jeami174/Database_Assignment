using System;
using System.Threading.Tasks;
using Business.Dtos;
using Business.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Presentation_WPF_MainApp.Interfaces;

namespace Presentation_WPF_MainApp.ViewModels
{
    /// <summary>
    /// ViewModel för att skapa ett nytt projekt.
    /// DTO:t (ProjectCreateDto) används som formulärdata,
    /// men när projektet har skapats returneras ett ProjectModel.
    /// </summary>
    public partial class ProjectCreateViewModel : ObservableObject
    {
        private readonly IProjectService _projectService;
        private readonly INavigation _navigation;

        // Använder ett backing field med understreck för att säkerställa att en publik property "ProjectForm" genereras.
        [ObservableProperty]
        private ProjectCreateDto _projectForm = new ProjectCreateDto();

        public ProjectCreateViewModel(IProjectService projectService, INavigation navigation)
        {
            _projectService = projectService;
            _navigation = navigation;

            // Sätt eventuella standardvärden
            ProjectForm.StartDate = DateTime.Now;
            ProjectForm.EndDate = DateTime.Now.AddDays(7);
        }

        /// <summary>
        /// Sparar det nya projektet via business-tjänsten och navigerar sedan tillbaka till projektlistan.
        /// </summary>
        [RelayCommand]
        private async Task SaveAsync()
        {
            try
            {
                // Anropar CreateProjectAsync med DTO:t.
                // Förväntar oss att metoden returnerar ett ProjectModel, vilket kan användas vid behov.
                var createdProject = await _projectService.CreateProjectAsync(ProjectForm);

                // Efter skapandet navigerar vi tillbaka till projektlistan.
                _navigation.ShowProjectList();
            }
            catch (Exception ex)
            {
                // Hantera eventuella fel, exempelvis logga eller visa ett meddelande.
                Console.WriteLine($"Fel vid skapande av projekt: {ex.Message}");
            }
        }

        /// <summary>
        /// Avbryter skapandet och navigerar tillbaka till projektlistan.
        /// </summary>
        [RelayCommand]
        private void Cancel()
        {
            _navigation.ShowProjectList();
        }
    }
}


