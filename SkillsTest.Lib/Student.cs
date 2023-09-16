using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace SkillsTest.Lib
{
    public class Student
    {
        private string email = string.Empty;

        public int? Id { get; set; }

        public String LastName { get; set; } = String.Empty;

        public String FirstName { get; set; } = String.Empty;

        public String Email
        {
            get
            {
                return email;
            } set
            {
                if (isEmailValid(value))
                {
                    email = value;
                }
                throw new ArgumentException();
            }
        }

        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

        private bool isEmailValid(string email)
        {
            //email not being set is OK
            if (String.IsNullOrEmpty(email)) return true;

            var indexAt = email.IndexOf('@');
            var indexDot = email.LastIndexOf('.');

            // Validates there is an '@', there's at least 1 character before the '@',
            // and there's a '.' in the domain
            if (indexAt > 1 && indexDot > indexAt)
            {
                return true;
            }
            
            return false;
        }
    }
}