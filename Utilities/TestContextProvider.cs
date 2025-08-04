using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightTests.Utilities {     
    
        public static class TestContextProvider
        {
            private static readonly AsyncLocal<string> _testName = new();
            private static readonly AsyncLocal<bool> _testFailed = new();

            public static string TestName
            {
                get => _testName.Value;
                set => _testName.Value = value;
            }

            public static bool TestFailed
            {
                get => _testFailed.Value;
                set => _testFailed.Value = value;
            }
        }
    }



