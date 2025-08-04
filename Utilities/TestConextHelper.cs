using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightTests.Utilities
{
    internal class TestConextHelper
    {
        public static void SetTestName([CallerMemberName] string testMethodName = "")
        {
            TestContextProvider.TestName = testMethodName;
        }
    }
}
