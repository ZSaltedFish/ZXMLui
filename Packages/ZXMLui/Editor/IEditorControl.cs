using System.Collections.Generic;
using UnityEngine;

namespace ZKnight.ZXMLui
{
    public interface IEditorControl
    {
        void SetParent(IEditorControl parent);
        Rect Rectangle { get; }
        List<IEditorControl> FirstChildrenList { get; }
        IEditorControl Root { get; set; }
        string Name { get; }
        void Dispose();
        TextAsset ReferenceXML { get; }

        void ExcuteChildAdded(IEditorControl e);
        void ExcuteChildRemove(IEditorControl e);

        EditorControl GetCore();
        string XMLNodePath { get; }
    }
}
