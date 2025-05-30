using GraphQLDemo.API.GraphQL.Queries;
using GraphQLDemo.API.Services;
using HotChocolate.Language;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add the .Net db context registration
var connectionString = builder.Configuration.GetConnectionString("default");
builder.Services.AddDbContext<SchoolDbContext>(opts => opts.UseSqlite(connectionString));

// Configuring the Graphql DB Context
builder
    .AddGraphQL()
    .AddTypes()
    .AddQueryType()
    .AddSubscriptionType()
    .AddInMemorySubscriptions()
    .AddMutationType()
    .RegisterDbContextFactory<SchoolDbContext>();

var app = builder.Build();

app.MapGraphQL();

app.UseWebSockets();

app.RunWithGraphQLCommands(args);