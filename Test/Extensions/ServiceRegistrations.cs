using Test.Contracts.Response;
using Test.Entities;
using Test.Mappers;
using Test.Mappers.Impl;
using Test.Repositories;
using Test.Repositories.Impl;
using Test.Services;
using Test.Services.Impl;
using Task = Test.Entities.Task;

namespace Test.Extensions;

public static class ServiceRegistrations
{
    public static IServiceCollection RegisterRequiredDependencies(this IServiceCollection services)
    {
        services.AddMappers()
                .AddRepositories()
                .AddServices();
        
        return services;
    }

    private static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.AddScoped<IMapper<Task, TaskResponseDto>, TaskMapper>();
        services.AddScoped<IMapper<TeamMember, TeamMemberResponseDto>, TeamMemberMapper>();
        
        return services;
    }
    
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<ITeamMemberRepository, TeamMemberRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        
        return services;
    }
    
    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITeamMemberService, TeamMemberService>();
        services.AddScoped<IProjectService, ProjectService>();
        
        return services;
    }
}