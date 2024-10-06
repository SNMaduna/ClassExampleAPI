namespace Architecture_API.Models
{
    public interface ICourseRepository
    {
        // Course
        Task<Course[]> GetAllCourseAsync();
        Task<Course> GetCourseAsync(int courseId);
        Task<bool> SaveChangesAsync();
        Task <Course> AddCourseAsync(Course course);
        Task<Course> RemoveCourseAsync(int courseId);
        Task<Course> UpdateCourseAsync(Course course);

    }
}
