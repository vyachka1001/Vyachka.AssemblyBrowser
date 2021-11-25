using System.Linq;
using System.Reflection;
using System.Text;

namespace Core.Browser.SignaturePrinters
{
    public class MethodSignaturePrinter : ISignaturePrinter
    {
        public bool CanPrint(MemberInfo info)
        {
            return info is MethodInfo;
        }

        public string Print(MemberInfo info)
        {
            var methodInfo = info as MethodInfo;
            var builder = new StringBuilder();
            builder.Append(methodInfo.IsPublic ? "public " : "private ");
            builder.Append(methodInfo.ReturnType.Name);
            builder.Append($" {methodInfo.Name}");
            var pars = methodInfo.GetParameters()
                .Select(p => p.ParameterType.Name);
            builder.Append($"({string.Join(", ", pars)})");
            return builder.ToString();
        }
    }
}