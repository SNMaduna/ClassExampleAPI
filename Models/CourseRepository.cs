using Microsoft.EntityFrameworkCore;
using System;

namespace Architecture_API.Models
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext _appDbContext;

        public CourseRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        
        
        public async Task<Course[]> GetAllCourseAsync()
        {
            IQueryable<Course> query = _appDbContext.Courses;
            return await query.ToArrayAsync();
        }
        

        public async Task<Course> UpdateCourseAsync(Course course)
        {
            var updatethis = await _appDbContext.Courses.FirstOrDefaultAsync(c => c.CourseId == course.CourseId);
            
            if (updatethis == null) 
            {
                throw new InvalidOperationException($"Course not found");
            }
            
            updatethis.Name = course.Name;
            updatethis.Description = course.Description;
            updatethis.Duration = course.Duration;

            await _appDbContext.SaveChangesAsync();
            return updatethis;
        
        }

        
        public async Task<Course> RemoveCourseAsync(int courseId) 
        {
            var removeCourse = await _appDbContext.Courses.FindAsync(courseId);
            if (removeCourse == null)
            {
                
                return null;
                
            }
            _appDbContext.Courses.Remove(removeCourse);
            await _appDbContext.SaveChangesAsync();
            return removeCourse;
        }


        public async Task<Course> GetCourseAsync(int courseId)
        {
            return await _appDbContext.Courses.FirstOrDefaultAsync(c => c.CourseId == courseId);
        }


        public Task<bool> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
       
        
        public async Task<Course> AddCourseAsync(Course course)
        {
            await _appDbContext.Courses.AddAsync(course);
            await _appDbContext.SaveChangesAsync();
            return course;
        }
    }
}
