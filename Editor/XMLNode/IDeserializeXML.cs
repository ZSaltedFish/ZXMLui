using System.Xml;

namespace ZKnight.ZXMLui
{
    public interface IDeserializeXML
    {
        object Deserialize(XmlAttribute data, IEditorControl ctrl);
    }
}
