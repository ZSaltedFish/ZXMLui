using UnityEditor;
using UnityEngine;

namespace ZKnight.ZXMLui
{
    public class DemoUI : EditorControlDialog
    {
        public override string XMLNodePath => "Packages/com.zknight.zxmlui/Demo/DemoUI.xml";

        public EditorText TextInput;
        public EditorButton ClickBtn;
        
        public void OnBtnClick(EditorButton e)
        {
            Debug.Log(TextInput.Content);
        }

        public void ClickBtn_OnBtnClick(EditorButton e)
        {
            Debug.Log(TextInput.Content);
        }

        [MenuItem("ZXMLui/Demo UI")]
        public static void Init()
        {
            var window = GetWindow<DemoUI>();
            window.Show();
        }
    }
}
