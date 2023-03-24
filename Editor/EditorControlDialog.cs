using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ZKnight.UnityXMLui
{
    public abstract class EditorControlDialog : EditorWindow, IEditorControl
    {
        public List<Action<IEditorControl>> OnAddChild = new List<Action<IEditorControl>>();
        public List<Action<IEditorControl>> OnRemoveChild = new List<Action<IEditorControl>>();

        public List<Action<EditorControlDialog>> OnWindowFocus = new List<Action<EditorControlDialog>>();
        public List<Action<EditorControlDialog>> OnWindowLostFocus = new List<Action<EditorControlDialog>>();

        private EditorBaseRoot _baseRoot;

        public string XmlPath = string.Empty;
        public virtual string XMLNodePath => XmlPath;
        public void Awake()
        {
            _baseRoot = NodeFactoryXML.CreateEditorControl<EditorBaseRoot>();
            _baseRoot.Root = this;
            NodeFactoryXML.InitControl(this);
            Start();
        }

        public virtual void Start() { }
        public void OnGUI()
        {
            List<IEditorControl> foreachList = new List<IEditorControl>(_baseRoot.FirstChildrenList);
            foreach (IEditorControl c in foreachList)
            {
                (c as EditorControl).DrawControl();
            }
            Event e = Event.current;
            if (e.type == EventType.Layout)
            {
                return;
            }
            RunEvent(e);
            Repaint();
        }

        private bool _isFocus = false;
        private void RunEvent(Event e)
        {
            if (focusedWindow != this)
            {
                _isFocus = false;
                return;
            }

            _baseRoot.Size = Size;
            if (!_isFocus)
            {
                _isFocus = true;
                foreach (var act in OnWindowFocus)
                {
                    act(this);
                }
            }
            Vector2 mouse = e.mousePosition;
            List<IEditorSeletable> curList = new List<IEditorSeletable>();

            for (int i = FirstChildrenList.Count - 1; i >= 0; --i)
            {
                SelectableCreate(mouse, FirstChildrenList[i] as EditorControl, curList);
            }
            
            EditorControlDragEventManager.Instance.FilterCtrl(curList, e);
        }

        private void SelectableCreate(Vector2 mouse, EditorControl root, List<IEditorSeletable> list)
        {
            if (root == null || !root.HierarchyActive || !root.PosIsOn(mouse))
            {
                return;
            }

            for (int i = root.FirstChildrenList.Count - 1; i >= 0; --i)
            {
                SelectableCreate(mouse, root.FirstChildrenList[i] as EditorControl, list);
            }

            list.Add(root);
        }

        public Vector2 Size
        {
            get { return position.size; }
        }

        public Rect Rectangle
        {
            get
            {
                return new Rect(Vector2.zero, position.size);
            }
        }

        public List<IEditorControl> FirstChildrenList
        {
            get { return _baseRoot.FirstChildrenList; }
        }

        public void ExcuteChildAdded(IEditorControl e)
        {
            foreach (var act in OnAddChild)
            {
                act(e);
            }
        }

        public void ExcuteChildRemove(IEditorControl e)
        {
            foreach (var act in OnRemoveChild)
            {
                act(e);
            }
        }

        public IEditorControl Root { get { return this; }set { } }

        public string Name
        {
            get
            {
                return "Default";
            }
        }

        public List<Action<IEditorControl>> OnDispose = new List<Action<IEditorControl>>();

        public void SetParent(IEditorControl parent)
        {
        }

        public void Dispose()
        {
            Close();
        }

        public void OnDestroy()
        {
            List<IEditorControl> ctrls = new List<IEditorControl>(FirstChildrenList);
            foreach (EditorControl ctrl in ctrls)
            {
                ctrl.Dispose();
            }
            foreach (var act in OnDispose)
            {
                act(this);
            }
        }

        public void OnEnable()
        {
            foreach (var act in OnWindowFocus)
            {
                act(this);
            }
        }

        public void OnDisable()
        {
            foreach (var act in OnWindowLostFocus)
            {
                act(this);
            }
        }

        public EditorControl GetCore()
        {
            return _baseRoot;
        }

        /// <summary>
        /// 从屏幕坐标转换到窗口坐标
        /// </summary>
        /// <param name="pos">屏幕坐标</param>
        /// <returns></returns>
        public Vector2 TransformV2FromScreen(Vector2 pos)
        {
            return pos - position.position;
        }
    }
}
