using System;
using System.Collections.Generic;

namespace ZKnight.ZXMLui
{
    public class EditorTab : EditorControl
    {
        public List<EditorToggle> _toggles = new List<EditorToggle>();
        public List<Action<object>> OnToggleChange = new List<Action<object>>();

        public void AddToggle(object content)
        {
            EditorToggle toggle = NodeFactoryXML.CreateEditorControl<EditorToggle>();
            _toggles.Add(toggle);

            toggle.Content = content;
            toggle.OnActiveChange.Add(OnValueChange);
        }

        private void OnValueChange(EditorControl obj)
        {
            
        }
    }
}
