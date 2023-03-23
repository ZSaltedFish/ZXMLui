using System.Xml;

namespace ZKnight.HTMLui
{
    public interface IDeserializeXML
    {
        object Deserialize(XmlAttribute data, IEditorControl ctrl);
    }
}
