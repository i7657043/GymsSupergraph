using GymSupergraph.Libs;
using HotChocolate.ApolloFederation.Types;
using HotChocolate.Execution;

namespace GymSupergraph.ManagersApi;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();

        builder.Services
            .AddGraphQLServer()
            .AddApolloFederation()
            .AddQueryType<Query>()
            .AddTypeExtension<GymType>();

        //var builder2 = WebApplication.CreateBuilder(args);
        //ISchema schema = builder2.Services
        //    .AddGraphQLServer()
        //    .AddApolloFederation()
        //    .AddQueryType<Query>()
        //    .AddTypeExtension<GymType>().BuildSchemaAsync().Result;
        //var s = schema.ToString();

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
        descriptor.ExtendsType<Gym>();

        descriptor.BindFieldsExplicitly();

        descriptor.Field(x => x.Id);

        descriptor.Key("id").ResolveReferenceWith(x => StubGym(default!));

        descriptor.Field("manager").Resolve(context =>
        {
            int gymId = context.Parent<Gym>().Id;

            return gymId == 1
                ? new Manager(2, 1, "Manager Two")
                : new Manager(1, 2, "Manager One");
        });
    }

    private static Gym StubGym(int id)
    {
        return new Gym(id, "Stub Gym");
    }
}

public class Query
{
    public Manager GetManager(int id)
    {
        return id == 1
            ? new Manager(1, 2, "Manager One")
            : new Manager(2, 1, "Manager Two");
    }

    public List<Manager> GetManagers() =>
        new List<Manager>
        {
            new Manager(1, 2, "Manager One"),
            new Manager(2, 1, "Manager Two"),
        };
}