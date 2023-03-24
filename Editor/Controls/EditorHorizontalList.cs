using UnityEngine;

namespace ZKnight.ZXMLui
{
    public class EditorHorizontalList : AEditorList
    {
        protected override void SubObjectSizeChange(EditorControl obj, Rect local)
        {
            float pos = 0;
            float height = 0;
            foreach (EditorControl ctrl in SubCtrls)
            {
                height = Mathf.Max(ctrl.LocalRect.height, height);
                ctrl.LocalPosition = new Vector2(pos, 0);
                pos += ctrl.LocalRect.width;
            }
            Size = new Vector2(pos, height);
        }
    }
}
