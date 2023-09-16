using System;
using System.Collections.Generic;
using System.Linq;

namespace SkillsTest.Lib
{

    public class DbCourseAPI : ICourseAPI
    {
        public DataContext Db { get; set; }

        public DbCourseAPI(DataContext db)
        {
            Db = db;
        }

        public Course? GetById(int id)
        {
            if (id <=0)
            {
                throw new ArgumentOutOfRangeException("ID must be greater than 0");
            }
            return Db.Courses.Where(course => course.Id == id).SingleOrDefault();
        }

        public Course? GetByName(string name)
        {
            return Db.Courses.Where(course => course.Name.ToLower() == course.Name.ToLower()).SingleOrDefault();
        }

        public IEnumerable<Course> GetAllCourses(int page = 1, int pageSize = 50)
        {
            var items = (page-1) * pageSize;
            return Db.Courses.OrderBy(c=>c.Name).Skip(items).Take(pageSize);
        }

        public void AddCourse(string courseName)
        {
            if (Db.Courses.Any(c => c.Name == courseName))
            {
                throw new ArgumentException("Course already exists");
            }

            Db.Courses.Add(new Course()
            {
                Name = courseName
            });
            Db.SaveChanges();
        }

        public void UpdateCourse(int courseId, string courseName)
        {
            var course = GetById(courseId);

            if (Db.Courses.Any(c => c.Name == courseName))
            {
                throw new ArgumentException("Course already exists");
            }


            if (course != null)
            {
                course.Name = courseName;
                Db.SaveChanges();
            }

            throw new ArgumentException("Course not found.");
        }

        public void DeleteCourse(int courseId)
        {
            if (courseId <=0)
            {
                throw new ArgumentOutOfRangeException("ID must be greater than 0");
            }

            Db.Courses.Remove(new Course { Id = courseId });
            Db.SaveChanges();
        }
    }
}