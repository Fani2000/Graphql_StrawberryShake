using GraphQLDemo.API.Models.MutationTypes;
using GraphQLDemo.API.Schema.Subscribtions;
using HotChocolate.Language;
using HotChocolate.Subscriptions;

namespace GraphQLDemo.API.Schema.Mutations;

[ExtendObjectType(OperationType.Mutation)]
public class CoursesMutations
{
    private readonly List<CourseResult?> _courses = new();

    public async Task<CourseResult> CreateCourse(CourseTypeInput courseInput, [Service] ITopicEventSender topicEventSender)
    {
        CourseResult course = new CourseResult()
        {
            Id = Guid.NewGuid(),
            Name = courseInput.Name,
            Subject = courseInput.Subject,
            InstructorId = courseInput.InstructorId
        };

        _courses.Add(course);
        
        // Update/Create an event stream for the CourseCreated mutation
        await topicEventSender.SendAsync(nameof(Subscription.CourseCreated), course);

        return course;
    }

    public async Task<CourseResult> UpdateCourse(Guid id, CourseTypeInput courseInput, [Service] ITopicEventSender topicEventSender)
    {
        CourseResult course = _courses.FirstOrDefault(c => c.Id == id);

        if(course == null)
        {
            throw new GraphQLException(new Error("Course not found.", "COURSE_NOT_FOUND"));
        }

        // Auto-Mapper
        course.Name = courseInput.Name;
        course.Subject = courseInput.Subject;
        course.InstructorId = courseInput.InstructorId;

        // Update/Create an event stream for the CourseUpdated mutation
        string updateCourseTopic = $"{course.Id}_{nameof(Subscription.CourseUpdated)}";
        await topicEventSender.SendAsync(updateCourseTopic, course);

        return course;
    }

    public bool DeleteCourse(Guid id)
    {
        return _courses.RemoveAll(c => c.Id == id) >= 1;
    }
}