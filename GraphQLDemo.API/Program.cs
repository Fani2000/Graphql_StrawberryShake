using GraphQLDemo.API.GraphQL.Queries;
using HotChocolate.Language;

var builder = WebApplication.CreateBuilder(args);

builder
    .AddGraphQL()
    .AddTypes()
    .AddQueryType();
    // .AddMutationType();

var app = builder.Build();

app.MapGraphQL();

app.RunWithGraphQLCommands(args);