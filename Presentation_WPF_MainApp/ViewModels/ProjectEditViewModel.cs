using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Presentation_WPF_MainApp.ViewModels
{
    public partial class ProjectEditViewModel : ObservableObject
    {
        private readonly IProjectService _projectService;
        private readonly ICustomerService _customerService;
        private readonly IServiceService _serviceService;
        private readonly IUserService _userService;
        private readonly Interfaces.INavigation _navigation;

        // Projektet som ska redigeras
        [ObservableProperty]
        private ProjectModel project = new();

        // Listor med data som kan användas för dropdowns eller liknande
        [ObservableProperty]
        private List<CustomerModel> customers = new();

        [ObservableProperty]
        private List<ServiceModel> services = new();

        [ObservableProperty]
        private List<UserModel> users = new();

        public ProjectEditViewModel(IProjectService projectService,
                                    ICustomerService customerService,
                                    IServiceService serviceService,
                                    IUserService userService,
                                    Interfaces.INavigation navigation)
        {
            _projectService = projectService;
            _customerService = customerService;
            _serviceService = serviceService;
            _userService = userService;
            _navigation = navigation;
        }

        /// <summary>
        /// Laddar projektet med angivet id och associerade data.
        /// </summary>
        public async Task LoadProjectAsync(int projectId)
        {
            // Hämtar projekt med detaljer, om null skapas ett nytt tomt objekt.
            Project = await _projectService.GetProjectWithDetailsAsync(projectId) ?? new ProjectModel();

            // Hämtar listor för dropdowns eller extra information
            Customers = new List<CustomerModel>(await _customerService.GetAllCustomersAsync());
            Services = new List<ServiceModel>(await _serviceService.GetAllServicesAsync());
            Users = new List<UserModel>(await _userService.GetAllUsersAsync());
        }

        /// <summary>
        /// Sparar de ändrade uppgifterna för projektet.
        /// </summary>
        [RelayCommand]
        public async Task SaveAsync()
        {
            try
            {
                // Här mappar vi ProjectModel till en DTO som används i update-metoden.
                // Anpassa mappningen enligt dina faktiska fält.
                var updateDto = new Business.Dtos.ProjectUpdateDto
                {
                    Title = Project.Title,
                    Description = Project.Description,
                    StartDate = Project.StartDate,
                    EndDate = Project.EndDate,
                    TotalPrice = Project.TotalPrice,
                    StatusId = Project.Status.Id,
                    UserId = Project.User.Id
                };

                await _projectService.UpdateProjectAsync(Project.Id, updateDto);
                _navigation.ShowProjectList();
            }
            catch (Exception ex)
            {
                // Hantera fel, t.ex. visa ett meddelande till användaren
                Console.WriteLine($"Fel vid sparande: {ex.Message}");
            }
        }

        /// <summary>
        /// Avbryter redigeringen och navigerar tillbaka till projektlistan.
        /// </summary>
        [RelayCommand]
        public void Cancel()
        {
            _navigation.ShowProjectList();
        }
    }
}
