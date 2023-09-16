using SkillsTest.Lib;
using System;
using System.Linq;
using Xunit;

namespace SkillsTest.Tests
{
    public class StudentAPITests
    {
        [Fact]
        public void Can_Get_Student_With_Id_1()
        {
            DbStudentAPI api = new DbStudentAPI(DataContextHelper.GetMockDb(nameof(StudentAPITests)));
            var student = api.GetById(1);

            Assert.NotNull(student);
        }

        [Fact]
        public void Can_Get_All_Students()
        {
            DbStudentAPI api = new DbStudentAPI(DataContextHelper.GetMockDb(nameof(StudentAPITests)));
            var students = api.GetAllStudents().ToList();

            Assert.NotNull(students);
            Assert.NotEmpty(students);
        }

        [Theory]
        [InlineData("Advanced Basketweaving")]
        [InlineData("Math for Liberal Arts Majors")]
        [InlineData("The Cosmos: An Introduction")]
        public void Can_Filter_By_Course(string courseName)
        {
            DbStudentAPI api = new DbStudentAPI(DataContextHelper.GetMockDb(nameof(StudentAPITests)));

            var students = api.GetAllStudents(courseName).ToList();

            Assert.NotNull(students);
            Assert.NotEmpty(students);
            Assert.All(students, s => s.Courses.Any(c => c.Name.Equals(courseName)));
        }

        [Fact]
        public void Update_Requires_StudentId()
        {
            DbStudentAPI api = new DbStudentAPI(DataContextHelper.GetMockDb(nameof(StudentAPITests)));
            var student = new Student();
            Assert.Throws<ArgumentException>(() => api.UpdateStudent(student));
        }

        [Fact]
        public void Update_Student_Not_Found()
        {
            DbStudentAPI api = new DbStudentAPI(DataContextHelper.GetMockDb(nameof(StudentAPITests)));
            var student = new Student()
            {
                Id = int.MaxValue
            };
            Assert.Throws<ArgumentException>(() => api.UpdateStudent(student));
        }

        [Fact]
        public void Add_Does_Not_Accept_StudentId()
        {
            DbStudentAPI api = new DbStudentAPI(DataContextHelper.GetMockDb(nameof(StudentAPITests)));
            var student = new Student()
            {
                Id = 1
            };
            Assert.Throws<ArgumentException>(() => api.AddStudent(student));
        }

        [Fact]
        public void Delete_Student_Requires_Valid_Id()
        {
            DbStudentAPI api = new DbStudentAPI(DataContextHelper.GetMockDb(nameof(StudentAPITests)));
            Assert.Throws<ArgumentOutOfRangeException>(() => api.DeleteStudent(int.MinValue));
        }
    }
}