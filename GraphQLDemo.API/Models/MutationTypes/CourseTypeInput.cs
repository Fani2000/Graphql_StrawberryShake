using GraphQLDemo.API.GraphQL.Models.QueryTypes;

namespace GraphQLDemo.API.Models.MutationTypes;

public class CourseTypeInput
{
    public string Name { get; set; }
    public Subject Subject { get; set; }
    public Guid InstructorId { get; set; }
}