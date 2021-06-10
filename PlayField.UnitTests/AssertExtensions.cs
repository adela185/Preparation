using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayField.UnitTests
{
    public static class AssertExtensions
    {
        public static void IsOfType<T>(this Assert assert, object obj)
        {
            Type type = typeof(T);
            Assert.IsInstanceOfType(obj, type);
        }
    }
}
