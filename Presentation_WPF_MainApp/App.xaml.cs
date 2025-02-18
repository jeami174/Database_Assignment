using Business.Interfaces;
using Business.Services;
using Data.Contexts;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation_WPF_MainApp.Navigation;
using Presentation_WPF_MainApp.ViewModels;
using Presentation_WPF_MainApp.Views;
using System.Windows;

namespace Presentation_WPF_MainApp
{
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    // Registrera din EF Core DbContext med en scoped lifetime.
                    services.AddDbContext<DataContext>(options =>
                    {
                        // Ange din faktiska connection string här
                        options.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Projects\\Database_Assignment\\Data\\Databases\\local_database.mdf;Integrated Security=True;Connect Timeout=30");
                    });

                    // Repositories
                    services.AddScoped<IProjectRepository, ProjectRepository>();
                    services.AddScoped<ICustomerRepository, CustomerRepository>();
                    services.AddScoped<IServiceRepository, ServiceRepository>();
                    services.AddScoped<IStatusTypeRepository, StatusTypeRepository>();
                    services.AddScoped<IUserRepository, UserRepository>();

                    // Business services (förutsatt att dessa är stateless och kan vara singeltons)
                    services.AddSingleton<IProjectService, ProjectService>();
                    services.AddSingleton<ICustomerService, CustomerService>();
                    services.AddSingleton<IServiceService, ServiceService>();
                    services.AddSingleton<IUserService, UserService>();

                    // Navigation och MVVM
                    services.AddSingleton<Interfaces.INavigation, AppNavigation>();

                    // Huvudfönster och MainViewModel
                    services.AddSingleton<MainViewModel>();
                    services.AddSingleton<MainWindow>();

                    // Projektvyer och ViewModels
                    services.AddTransient<ProjectCreateViewModel>();
                    services.AddTransient<ProjectCreateView>();

                    services.AddTransient<ProjectDetailViewModel>();
                    services.AddTransient<ProjectDetailView>();

                    //services.AddTransient<ProjectEditViewModel>();
                    //services.AddTransient<ProjectEditView>();

                    services.AddTransient<ProjectListViewModel>();
                    services.AddTransient<ProjectListView>();
                })
                .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainViewModel = _host.Services.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _host.Services.GetRequiredService<ProjectListViewModel>();

            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.DataContext = mainViewModel;
            mainWindow.Show();

            // Hämtar MainWindow och sätter dess DataContext via DI
            //var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            //mainWindow.DataContext = _host.Services.GetRequiredService<MainViewModel>();
            //mainWindow.Show();

            //base.OnStartup(e);
        }
    }
}
