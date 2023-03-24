using System;

namespace ZKnight.UnityXMLui
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
