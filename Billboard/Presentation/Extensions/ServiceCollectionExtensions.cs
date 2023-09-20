namespace Presentation.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureDbContext()
    {
        // TODO configure connection to PostgreSQL
        throw new NotImplementedException();
    }

    public static IServiceCollection ConfigureCqrs()
    {
        // TODO configure CQRS by MediatR
        throw new NotImplementedException();
    }
}