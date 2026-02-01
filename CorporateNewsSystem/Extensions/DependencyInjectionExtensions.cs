using System.Reflection;

namespace Api.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            var serviceInterfaceAssembly = Assembly.Load("Domain");
            var serviceImplementationAssembly = Assembly.Load("Application");

            var serviceInterfaces = serviceInterfaceAssembly.GetTypes()
                .Where(sufInterface => sufInterface.IsInterface && sufInterface.Name.EndsWith("Service")).ToList();

            foreach (var serviceInterface in serviceInterfaces)
            {
                var implementingClass = serviceImplementationAssembly.GetTypes()
                    .FirstOrDefault(t => t.IsClass && serviceInterface.IsAssignableFrom(t));

                if (implementingClass != null) services.AddScoped(serviceInterface, implementingClass);
            }
        }

        public static void AddApplicationSingletonServices(this IServiceCollection services)
        {
            var serviceInterfaceAssembly = Assembly.Load("Domain");
            var serviceImplementationAssembly = Assembly.Load("Application");

            var serviceInterfaces = serviceInterfaceAssembly.GetTypes()
                .Where(sufInterface => sufInterface.IsInterface && sufInterface.Name.EndsWith("Singleton")).ToList();

            foreach (var serviceInterface in serviceInterfaces)
            {
                var implementingClass = serviceImplementationAssembly.GetTypes()
                    .FirstOrDefault(t => t.IsClass && serviceInterface.IsAssignableFrom(t));

                if (implementingClass != null) services.AddSingleton(serviceInterface, implementingClass);
            }
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            var serviceInterfaceAssembly = Assembly.Load("Domain");
            var serviceImplementationAssembly = Assembly.Load("Infrastructure");

            var serviceInterfaces = serviceInterfaceAssembly.GetTypes()
                .Where(sufInterface => sufInterface.IsInterface && sufInterface.Name.EndsWith("Repository")).ToList();

            foreach (var serviceInterface in serviceInterfaces)
            {
                var implementingClass = serviceImplementationAssembly.GetTypes()
                    .FirstOrDefault(t => t.IsClass && serviceInterface.IsAssignableFrom(t));

                if (implementingClass != null) services.AddScoped(serviceInterface, implementingClass);
            }
        }

        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            var serviceInterfaceAssembly = Assembly.Load("Domain");
            var serviceImplementationAssembly = Assembly.Load("Infrastructure");

            var serviceInterfaces = serviceInterfaceAssembly.GetTypes()
                .Where(sufInterface => sufInterface.IsInterface && sufInterface.Name.EndsWith("Service")).ToList();

            foreach (var serviceInterface in serviceInterfaces)
            {
                var implementingClass = serviceImplementationAssembly.GetTypes()
                    .FirstOrDefault(t => t.IsClass && serviceInterface.IsAssignableFrom(t));

                if (implementingClass != null) services.AddScoped(serviceInterface, implementingClass);
            }
        }

    }
}
