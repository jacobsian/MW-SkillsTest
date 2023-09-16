using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillsTest.Lib
{
    public interface IStudentAPI
    {
        Student? GetById(int id);

        IEnumerable<Student> GetAllStudents(Func<IEnumerable<Student>, IEnumerable<Student>> filterFunction);

        IEnumerable<Student> GetAllStudents(string course, Func<IEnumerable<Student>, IEnumerable<Student>>? filterFunction = null);

        void UpdateStudent(Student student);

        void AddStudent(Student student);

        void DeleteStudent(int id);
    }

}
