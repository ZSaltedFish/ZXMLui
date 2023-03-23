using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ZKnight.HTMLui
{
    public class EditorToggle : EditorControl
    {
        public GUIStyle NormalStyle = EditorStyles.textArea;
        public GUIStyle SelectStyle = "Button";
        private bool _isOn = false;
        public bool IsOn
        {
            get { return _isOn; }
            set { _isOn = value; ChangeValue(); }
        }

        public List<Action<EditorToggle>> OnValueChange = new List<Action<EditorToggle>>();

        private void ChangeValue()
        {
            Style = _isOn ? SelectStyle : NormalStyle;
            foreach (var act in OnValueChange)
            {
                act(this);
            }
        }

        protected override void Draw()
        {
            base.Draw();
            if (Content != null)
            {
                EditorGUILayout.LabelField(Content.ToString());
            }
        }
    }
}
