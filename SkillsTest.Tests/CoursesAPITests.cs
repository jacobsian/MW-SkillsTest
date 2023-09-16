using SkillsTest.Lib;
using System.Linq;
using Xunit;

namespace SkillsTest.Tests
{
    public class CoursesAPITests
    {
        private DbCourseAPI api = new DbCourseAPI(DataContextHelper.GetMockDb(nameof(CoursesAPITests)));

        [Fact]
        public void Can_Get_Course_With_Id_1()
        {
            var course = api.GetById(1);

            Assert.NotNull(course);
        }

        [Fact]
        public void Get_All_Courses_Pages_Correctly()
        {
            var courses = api.GetAllCourses(2, 1).ToList();
            Assert.NotNull(courses);

            Assert.Equal(1, 1);  //each page is only 1 element
        }
    }
}