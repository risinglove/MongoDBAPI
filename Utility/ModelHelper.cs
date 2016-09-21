using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class ModelHelper
    {

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="className">类名</param>
        /// <param name="list">字段名集合</param>
        /// <param name="para">dll存储的绝对路径</param>
        /// <param name="assemblyName">命名空间名（默认为Model）</param>
        /// <returns></returns>
        public static object CreateClass(string className, List<string> list, string para, string assemblyName = "Model")
        {
            try
            {
                var provider = new CSharpCodeProvider();
                //设置编译参数。  
                var cp = new CompilerParameters();
                cp.GenerateExecutable = false;
                cp.GenerateInMemory = true;
                cp.OutputAssembly = para + "/" + assemblyName + "_" + className + ".dll";
                // Generate debug information.
                cp.IncludeDebugInformation = true;
                // Save the assembly as a physical file.
                cp.GenerateInMemory = false;
                // Set the level at which the compiler 
                // should start displaying warnings.
                cp.WarningLevel = 3;
                // Set whether to treat all warnings as errors.
                cp.TreatWarningsAsErrors = false;
                // Set compiler argument to optimize output.
                //cp.CompilerOptions = "/optimize";
                cp.ReferencedAssemblies.Add(para + "/MongoDB.Bson.dll");
                StringBuilder classSource = new StringBuilder();
                classSource.Append("using MongoDB.Bson;\n using MongoDB.Bson.Serialization.Attributes;\n ");
                classSource.Append("namespace " + assemblyName + " {");
                classSource.Append(" public class " + className + " \n");
                classSource.Append("{\n");
                classSource.Append("[BsonId][BsonRepresentation(BsonType.ObjectId)]\n");
                classSource.Append("public string Id { get; set; }\n");
                //创建属性。  
                /*************************在这里改成需要的属性******************************/
                foreach (string value in list)
                {
                    classSource.Append("public string " + value + " { get; set; }\n");
                }
                classSource.Append("}\n}");
                System.Diagnostics.Debug.WriteLine(classSource.ToString());
                //编译代码。  
                CompilerResults result = provider.CompileAssemblyFromSource(cp, classSource.ToString());
                if (result.Errors.Count > 0)
                {
                    return null;
                }
                //获取编译后的程序集。  
                Assembly assembly = result.CompiledAssembly;
                return assembly.CreateInstance(assemblyName + "." + className);
            }
            catch (Exception e)
            {
                return null;
            }

        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="para">dll存储的绝对路径（带上dll的完整名字）</param>
        /// <returns></returns>
        public object GetModel(string para, string className, string assemblyName = "Model")
        {
            Assembly ass;
            //Type type;
            object obj = null;
            try
            {
                ass = Assembly.LoadFile(para);//要绝对路径
                //type = ass.GetType("Webtest.ReflectTest");//必须使用 名称空间+类名称
                //MethodInfo method = type.GetMethod("WriteString");//方法的名称
                obj = ass.CreateInstance(assemblyName + "." + className);//必须使用名称空间+类名称
                return obj;
            }
            catch (Exception e)
            {
                return null;
            }
        }

    }
}
