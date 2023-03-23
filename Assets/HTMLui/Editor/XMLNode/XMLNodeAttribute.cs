using System;

namespace ZKnight.HTMLui
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class XMLNodeAttribute : Attribute
    {
        public string NodeType;
        public XMLNodeAttribute(string type)
        {
            NodeType = type;
        }
    }
}
