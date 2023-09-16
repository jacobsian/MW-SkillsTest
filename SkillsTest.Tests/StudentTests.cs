using SkillsTest.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SkillsTest.Tests
{
    public class StudentTests
    {
        [Fact]
        public void Student_Email_Is_Validated()
        {
            Assert.Throws<ArgumentException>(() => {
                var student = new Student()
                {
                    Id = 1,
                    Email = "abc"
                };
            });
        }
    }
}
