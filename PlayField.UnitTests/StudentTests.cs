using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;

namespace PlayField.UnitTests
{
    [TestClass]
    public class StudentTests
    {
        [TestMethod]
        public void allSubs_StudentWith3Subjects_ReturnsStringOf3Subjects()
        {
            //Arrange
            string[] subjects = { "Math", "History", "Art" };
            string result = "Math History Art ";

            //Act
            var _return = Student.allSubs(subjects);

            //Assert
            Assert.AreEqual(result, _return);
            Assert.That.IsOfType<string>(_return);
        }
    }
}
