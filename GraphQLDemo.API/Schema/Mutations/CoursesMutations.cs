using GraphQLDemo.API.Models.MutationTypes;
using HotChocolate.Language;

namespace GraphQLDemo.API.Schema.Mutations;

[ExtendObjectType(OperationType.Mutation)]
public class CoursesMutations
{
    private readonly List<CourseResult> _courses;

    public CoursesMutations()
    {
        _courses = new List<CourseResult>();
    }

    public CourseResult CreateCourse(CourseTypeInput courseInput)
    {
        CourseResult courseType = new CourseResult()
        {
            Id = Guid.NewGuid(),
            Name = courseInput.Name,
            Subject = courseInput.Subject,
            InstructorId = courseInput.InstructorId
        };

        _courses.Add(courseType);

        return courseType;
    }

    public CourseResult UpdateCourse(Guid id, CourseTypeInput courseInput)
    {
        CourseResult course = _courses.FirstOrDefault(c => c.Id == id);

        if(course == null)
        {
            throw new GraphQLException(new Error("Course not found.", "COURSE_NOT_FOUND"));
        }

        course.Name = courseInput.Name;
        course.Subject = courseInput.Subject;
        course.InstructorId = courseInput.InstructorId;

        return course;
    }

    public bool DeleteCourse(Guid id)
    {
        return _courses.RemoveAll(c => c.Id == id) >= 1;
    }
}