using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ZKnight.UnityXMLui
{
    public class EditorToolBar : EditorControl
    {
        public EditorHorizontalList HorList;
        private Dictionary<EditorButton, EditorPopupMenu> _name2Context = new Dictionary<EditorButton, EditorPopupMenu>();

        public EditorToolBar()
        {
            Style = EditorStyles.toolbar;
            HorList = NodeFactoryXML.CreateEditorControl<EditorHorizontalList>();
            HorList.SetParent(this);
        }

        public void AddItem(string name, EditorPopupMenu pop)
        {
            EditorButton btn = NodeFactoryXML.CreateEditorControl<EditorButton>();
            btn.Content = name;
            btn.Style = EditorStyles.toolbarDropDown;
            btn.OnBtnClick += OnSelectBtnClick;
            btn.Size = new Vector2(100, Size.y);
            _name2Context.Add(btn, pop);
            HorList.AddItem(btn);
        }

        public void RemvoeItem(string name)
        {
            EditorButton btn = GetBtnWithName(name);
            if (btn == null)
            {
                return;
            }

            HorList.RemoveItem(btn);
            _name2Context.Remove(btn);
        }

        private EditorButton GetBtnWithName(string name)
        {
            foreach (var btn in _name2Context.Keys)
            {
                if ((string)btn.Content == name)
                {
                    return btn;
                }
            }
            return null;
        }

        private void OnSelectBtnClick(EditorButton obj)
        {
            EditorPopupMenu menu = _name2Context[obj];
            EditorControlDragEventManager.Instance.PopupMenu(this, menu, obj.RootPosition + new Vector2(0, obj.Size.y));
        }
    }
}
