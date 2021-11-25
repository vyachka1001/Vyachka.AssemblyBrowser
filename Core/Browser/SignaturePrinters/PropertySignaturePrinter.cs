using System.Reflection;
using System.Text;

namespace Core.Browser.SignaturePrinters
{
    public class PropertySignaturePrinter : ISignaturePrinter
    {
        public bool CanPrint(MemberInfo info)
        {
            return info is PropertyInfo;
        }

        public string Print(MemberInfo info)
        {
            bool getter = false;
            bool setter = false;
            string getterModifier = "";
            string setterModifier = "";
            string type = "";
            var propertyInfo = info as PropertyInfo;
            var accessors = propertyInfo.GetAccessors(true);
            foreach (var m in accessors)
            {
                if (m.ReturnType == typeof(void))
                {
                    setter = true;
                    setterModifier = m.IsPublic ? "" : "private ";
                    type = m.GetParameters()[0].ParameterType.Name;
                }
                else
                {
                    getter = true;
                    getterModifier = m.IsPublic ? "public" : "private";
                    type = m.ReturnType.Name;
                }
            }

            return $"{(getter ? getterModifier : setterModifier)} {type} {info.Name} {(getter ? "get;" : "")} {setterModifier}{(setter ? "set; " : "")}";
        }
    }
}