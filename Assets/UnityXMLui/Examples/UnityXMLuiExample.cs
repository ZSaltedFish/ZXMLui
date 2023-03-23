#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace ZKnight.UnityXMLui.Example
{
    public class UnityXMLuiExample : EditorControlDialog
    {
        public override string XMLNodePath => "Assets/UnityXMLui/Examples/UnityXMLuiExample.xml";

        public EditorButton TestBtn;

        public override void Start()
        {
            TestBtn.OnBtnClick += TestBtn_OnBtnClick;
        }

        private void TestBtn_OnBtnClick(EditorButton obj)
        {
            Debug.Log("hahaha");
        }

        [MenuItem("UnityXMLui/Example UI")]
        public static void Init()
        {
            var window = GetWindow<UnityXMLuiExample>();
            window.Show();
        }
    }
}
#endif
