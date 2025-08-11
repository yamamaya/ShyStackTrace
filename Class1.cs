using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShyStackTrace_test {
    internal class Class1 {
        internal static void Test() {
            throw new Exception( "This is a test exception." );
        }

        internal static void Test2() {
            SubComponent.Class2.Test();
        }
    }
}
