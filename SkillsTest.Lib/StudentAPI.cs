using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace SkillsTest.Lib
{

    public class DbStudentAPI : IStudentAPI
    {
        public DataContext Db { get; private set; }

        public DbStudentAPI(DataContext db)
        {
            Db = db;
        }

        public Student? GetById(int id)
        {
            if (id <=0)
            {
                throw new ArgumentOutOfRangeException("ID must be greater than 0");
            }
            return Db.Students.Where(student => student.Id == id).SingleOrDefault();
        }

        public IEnumerable<Student> GetAllStudents(Func<IEnumerable<Student>, IEnumerable<Student>>? filterFunction = null)
        {
            var students = Db.Students.AsEnumerable();

            if (filterFunction == null)
            {
                return students;
            }
            return filterFunction(students);
        }

        public IEnumerable<Student> GetAllStudents(string course, Func<IEnumerable<Student>, IEnumerable<Student>>? filterFunction = null)
        {
            var students =  Db.Students.Where(
                student => student.Courses.Select(c => c.Name.ToLower())
                .Contains(course.ToLower())
                );

            if (filterFunction == null)
            {
                return students;
            }
            return filterFunction(students);
        }

        public void UpdateStudent(Student updatedStudent)
        {
            if (!updatedStudent.Id.HasValue)
            {
                throw new ArgumentException("Record does not have an ID set.  Did you mean to call Add?");
            }

            var foundStudent = Db.Students.FirstOrDefault(s => s.Id == updatedStudent.Id);

            if (foundStudent is null)
            {
                throw new ArgumentException("Student not found.");
            }

            foundStudent.FirstName = updatedStudent.FirstName;
            foundStudent.LastName = updatedStudent.LastName;
            foundStudent.Email = updatedStudent.Email;
            foundStudent.Courses = new List<Course>(updatedStudent.Courses);

            Db.SaveChanges();

        }

        public void AddStudent(Student student)
        {
            if (student.Id.HasValue)
            {
                throw new ArgumentException("Record has an ID set.  Did you mean to call Update?");
            }

            if (Db.Students.Any(s => s.Email == student.Email))
            {
                throw new ArgumentException("Student is already registered");
            }

            Db.Students.Add(student);
            Db.SaveChanges();
        }

        public void DeleteStudent(int id)
        {
            if (id <=0)
            {
                throw new ArgumentOutOfRangeException("ID must be greater than 0");
            }

            Db.Students.Remove(new Student {Id = id});
            Db.SaveChanges();
        }
    }
}
