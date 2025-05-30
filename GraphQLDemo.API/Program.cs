using GraphQLDemo.API.GraphQL.Queries;
using HotChocolate.Language;

var builder = WebApplication.CreateBuilder(args);

builder
    .AddGraphQL()
    .AddTypes()
    .AddQueryType()
    .AddSubscriptionType()
    .AddInMemorySubscriptions()
    .AddMutationType();

var app = builder.Build();

app.MapGraphQL();

app.UseWebSockets();

app.RunWithGraphQLCommands(args);