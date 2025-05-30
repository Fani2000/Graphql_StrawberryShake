using GraphQLDemo.API.GraphQL.Models.QueryTypes;

namespace GraphQLDemo.API.Models.MutationTypes;

public class CourseResult
{
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Subject Subject { get; set; }
        public Guid InstructorId { get; set; }
}