using UnityEngine;

namespace ZKnight.ZXMLui
{
    public class EditorPanel : EditorControl
    {
        private Vector2 _v;
        private Rect _showRect;
        private Rect _posRect;

        public EditorPanel()
        {
            OnChildAdd.Add(AddChild);
            OnChildRemove.Add(RemoveChild);
            OnSizeChange.Add(OnSizeChangeEvent);
        }

        private void OnSizeChangeEvent(EditorControl ec, Rect rect)
        {
            _posRect = new Rect(Vector2.zero, Size);
        }

        private void AddChild(IEditorControl obj)
        {
            if (!(obj is EditorControl ec))
            {
                return;
            }
            ec.OnSizeChange.Add(OnReCaluSize);
            ec.OnActiveChange.Add(OnReCaluSizeActive);
            ReCaluSize();
        }

        private void OnReCaluSizeActive(EditorControl obj)
        {
            ReCaluSize();
        }

        private void OnReCaluSize(EditorControl obj, Rect rect)
        {
            ReCaluSize();
        }

        private void RemoveChild(IEditorControl obj)
        {
            EditorControl ec = obj as EditorControl;
            if (ec == null)
            {
                return;
            }
            ec.OnSizeChange.Remove(OnReCaluSize);
            ec.OnActiveChange.Remove(OnReCaluSizeActive);
            ReCaluSize();
        }

        private void ReCaluSize()
        {
            Vector2 size = Vector2.zero;
            foreach (EditorControl child in FirstChildrenList)
            {
                if (child.Active)
                {
                    size.y += child.Size.y;
                }
            }
            _showRect.size = size;
        }

        protected override void Draw()
        {
            base.Draw();
            _v = GUI.BeginScrollView(_posRect, _v, _showRect);
            foreach (EditorControl ctrl in FirstChildrenList)
            {
                ctrl.ScrollOffset = _v;
            }
        }

        protected override void EndDraw()
        {
            GUI.EndScrollView();
            base.EndDraw();
        }
    }
}
