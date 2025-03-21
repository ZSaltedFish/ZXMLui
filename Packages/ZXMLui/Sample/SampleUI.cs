using UnityEditor;
using UnityEngine;

namespace ZKnight.ZXMLui.Demo
{
    public class SampleUI : EditorControlDialog
    {
        public override string XMLNodePath => "Packages/com.zknight.zxmlui/Sample/SampleUI.xml";

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

        [MenuItem("ZXMLui/Sample UI")]
        public static void Init()
        {
            var window = GetWindow<SampleUI>();
            window.Show();
        }
    }
}
