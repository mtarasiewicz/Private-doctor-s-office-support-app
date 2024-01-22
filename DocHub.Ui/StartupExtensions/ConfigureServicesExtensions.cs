using DocHub.Core.Domain.Entities.IdentityEntities;
using DocHub.Infrastructure.DbContext;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;
using DocHub.Core.Domain.RepositoryContracts;
using DocHub.Infrastructure.Repositories;
using DocHub.Core.ServiceContracts;
using DocHub.Core.Services;
using DocHub.Core.ValidationAttributes;

namespace DocHub.Ui.StartupExtensions
{
    /// <summary>
    ///  A class extending the services container enabling the configuration of application services
    /// </summary>
    public static class ConfigureServicesExtensions
    {
        /// <summary>
        /// Method extending the builder object for adding services to the IoC
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <param name="configuration">IConfiguration</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration) 
        {
            /*Add Controllers with views*/
            services.AddControllersWithViews(options =>
            {
                ///configure protection against XSRF attacks*/
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });
            /*Repositories*/
            services.AddScoped<IPatientsRepository, PatientsRepository>();
            services.AddScoped<IAppointmentsRepository, AppointmentsRepository>();
            /*Services*/
            services.AddScoped<IPatientsAdderService, PatientsAdderService>();
            services.AddScoped<IPatientsAdderService, PatientsAdderService>();
            services.AddScoped<IPatientsGetterService, PatientsGetterService>(); 
            services.AddScoped<IPatientsUpdaterService, PatientsUpdaterService>();
            services.AddScoped<IPatientsSorterService, PatientsSorterService>();
            services.AddScoped<IPatientsSearcherService, PatientsSearcherService>();
            services.AddScoped<IAppointmentsGetterService, AppointmentsGetterService>();
            services.AddScoped<IAppointmentsAdderService, AppointmentAdderService>();
            services.AddScoped<IAppointmentsBookerService, AppointmentsBookerService>();
            services.AddScoped<IAppointmentsAddRangeService, AppointmentsAddRangeService>();
            services.AddScoped<IAppointmentUpdaterService, AppointmentsUpdaterService>();
            /*Configure database connection*/
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = true;
                options.Password.RequiredUniqueChars = 1;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>()
                .AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>();

            services.AddAuthorization(options =>
            {
                //options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            });
            services.ConfigureApplicationCookie(options =>
            {
                //options.LoginPath = "/Account/Login";
            });
            return services;
        }
    }
}
