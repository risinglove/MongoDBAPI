using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

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
        public static object CreateUserDll(string UserId, List<DataBaseModel> list, string path = "", string assemblyName = "Model")
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(UserId) && list != null && list.Count > 0)
                {
                    path = string.IsNullOrWhiteSpace(path) ? AppDomain.CurrentDomain.BaseDirectory + @"MyDLL\" + assemblyName + "_" + UserId + ".dll" : path + @"\" + assemblyName + "_" + UserId + ".dll";
                    try
                    {
                        if (File.Exists(path))
                        {
                            FileStream OpenInputStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                            OpenInputStream.Dispose();
                            //存在
                            File.SetAttributes(path, FileAttributes.Normal);
                            //计算扇区数目
                            double sectors = Math.Ceiling(new FileInfo(path).Length / 512.0);
                            // 创建一个同样大小的虚拟缓存
                            byte[] dummyBuffer = new byte[512];
                            // 创建一个加密随机数目生成器
                            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                            // 打开这个文件的FileStream
                            FileStream inputStream = new FileStream(path, FileMode.Open, FileAccess.Write, FileShare.ReadWrite);
                            //for (int currentPass = 0; currentPass < timesToWrite; currentPass++)
                            //{
                                // 文件流位置
                                inputStream.Position = 0;
                                //循环所有的扇区
                                for (int sectorsWritten = 0; sectorsWritten < sectors; sectorsWritten++)
                                {
                                    //把垃圾数据填充到流中
                                    rng.GetBytes(dummyBuffer);
                                    // 写入文件流中
                                    inputStream.Write(dummyBuffer, 0, dummyBuffer.Length);
                                }
                            //}
                            // 清空文件
                            inputStream.SetLength(0);
                            // 关闭文件流
                            inputStream.Close();
                            // 清空原始日期需要
                            DateTime dt = new DateTime(2037, 1, 1, 0, 0, 0);
                            File.SetCreationTime(path, dt);
                            File.SetLastAccessTime(path, dt);
                            File.SetLastWriteTime(path, dt);
                            File.Delete(path);
                        }
                    }
                    catch (Exception e)
                    {
                    }
                    var provider = new CSharpCodeProvider();
                    //设置编译参数。  
                    var cp = new CompilerParameters();
                    cp.GenerateExecutable = false;
                    cp.GenerateInMemory = true;
                    cp.OutputAssembly = path;
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
                    cp.ReferencedAssemblies.Add(AppDomain.CurrentDomain.BaseDirectory + "/bin/MongoDB.Bson.dll");
                    cp.ReferencedAssemblies.Add("System.dll");
                    StringBuilder classSource = new StringBuilder();
                    classSource.Append("using System;\n using MongoDB.Bson;\n using MongoDB.Bson.Serialization.Attributes;\n ");
                    classSource.Append("namespace Utility." + assemblyName + " {\n");
                    foreach (var model in list)
                    {
                        classSource.Append(" public class " + model.TableName + " \n");
                        classSource.Append("{\n");
                        classSource.Append("[BsonId]\n");
                        classSource.Append("[BsonRepresentation(BsonType.ObjectId)]\n");
                        classSource.Append("public string Id { get; set; }\n");
                        //创建属性。  
                        /*************************在这里改成需要的属性******************************/
                        foreach (var item in model.list)
                        {
                            if (!string.IsNullOrWhiteSpace(item.name))
                            {
                                switch (item.type)
                                {
                                    default:
                                    case "string":
                                        classSource.Append("public string " + item.name + " { get; set; }  \n");
                                        break;
                                    case "int":
                                        classSource.Append("public int " + item.name + " { get; set; } \n");
                                        break;
                                    case "decimal":
                                        classSource.Append("public decimal " + item.name + " { get; set; }  \n");
                                        break;
                                    case "date":
                                        classSource.Append("public DateTime " + item.name + " { get; set; }  \n");
                                        break;
                                    case "bool":
                                        classSource.Append("public bool " + item.name + " { get; set; }  \n");
                                        break;
                                }
                            }
                        }
                        classSource.Append("public DateTime CreateDate { get; set; }\n");
                        classSource.Append("}\n");
                    }
                    classSource.Append("}\n");
                    System.Diagnostics.Debug.WriteLine(classSource.ToString());
                    //编译代码。  
                    CompilerResults result = provider.CompileAssemblyFromSource(cp, classSource.ToString());
                    if (result.Errors.Count > 0)
                    {
                        return null;
                    }
                    //获取编译后的程序集。  
                    Assembly assembly = result.CompiledAssembly;
                    return assembly.CreateInstance("Utility." + assemblyName + "." + UserId);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }





        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="className">类名</param>
        /// <param name="list">字段名集合</param>
        /// <param name="para">dll存储的绝对路径</param>
        /// <param name="assemblyName">命名空间名（默认为Model）</param>
        /// <returns></returns>
        public static object CreateClass(string className, List<string> list, string assemblyName = "Model")
        {
            try
            {
                var provider = new CSharpCodeProvider();
                //设置编译参数。  
                var cp = new CompilerParameters();
                cp.GenerateExecutable = false;
                cp.GenerateInMemory = true;
                cp.OutputAssembly = AppDomain.CurrentDomain.BaseDirectory + "/MyDLL/" + assemblyName + "_" + className + ".dll";
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
                cp.ReferencedAssemblies.Add(AppDomain.CurrentDomain.BaseDirectory + "/bin/MongoDB.Bson.dll");
                StringBuilder classSource = new StringBuilder();
                classSource.Append("using MongoDB.Bson;\n using MongoDB.Bson.Serialization.Attributes;\n ");
                classSource.Append("namespace Utility." + assemblyName + " {");
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
                return assembly.CreateInstance("Utility." + assemblyName + "." + className);
            }
            catch (Exception e)
            {
                return null;
            }

        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="para">dll存储的绝对路径</param>
        /// <returns></returns>
        public static object GetModel(string className, string assemblyName = "Model", string path = "")
        {
            Assembly ass;
            //Type type;
            object obj = null;
            try
            {
                path = string.IsNullOrWhiteSpace(path) ? AppDomain.CurrentDomain.BaseDirectory + "/MyDLL/" + assemblyName + "_" + className + ".dll" : path + "/" + assemblyName + "_" + className + ".dll";
                ass = Assembly.LoadFile(path);//要绝对路径
                obj = ass.CreateInstance("Utility." + assemblyName + "." + className);//必须使用名称空间+类名称
                return obj;
            }
            catch (Exception e)
            {
                return null;
            }
        }


        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="para">dll存储的绝对路径</param>
        /// <returns></returns>
        public static object GetModel(string className, string UserId, string assemblyName = "Model", string path = "")
        {
            Assembly ass;
            //Type type;
            object obj = null;
            try
            {
                path = string.IsNullOrWhiteSpace(path) ? AppDomain.CurrentDomain.BaseDirectory + "/MyDLL/" + assemblyName + "_" + UserId + ".dll" : path + "/" + assemblyName + "_" + UserId + ".dll";
                ass = Assembly.LoadFile(path);//要绝对路径
                obj = ass.CreateInstance("Utility." + assemblyName + "." + className);//必须使用名称空间+类名称
                return obj;
            }
            catch (Exception e)
            {
                return null;
            }
        }


        /// <summary>
        /// 获取类型
        /// </summary>
        /// <param name="para"></param>
        /// <param name="className"></param>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        public static Type GetModelType(string className, string assemblyName = "Model")
        {
            Assembly ass;
            Type type;
            try
            {
                ass = Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + "/MyDLL/" + assemblyName + "_" + className + ".dll");//要绝对路径
                type = ass.GetType("Utility." + assemblyName + "." + className);//必须使用 名称空间+类名称
                                                                                //MethodInfo method = type.GetMethod("WriteString");//方法的名称
                return type;
            }
            catch (Exception e)
            {
                return null;
            }
        }

    }
}
