//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Reflection;
//using System.CodeDom.Compiler;
//using System.IO;
//using Microsoft.CSharp;

////http://www.codemag.com/article/0211081
//namespace CSCRuleEngineBL.CodeMagazineMethod
//{
//    public interface IRemoteInterface
//    {
//        object Invoke(string lcMethod, object[] Parameters);
//    }

//    public class RemoteLoaderFactory : MarshalByRefObject
//    {
//        private const BindingFlags bfi =
//           BindingFlags.Instance | BindingFlags.Public |
//           BindingFlags.CreateInstance;
//        public IRemoteInterface Create(string assemblyFile,
//           string typeName, object[] constructArgs)
//        {
//            return (IRemoteInterface)
//               Activator.CreateInstanceFrom(
//               assemblyFile, typeName, false, bfi, null,
//               constructArgs, null, null, null).Unwrap();
//        }
//    }

//    class Program
//    {
//        // ...
//        private string lcCode = null;
//        // Create an AppDomain
//        AppDomainSetup loSetup = new AppDomainSetup();

//        public void test()
//        {
//            loSetup.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory;


//            AppDomain loAppDomain = AppDomain.CreateDomain("MyAppDomain", null, loSetup);
//            // Must create a fully functional assembly code
//            lcCode = @"using System;
//using System.IO;
//using System.Windows.Forms;
//using System.Reflection;
//using Westwind.RemoteLoader;
//namespace MyNamespace
//{
//public class MyClass : MarshalByRefObject,IRemoteInterface {
//public object Invoke(string lcMethod,object[] Parameters) {
//return this.GetType().InvokeMember(lcMethod,
//   BindingFlags.InvokeMethod,null,this,Parameters);
//}
//public object DynamicCode(params object[] Parameters) {
//" + lcCode +
//                     "} } }";

//            ICodeCompiler loCompiler = new CSharpCodeProvider().CreateCompiler();
//            CompilerParameters loParameters = new CompilerParameters();
//            // Start by adding any referenced assemblies
//            loParameters.ReferencedAssemblies.Add("System.dll");
//            loParameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
//            // Important that this gets loaded or the Interface won't work!
//            loParameters.ReferencedAssemblies.Add("Remoteloader.dll");
//            // Load the resulting assembly into memory
//            loParameters.GenerateInMemory = false;
//            loParameters.OutputAssembly = "MyNamespace.dll";
//            // Now compile the whole thing
//            CompilerResults loCompiled =
//                loCompiler.CompileAssemblyFromSource(loParameters, lcCode);
//            if (loCompiled.Errors.HasErrors)
//            {
//                // ...
//                return;
//            }

//            // create the factory class in the secondary app-domain
//            RemoteLoaderFactory factory = (RemoteLoaderFactory)loAppDomain.CreateInstance("RemoteLoader",
//                    "Westwind.RemoteLoader.RemoteLoaderFactory").Unwrap();
//            // with help of factory, create a real 'LiveClass' instance
//            object loObject = factory.Create("mynamespace.dll",
//                "MyNamespace.MyClass", null);
//            // Cast object to remote Interface, avoid loading type info
//            IRemoteInterface loRemote = (IRemoteInterface)loObject;
//            if (loObject == null)
//            {
//                //MessageBox.Show("Couldn't load class.");
//                return;
//            }
//            object[] loCodeParms = new object[1];
//            loCodeParms[0] = "West Wind Technologies";
//            try
//            {
//                // Indirectly call the remote Interface
//                object loResult = loRemote.Invoke("DynamicCode", loCodeParms);
//                DateTime ltNow = (DateTime)loResult;

//                //MessageBox.Show("Method Call Result:\r\n\"+loResult.ToString())
//            }
//            catch (Exception loError)
//            {
//                //MessageBox.Show(loError.Message, "Compiler Demo");
//            }
//            loRemote = null;
//            AppDomain.Unload(loAppDomain);
//            loAppDomain = null;
//            // Delete the generated code DLL when done
//            File.Delete("mynamespace.dll");
//        }
//    }
//}
