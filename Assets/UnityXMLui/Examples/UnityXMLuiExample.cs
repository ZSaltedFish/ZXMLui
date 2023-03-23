#if UNITY_EDITOR

using UnityEditor;

namespace ZKnight.UnityXMLui.Example
{
    public class UnityXMLuiExample : EditorControlDialog
    {
        public override string XMLNodePath => "Assets/UnityXMLui/Examples/UnityXMLuiExample.xml";

        [MenuItem("UnityXMLui/Example UI")]
        public static void Init()
        {
            var window = GetWindow<UnityXMLuiExample>();
            window.Show();
        }
    }
}
#endif
