using System.Collections.Generic;
using UnityEngine;

namespace ZKnight.UnityXMLui
{
    public abstract class AEditorList : EditorControl
    {
        public EditorControl this[int x]
        {
            get { return SubCtrls[x]; }
        }
        protected List<EditorControl> SubCtrls = new List<EditorControl>();

        public int Count => SubCtrls.Count;

        public void AddItem(EditorControl ctrl)
        {
            int index = SubCtrls.Count;
            AddItem(ctrl, index);
        }

        public void AddItem(EditorControl ctrl, int index)
        {
            SubCtrls.Insert(index, ctrl);
            ctrl.OnSizeChange.Add(SubObjectSizeChange);
            ctrl.SetParent(this);
            SubObjectSizeChange(ctrl, LocalRect);
        }

        public void RemoveItem(EditorControl ctrl)
        {
            ctrl.OnSizeChange.Remove(SubObjectSizeChange);
            SubCtrls.Remove(ctrl);
            ctrl.SetParent(null);
            SubObjectSizeChange(ctrl, LocalRect);
        }

        protected abstract void SubObjectSizeChange(EditorControl obj, Rect local);

        public void Clear()
        {
            List<EditorControl> list = new List<EditorControl>(SubCtrls);
            foreach (var ctrl in list)
            {
                ctrl.SetParent(null);
                ctrl.OnSizeChange.Remove(SubObjectSizeChange);
                SubCtrls.Remove(ctrl);
            }
            Size = Vector2.zero;
        }
    }
}
