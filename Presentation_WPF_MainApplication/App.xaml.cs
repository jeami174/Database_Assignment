using System.Windows;
using Business.Interfaces;
using Business.Services;
using Data.Contexts;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation_WPF_MainApplication.ViewModels;
using Presentation_WPF_MainApplication.Views;

namespace Presentation_WPF_MainApplication;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>

public partial class App : Application
{
    private IHost _host;

    public App()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddDbContext<DataContext>(options => options.UseSqlServer(@"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Projects\\Database_Assignment\\Data\\Databases\\local_database.mdf;Integrated Security=True;Connect Timeout=30"), ServiceLifetime.Scoped);

                services.AddScoped<ICustomerContactRepository, CustomerContactRepository>();
                services.AddScoped<ICustomerRepository, CustomerRepository>();
                services.AddScoped<IProjectRepository, ProjectRepository>();
                services.AddScoped<IServiceRepository, ServiceRepository>();
                services.AddScoped<IStatusTypeRepository, StatusTypeRepository>();
                services.AddScoped<IUserRepository, UserRepository>();
                services.AddScoped<IUserRoleRepository, UserRoleRepository>();
                services.AddScoped<ICustomerContactService, CustomerContactService>();
                services.AddScoped<ICustomerService, CustomerService>();
                services.AddScoped<IProjectService, ProjectService>();
                services.AddScoped<IServiceService, ServiceService>();
                services.AddScoped<IUserRoleService, UserRoleService>();
                services.AddScoped<IUserService, UserService>();

                services.AddSingleton<MainViewModel>();
                services.AddSingleton<MainWindow>();

                services.AddSingleton<ProjectsViewModel>();
                services.AddTransient<ProjectsView>();
                services.AddTransient<ProjectAddViewModel>();
                services.AddTransient<ProjectAddView>();
                services.AddTransient<ProjectEditViewModel>();
                services.AddTransient<ProjectEditView>();
                services.AddTransient<ProjectDetailsViewModel>();
                services.AddTransient<ProjectDetailsView>();
            })
            .Build();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
}
