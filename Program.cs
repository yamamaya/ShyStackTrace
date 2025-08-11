namespace ShyStackTrace_test {
    internal class Program {
        static void Main( string[] args ) {
            Console.WriteLine( "ShyStackTrace Test1" );
            try {
                throw new Exception( "This is a test exception." );
            } catch ( Exception ex ) {
                string stackTrace = OaktreeLab.Utils.Debug.ShyStackTrace.GenerateShyStackTrace( ex );
                Console.WriteLine( "-------------------" );
                Console.WriteLine( ex.StackTrace );
                Console.WriteLine( "-------------------" );
                Console.WriteLine( stackTrace );
                Console.WriteLine( "-------------------" );
            }

            Console.WriteLine();
            Console.WriteLine( "ShyStackTrace Test2" );
            try {
                Class1.Test2();
            } catch ( Exception ex ) {
                string stackTrace = OaktreeLab.Utils.Debug.ShyStackTrace.GenerateShyStackTrace( ex );
                Console.WriteLine( "-------------------" );
                Console.WriteLine( ex.StackTrace );
                Console.WriteLine( "-------------------" );
                Console.WriteLine( stackTrace );
                Console.WriteLine( "-------------------" );
            }

            Console.WriteLine();
            Console.WriteLine( "ShyStackTrace Test3" );
            try {
                SubComponent.Class2.Test2();
            } catch ( Exception ex ) {
                string stackTrace = OaktreeLab.Utils.Debug.ShyStackTrace.GenerateShyStackTrace( ex );
                Console.WriteLine( "-------------------" );
                Console.WriteLine( ex.StackTrace );
                Console.WriteLine( "-------------------" );
                Console.WriteLine( stackTrace );
                Console.WriteLine( "-------------------" );
            }

            Console.WriteLine();
            Console.WriteLine( "ShyStackTrace Test4" );
            try {
                SubComponent.SubComponent2.Class3.Test2();
            } catch ( Exception ex ) {
                string stackTrace = OaktreeLab.Utils.Debug.ShyStackTrace.GenerateShyStackTrace( ex );
                Console.WriteLine( "-------------------" );
                Console.WriteLine( ex.StackTrace );
                Console.WriteLine( "-------------------" );
                Console.WriteLine( stackTrace );
                Console.WriteLine( "-------------------" );
            }
        }
    }
}
