using UnityEngine;

namespace ZKnight.HTMLui
{
    public interface IEditorSeletable
    {
        void Click(EditorEvent e);
        void RightClick(EditorEvent e);
        bool PosIsOn(Vector2 mouse);
        void DragOut(IEditorSeletable ctrl, EditorEvent e);
        void DragIn(IEditorSeletable ctrl, EditorEvent e);
        void DragStart(EditorEvent e);
        void MouseIn(EditorEvent e);
        void MouseOut(EditorEvent e);
        void MouseUp(EditorEvent e);
        void MouseDown(EditorEvent e);
        void KeyDown(EditorEvent e);
        void MouseMove(EditorEvent ee);
        void MouseDrag(EditorEvent e);
    }
}
