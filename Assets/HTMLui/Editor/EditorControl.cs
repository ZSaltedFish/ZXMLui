using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZKnight.HTMLui
{
    [Serializable]
    public abstract class EditorControl : IEditorControl, IEditorSeletable, IEditorSize
    {
        public virtual string XMLNodePath
        {
            get
            {
                return $"Assets/Editor/ControlXmls/{GetType().Name}.xml";
            }
        }

        private static long ID_GEN = 0;
        protected long ID { get; private set; }

        public EditorAnchor Anchor { set { SetAnchor(value); } }
        public Vector4 Margin { set { SetMargin(value); } get{ return _margin; } }
        public EditorControl ParentControl => Parent;

        private List<IEditorControl> _subControls = new List<IEditorControl>(); 
        private List<IEditorControl> _allChildList = new List<IEditorControl>();
        private IEditorControl _root;
        protected EditorControl Parent;
        public GUIStyle Style;
        public string Name { get; set; }
        public object Content { get; set; }
        public bool IsGroupType { get; set; } = false;
        private bool _active = true;
        public Vector2 ScrollOffset;
        public bool Active
        {
            get { return _active; }
            set
            {
                if (_active != value)
                {
                    _active = value;
                    foreach (var act in OnActiveChange)
                    {
                        act(this);
                    }
                }
            }
        }

        public EditorControl()
        {
            ID = ++ID_GEN;
        }

        public bool IsFocus { get { return GUI.GetNameOfFocusedControl() == ID.ToString(); } }

        public void SetFocus()
        {
            GUI.FocusControl(ID.ToString());
        }

        public EditorPopupMenu Context;

        public bool HierarchyActive
        {
            get
            {
                if (!(Parent is EditorControl ec) || !Active)
                {
                    return Active;
                }
                else
                {
                    return ec.HierarchyActive;
                }
            }
        }
        public List<IEditorControl> ChildrenList
        {
            get
            {
                return _allChildList;
            }
        }
        public List<IEditorControl> FirstChildrenList => _subControls;
        public IEditorControl Root
        {
            get
            {
                return _root;
            }
            set { _root = value; }
        }
        #region 响应事件定义
        public List<Action<IEditorControl>> OnChildAdd = new List<Action<IEditorControl>>();
        public List<Action<IEditorControl>> OnChildRemove = new List<Action<IEditorControl>>();
        public List<Action<EditorControl>> OnActiveChange = new List<Action<EditorControl>>();
        public List<Action<EditorControl, Rect>> OnSizeChange = new List<Action<EditorControl, Rect>>();
        public List<Action<EditorControl>> OnDispose = new List<Action<EditorControl>>();
        public List<Action<EditorControl>> OnPositionChange = new List<Action<EditorControl>>();
        public List<Action<EditorControl, EditorEvent>> OnMouseClick = new List<Action<EditorControl, EditorEvent>>();
        public List<Action<EditorControl, EditorEvent>> OnRightClick = new List<Action<EditorControl, EditorEvent>>();
        public List<Action<EditorControl, EditorEvent>> OnMouseIn = new List<Action<EditorControl, EditorEvent>>();
        public List<Action<EditorControl, EditorEvent>> OnMouseLeft = new List<Action<EditorControl, EditorEvent>>();
        public List<Action<EditorControl, EditorEvent>> OnMouseMove = new List<Action<EditorControl, EditorEvent>>();
        public List<Action<EditorControl, EditorEvent>> OnMouseDrag = new List<Action<EditorControl, EditorEvent>>();

        public List<Action<EditorControl, EditorEvent>> OnMouseDown = new List<Action<EditorControl, EditorEvent>>();
        public List<Action<EditorControl, EditorEvent>> OnMouseUp = new List<Action<EditorControl, EditorEvent>>();
        public List<Action<EditorControl, EditorEvent>> OnKeyDown = new List<Action<EditorControl, EditorEvent>>();

        public List<Action<EditorControl, EditorEvent>> OnDragStart = new List<Action<EditorControl, EditorEvent>>();
        public List<Action<EditorControl, EditorEvent, IEditorSeletable>> OnDragOut = new List<Action<EditorControl, EditorEvent, IEditorSeletable>>();
        public List<Action<EditorControl, EditorEvent, IEditorSeletable>> OnDragIn = new List<Action<EditorControl, EditorEvent, IEditorSeletable>>();
        #endregion

        #region 位置与大小
        private Rect _localRect;
        private bool _isAnchor;
        private byte _anchorType = (byte)(EditorAnchor.Top | EditorAnchor.Left);
        public Rect Rect
        {
            get { return Rectangle; }
            set
            {
                LocalPosition = value.position;
                Size = value.size;
            }
        }

        public Rect LocalRect
        {
            get { return _localRect; }
        }
        public Rect Rectangle
        {
            get
            {
                if (Parent == null)
                {
                    return LocalRect;
                }
                return new Rect(Parent.Rectangle.position + LocalRect.position, LocalRect.size);
            }
        }

        public Vector2 LocalPosition
        {
            get { return LocalRect.position; }
            set { RePos(value); }
        }

        public Vector2 RootPosition
        {
            get
            {
                if (Parent != null)
                {
                    return LocalPosition + Parent.RootPosition;
                }
                else
                {
                    return LocalPosition;
                }
            }
        }

        public Vector2 Size
        {
            get { return LocalRect.size; }
            set { ReSize(value); }
        }

        public void ReSize(Vector2 size)
        {
            if (size == _localRect.size)
            {
                return;
            }
            Rect oldRect = _localRect;
            _localRect.size = size;
            _selfRect = new Rect(LocalPosition, size);
            foreach (var act in OnSizeChange)
            {
                act(this, oldRect);
            }
        }

        public void RePos(Vector2 pos)
        {
            _localRect.position = pos;
            foreach (var act in OnPositionChange)
            {
                act(this);
            }
        }

        private Rect _selfRect;
        private Vector4 _margin;

        public Rect SelfRect
        {
            get
            {
                if (_selfRect == null)
                {
                    _selfRect = new Rect(Vector2.zero, Size);
                }
                return _selfRect;
            }
        }

        public void SetAnchor(EditorAnchor anchors)
        {
            _isAnchor = true;
            _anchorType = (byte)anchors;

            Parent?.OnSizeChange.Add(ResetMarginSize);
        }

        public void SetMargin(Vector4 margin)
        {
            _margin = margin;
            if (!(Parent is EditorControl ec))
            {
                return;
            }
            ResetMarginSize(ec, ec.LocalRect);
        }

        private void ResetMarginSize(EditorControl parent, Rect oldLocalRect)
        {
            bool isTop = (_anchorType & (byte)EditorAnchor.Top) > 0;
            bool isDown = (_anchorType & (byte)EditorAnchor.Down) > 0;
            bool isLeft = (_anchorType & (byte)EditorAnchor.Left) > 0;
            bool isRight = (_anchorType & (byte)EditorAnchor.Right) > 0;

            float left, right, top, down;
            if (isLeft)
            {
                left = _margin.x;
                if (isRight)
                {
                    right = parent.Size.x - _margin.x - _margin.z;
                }
                else
                {
                    right = Size.x;
                }
            }   //计算左右
            else
            {
                right = Size.x;
                if (isRight)
                {
                    left = parent.Size.x - _margin.z - Size.x;
                }
                else
                {
                    left = LocalPosition.x + oldLocalRect.xMin - parent.LocalRect.xMin;
                }
            }

            if (isTop)
            {
                top = _margin.y;
                if (isDown)
                {
                    down = parent.Size.y - _margin.y - _margin.w;
                }
                else
                {
                    down = Size.y;
                }
            }   //计算上下
            else
            {
                down = Size.y;
                if (isDown)
                {
                    top = parent.Size.y - _margin.w - Size.y;
                }
                else
                {
                    top = LocalPosition.y + oldLocalRect.yMin - parent.LocalRect.yMin;
                }
            }

            LocalPosition = new Vector2(left, top);
            Size = new Vector2(right, down);
        }
        #endregion

        #region IEditorControl
        public void DrawControl()
        {
            if (!Active)
            {
                return;
            }

            if (!IsGroupType)
            {
                if (Style == null)
                {
                    GUILayout.BeginArea(LocalRect);
                }
                else
                {
                    GUILayout.BeginArea(LocalRect, Style);
                }
            }
            else
            {
                if (Style == null)
                {
                    GUI.BeginGroup(LocalRect);
                }
                else
                {
                    GUI.BeginGroup(LocalRect, Style);
                }
            }
            Draw();
            foreach (EditorControl ctrl in _subControls)
            {
                ctrl.DrawControl();
            }
            EndDraw();

            if (!IsGroupType)
            {
                GUILayout.EndArea();
            }
            else
            {
                GUI.EndGroup();
            }
        }

        public virtual void Dispose()
        {
            List<IEditorControl> ctrls = new List<IEditorControl>(FirstChildrenList);
            foreach (EditorControl ec in ctrls)
            {
                ec.Dispose();
            }
            foreach (var act in OnDispose)
            {
                act(this);
            }
            SetParent(null);
        }

        protected virtual void Draw() { }
        protected virtual void EndDraw() { }
        public void SetParent(IEditorControl parent)
        {
            Parent?.FirstChildrenList.Remove(this);
            Parent?.ExcuteChildRemove(this);
            
            if (_isAnchor)
            {
                Parent?.OnSizeChange.Remove(ResetMarginSize);
            }
            Parent = parent?.GetCore();
            Parent?.FirstChildrenList.Add(this);
            Parent?.ExcuteChildAdded(this);
            Root = Parent?.Root;
            if (_isAnchor && Parent != null)
            {
                Parent.OnSizeChange.Add(ResetMarginSize);
                ResetMarginSize(Parent, Parent.LocalRect);
            }
        }

        public void ExcuteChildAdded(IEditorControl ctrl)
        {
            foreach (var act in OnChildAdd)
            {
                act(ctrl);
            }
        }

        public void ExcuteChildRemove(IEditorControl ctrl)
        {
            foreach (var act in OnChildRemove)
            {
                act(ctrl);
            }
        }
        
        public EditorControl GetCore()
        {
            return this;
        }

        public virtual void InitFinish() { }
        #endregion

        #region IEditorSelectbale
        public bool PosIsOn(Vector2 mouse)
        {
            if (mouse.x < Rectangle.xMin - ScrollOffset.x || mouse.x > Rectangle.xMax - ScrollOffset.x)
            {
                return false;
            }

            if (mouse.y < Rectangle.yMin - ScrollOffset.y || mouse.y > Rectangle.yMax - ScrollOffset.y)
            {
                return false;
            }
            return true;
        }

        public void DragOut(IEditorSeletable ctrl, EditorEvent e)
        {
            foreach (var act in OnDragOut)
            {
                act(this, e, ctrl);
            }
        }

        public void DragIn(IEditorSeletable ctrl, EditorEvent e)
        {
            foreach (var act in OnDragIn)
            {
                act(this, e, ctrl);
            }
        }

        public void DragStart(EditorEvent e)
        {
            foreach (var act in OnDragStart)
            {
                act(this, e);
            }
        }

        public void Click(EditorEvent e)
        {
            foreach (var act in OnMouseClick)
            {
                act(this, e);
            }
        }

        public void RightClick(EditorEvent e)
        {
            foreach (var act in OnRightClick)
            {
                act(this, e);
            }
        }

        public void MouseIn(EditorEvent e)
        {
            foreach (var act in OnMouseIn)
            {
                act(this, e);
            }
        }

        public void MouseOut(EditorEvent e)
        {
            foreach (var act in OnMouseLeft)
            {
                act(this, e);
            }
        }

        public void MouseDown(EditorEvent e)
        {
            foreach (var act in OnMouseDown)
            {
                act(this, e);
            }
        }

        public void MouseUp(EditorEvent e)
        {
            foreach (var act in OnMouseUp)
            {
                act(this, e);
            }
        }

        public void KeyDown(EditorEvent e)
        {
            foreach (var act in OnKeyDown)
            {
                act(this, e);
            }
        }

        public void MouseMove(EditorEvent e)
        {
            foreach (var act in OnMouseMove)
            {
                act(this, e);
            }
        }

        public void MouseDrag(EditorEvent ee)
        {
            foreach (var act in OnMouseDrag)
            {
                act(this, ee);
            }
        }
        #endregion

        #region 其他方法
        public override string ToString()
        {
            return string.Format("{0}->{1}", Name, GetType().Name);
        }

        public T GetChild<T>(string name) where T : IEditorControl
        {
            return (T)_subControls.Find(v => v.Name == name);
        }
        #endregion
    }
}
