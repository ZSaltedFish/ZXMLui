using System.Xml;

namespace ZKnight.UnityXMLui
{
    public interface IDeserializeXML
    {
        object Deserialize(XmlAttribute data, IEditorControl ctrl);
    }
}
