using UnityEditor;
using UnityEngine;

namespace ZKnight.ZXMLui.Demo
{
    public class DemoUI : EditorControlDialog
    {
        public override string XMLNodePath => "Packages/com.zknight.zxmlui/Demo/DemoUI.xml";

        public EditorText TextInput;
        public EditorButton ClickBtn;

        public override void Start()
        {
        }

        public override void OnDllReloaded()
        {
            base.OnDllReloaded();
            ReloadGUI();
        }

        public void ClickBtn_OnBtnClick(EditorControl e)
        {
            Debug.Log(TextInput.Content);

        }

        //[MenuItem("ZXMLui/Demo UI")]
        public static void Init()
        {
            var window = GetWindow<DemoUI>();
            window.Show();
        }
    }
}
