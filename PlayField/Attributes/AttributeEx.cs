using PlayField.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Help("This is an assembly", Version = "1.0", Description = "This Assembly Does Something.")]
namespace PlayField.Attributes
{   
    [Help("This is a Helper Attribute", Version = "2.0", Description = "This Class Does Nothing.")]
    //[Help("This is a do nothing attribute")]
    public class AttributeEx
    {
        [Obsolete("Any Text"/*, true*/)]
        static void Old() { }
         
        static void New() { }
        public static void Main(string[] args)
        {
            Old();

            HelpAttribute HelpAttr;

            string assemblyName;
            Process p = Process.GetCurrentProcess();
            assemblyName = p.ProcessName + ".exe";
            Assembly a = Assembly.LoadFrom(assemblyName);

            foreach (Attribute attribute in a.GetCustomAttributes(true))
            {
                HelpAttr = attribute as HelpAttribute;
                if(HelpAttr != null)
                {
                    Console.WriteLine($"Description of {assemblyName}: {HelpAttr.Description} - {HelpAttr.Version}");
                }
            }

            Type type = typeof(AttributeEx);

            foreach (Attribute attr in type.GetCustomAttributes())
            {
                HelpAttr = attr as HelpAttribute;
                if(attr != null)
                {
                    Console.WriteLine($"Description of {assemblyName}: {HelpAttr.Description} - {HelpAttr.Version}");
                }
            }

            //Querying Class-Method Attributes  
            foreach (MethodInfo method in type.GetMethods())
            {
                foreach (Attribute attr in method.GetCustomAttributes(true))
                {
                    HelpAttr = attr as HelpAttribute;
                    if (null != HelpAttr)
                    {
                        Console.WriteLine("Description of {0}:\n{1}",
                                          method.Name,
                                          HelpAttr.Description);
                    }
                }
            }
            //Querying Class-Field (only public) Attributes
            foreach (FieldInfo field in type.GetFields())
            {
                foreach (Attribute attr in field.GetCustomAttributes(true))
                {
                    HelpAttr = attr as HelpAttribute;
                    if (null != HelpAttr)
                    {
                        Console.WriteLine("Description of {0}:\n{1}",
                                          field.Name, HelpAttr.Description);
                    }
                }
            }

            Console.ReadLine();
        }
    }
}
