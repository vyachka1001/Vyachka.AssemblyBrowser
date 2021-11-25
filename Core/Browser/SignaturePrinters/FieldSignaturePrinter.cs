using System.Reflection;
using System.Text;

namespace Core.Browser.SignaturePrinters
{
    public class FieldSignaturePrinter : ISignaturePrinter
    {
        public bool CanPrint(MemberInfo info)
        {
            return info is FieldInfo;
        }

        public string Print(MemberInfo info)
        {
            var fieldInfo = info as FieldInfo;
            var builder = new StringBuilder();
            builder.Append(fieldInfo.IsPublic ? "public " : "private ");
            builder.Append(fieldInfo.FieldType.Name);
            builder.Append(' ');
            builder.Append(fieldInfo.Name);
            return builder.ToString();
        }
    }
}