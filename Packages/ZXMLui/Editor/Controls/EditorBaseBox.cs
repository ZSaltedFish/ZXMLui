using UnityEditor;

namespace ZKnight.UnityXMLui
{
    public class EditorBaseBox : EditorControl
    {
        private string _text;
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
        
        protected override void Draw()
        {
            EditorGUILayout.LabelField(Text);
        }
    }
}
