
using GymSupergraph.Libs;
using HotChocolate.ApolloFederation.Types;
using HotChocolate.Execution;

namespace GymSupergraph.GymApi;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();

        //var builder2 = WebApplication.CreateBuilder(args);
        //ISchema schema = builder2.Services
        //    .AddGraphQLServer()
        //    .AddApolloFederation()
        //    .AddQueryType<Query>()
        //    .AddTypeExtension<GymType>().BuildSchemaAsync().Result;
        //var s = schema.ToString();

        builder.Services
            .AddGraphQLServer()
            .AddApolloFederation()
            .AddQueryType<Query>()
            .AddType<GymType>();

        var app = builder.Build();

        app.UseHttpsRedirection();

        app.MapGraphQL();

        app.Run();
    }
}

public class GymType : ObjectType<Gym>
{
    protected override void Configure(IObjectTypeDescriptor<Gym> descriptor)
    {
        descriptor.Field(x => x.Id);

        descriptor.Key("id");
    }
}

public class Query
{
    public Gym GetGym(int id)
    {
        return id == 1
            ? new Gym(1, "Gym One")
            : new Gym(2, "Gym Two");
    }

    public List<Gym> GetGyms() =>
        new List<Gym>
        {
            new Gym(1, "Gym One"),
            new Gym(2, "Gym Two"),
        };
}