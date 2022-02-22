using Lamar; 

namespace ClientSuite.Web
{ 
    public class LamarRegistry : ServiceRegistry
    {
        public LamarRegistry()
        {
            Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.WithDefaultConventions();
                scanner.AssembliesAndExecutablesFromApplicationBaseDirectory(assembly =>
                    assembly.GetName().Name.StartsWith("ClientSuite."));
            });

        }
    }
}

