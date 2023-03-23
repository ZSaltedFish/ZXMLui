using System;

namespace ZKnight.HTMLui
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
