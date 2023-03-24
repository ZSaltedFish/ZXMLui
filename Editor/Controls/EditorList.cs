using UnityEngine;

namespace ZKnight.ZXMLui
{
    public class EditorList : AEditorList
    {
        protected override void SubObjectSizeChange(EditorControl obj, Rect local)
        {
            float pos = 0;
            float width = 0;
            foreach (EditorControl ctrl in SubCtrls)
            {
                width = Mathf.Max(ctrl.LocalRect.width, width);
                ctrl.LocalPosition = new Vector2(0, pos);
                pos += ctrl.LocalRect.height;
            }
            Size = new Vector2(width, pos);
        }
    }
}
