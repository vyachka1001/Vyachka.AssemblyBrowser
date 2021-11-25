using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core.Browser.SignaturePrinters;

namespace Core.Browser
{
    public class AssemblyBrowser
    {
        private readonly Assembly assembly;
        private readonly List<ISignaturePrinter> printers;
        
        public AssemblyBrowser(string fileName)
        {
            assembly = Assembly.LoadFrom(fileName);
            printers = new List<ISignaturePrinter>
            {
                new MethodSignaturePrinter(),
                new FieldSignaturePrinter(),
                new PropertySignaturePrinter()
            };
        }

        public List<string> GetNamespaces()
        {
            return assembly.GetTypes()
                .Select(t => t.Namespace)
                .Where(n => n is not null)
                .Distinct()
                .Where(n => !n.StartsWith("System"))
                .Where(n => !n.StartsWith("Microsoft"))
                .ToList();
        }

        public List<string> GetTypes(string ns)
        {
            return assembly.GetTypes()
                .Where(t => t.Namespace is not null)
                .Where(t => t.Namespace == ns)
                .Select(t => t.Name)
                .Where(n => !n.Contains('<'))
                .ToList();
        }

        public List<string> GetMethods(string nspace, string type)
        {
            return assembly.GetTypes()
                .Where(t => t.Namespace is not null)
                .Where(t => t.Namespace == nspace)
                .First(t => t.Name == type)
                .GetMembers(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance)
                .Where(m => !m.Name.Contains('<'))
                .Select(GetSignature)
                .ToList();
        }

        public string GetSignature(MemberInfo info)
        {
            foreach (var printer in printers)
            {
                if (printer.CanPrint(info))
                {
                    return printer.Print(info);
                }
            }

            return "<unsupported member>";
        }
    }
}