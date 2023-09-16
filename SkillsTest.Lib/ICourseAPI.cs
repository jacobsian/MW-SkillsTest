using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillsTest.Lib
{
    public interface ICourseAPI
    {
        Course? GetById(int id);

        Course? GetByName(string name);

        IEnumerable<Course> GetAllCourses(int page = 1, int pageSize = 50);

        void AddCourse(string courseName);

        void UpdateCourse(int courseId, string courseName);

        void DeleteCourse(int courseId);
    }
}
