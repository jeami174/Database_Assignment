using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Business.Models;
using Business.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Presentation_WPF_MainApplication.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation_WPF_MainApplication.ViewModels;

public partial class ProjectsViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IProjectService _projectService;

    public ProjectsViewModel(IServiceProvider serviceProvider, IProjectService projectService)
    {
        _serviceProvider = serviceProvider;
        _projectService = projectService;
        Projects = new ObservableCollection<ProjectModel>();

        // Ladda projekt asynkront – mappning sker i servicen via factoryn
        Task.Run(() => LoadProjectsAsync());
    }

    [ObservableProperty]
    private ObservableCollection<ProjectModel> projects;

    private async Task LoadProjectsAsync()
    {
        try
        {
            var models = await _projectService.ReadAllWithoutDetailsAsync();
            Projects = new ObservableCollection<ProjectModel>(models);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error loading projects: {ex.Message}");
        }
    }

    [RelayCommand]
    private void AddNewProject()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ProjectAddViewModel>();
    }

    [RelayCommand]
    private void EditProject(ProjectModel selectedProject)
    {
        if (selectedProject == null)
            return;

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ProjectEditViewModel>();
    }

    [RelayCommand]
    private void ToggleDetails(ProjectModel selectedProject)
    {
        if (selectedProject == null)
            return;

        // Exempel: Om du vill toggla en egenskap i ProjectModel, se till att modellen är Observable
        // selectedProject.IsDetailsVisible = !selectedProject.IsDetailsVisible;
    }
}


