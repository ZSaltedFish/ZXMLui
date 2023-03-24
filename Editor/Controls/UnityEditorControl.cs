using System;

namespace ZKnight.ZXMLui
{
    public class UnityEditorControl : EditorControl
    {
        public string Tips;
        public object Value;
        public Type ValueType;

        protected override void Draw()
        {
            base.Draw();
            if (ValueType != null)
            {
                //Value = EditorDataFields.EditorDataField(Tips, Value, ValueType);
            }
        }
    }
}
