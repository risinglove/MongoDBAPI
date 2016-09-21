using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class Translate
    {
        //public static ListToTranslate(this DataTable sender)
        //{
        //    var result = new List();
        //    if (string.IsNullOrWhiteSpace(sender.Namespace) || string.IsNullOrWhiteSpace(sender.TableName))
        //        throw new Exception("Namespace or TableName is NullOrWhiteSpace");
        //    var typeStr = "";
        //    foreach (DataColumn cloumn in sender.Columns)
        //    {
        //        typeStr += "    public string @2{set;get;}## ##".Replace("@2", cloumn.ColumnName);//Replace("@1", cloumn.DataType.Name).
        //    }
        //    typeStr = "namespace @1##{##  public class @2##  {##@3##  }##}".Replace("@1", sender.Namespace).Replace("@2", sender.TableName).Replace("@3", typeStr).Replace("##", "\r\n");
        //    var cr = new CSharpCodeProvider().CompileAssemblyFromSource(new CompilerParameters(new string[] { "System.dll" }), typeStr);
        //    var type = cr.CompiledAssembly.GetType(string.Format("{0}.{1}", sender.Namespace, sender.TableName));
        //    var properties = type.GetProperties();

        //    foreach (DataRow row in sender.Rows)
        //    {
        //        var dm = Activator.CreateInstance(type);
        //        foreach (DataColumn cloumn in sender.Columns)
        //        {
        //            var property = properties.FirstOrDefault(l => IsEnter(l.Name, cloumn.ColumnName));
        //            if (property != null)
        //            {
        //                property.SetValue(dm, row[cloumn].ToString());
        //            }
        //        }
        //        result.Add(dm);
        //    }
        //    return result;
        //}
    }
}
