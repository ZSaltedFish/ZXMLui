using System.Collections.Generic;
using UnityEngine;

namespace ZKnight.HTMLui
{
    public interface IEditorControl
    {
        void SetParent(IEditorControl parent);
        Rect Rectangle { get; }
        List<IEditorControl> FirstChildrenList { get; }
        IEditorControl Root { get; set; }
        string Name { get; }
        void Dispose();

        void ExcuteChildAdded(IEditorControl e);
        void ExcuteChildRemove(IEditorControl e);

        EditorControl GetCore();
        string XMLNodePath { get; }
    }
}
