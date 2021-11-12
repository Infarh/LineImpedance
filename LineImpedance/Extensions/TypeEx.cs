using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.XPath;

namespace LineImpedance.Extensions
{
    public static class TypeEx
    {
        private static XDocument? GetXmlDocument(this Type type)
        {
            var assembly = type.Assembly;
            var xml_file = new FileInfo(Path.ChangeExtension(assembly.Location, ".xml"));
            if (!xml_file.Exists) return null;

            return XDocument.Load(xml_file.FullName);
        }

        public static string? GetXmlSummary(this Type type)
        {
            if (type.GetXmlDocument() is not { } xml) return null;

            var name = $"T:{type.FullName}";

            var result = ((IEnumerable)xml.XPathEvaluate($"//member[@name='{name}']/summary"))
               .Cast<XElement>()
               .FirstOrDefault();

            return (string)result;
        }

        public static string? GetDescription(this Type type) => type.GetCustomAttribute<DescriptionAttribute>()?.Description;

        public static string? GetXmlSummary(this MemberInfo Member)
        {
            var type = Member.DeclaringType;
            if (type?.GetXmlDocument() is not { } xml) return null;

            string name;

            switch (Member.MemberType)
            {
                default: throw new NotSupportedException();

                case MemberTypes.Property:
                    name = $"P:{type.FullName}.{Member.Name}";
                    break;

                case MemberTypes.Method:
                    var method = (MethodInfo)Member;
                    var parameters = method.GetParameters()
                       .Select(p => p.ParameterType.FullName);
                    name = $"M:{type.FullName}.{Member.Name}({string.Join(",", parameters)})";
                    break;

                case MemberTypes.Event:
                    name = $"E:{type.FullName}.{Member.Name}";
                    break;

                case MemberTypes.Field:
                    name = $"F:{type.FullName}.{Member.Name}";
                    break;
            }

            var result = ((IEnumerable)xml.XPathEvaluate($"//member[@name='{name}']/summary"))
               .Cast<XElement>()
               .FirstOrDefault();

            return (string)result;
        }

        public static string? GetDescription(this MemberInfo member) => member.GetCustomAttribute<DescriptionAttribute>()?.Description;

        private static string GetString(this MemberTypes type) => type switch
        {
            MemberTypes.Property => "P",
            MemberTypes.TypeInfo => "T",
            MemberTypes.Method => "M",
            _ => throw new NotSupportedException()
        };
    }
}
