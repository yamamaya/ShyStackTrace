using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShyStackTrace_test.SubComponent.SubComponent2 {
    internal class Class3 {
        internal static void Test() {
            throw new Exception( "This is a test exception." );
        }

        internal static void Test2() {
            Class1.Test();
        }
    }
}
