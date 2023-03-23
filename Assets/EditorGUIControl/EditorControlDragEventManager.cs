using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZKnight.HTMLui
{
    public class EditorControlDragEventManager
    {
        private static EditorControlDragEventManager _instance;
        public static EditorControlDragEventManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EditorControlDragEventManager();
                }
                return _instance;
            }
        }
        private IEditorSeletable _dragOutCtrl;
        public void StartDragEvent(EditorControl ec)
        {
            _dragOutCtrl = ec;
        }

        private List<IEditorSeletable> _lastFrameList = new List<IEditorSeletable>();
        private List<IEditorSeletable> _mouseDownList = new List<IEditorSeletable>();
        private EditorPopupMenu _curPopup;

        public void FilterCtrl(List<IEditorSeletable> list, Event e)
        {
            RunEvent(_lastFrameList, e, (ies, ee) =>
            {
                if (!list.Contains(ies))
                {
                    ies.MouseOut(ee);
                }
            });

            if (e.isMouse && e.type != EventType.MouseMove)
            {
                if (_curPopup != null && !list.Contains(_curPopup))
                {
                    HidePopupMenu();
                }
            }

            EditorEvent editorEvent = new EditorEvent(e);
            foreach (IEditorSeletable seletable in list)
            {
                if (editorEvent.IsUsed())
                {
                    break;
                }
                if (!_lastFrameList.Contains(seletable))
                {
                    seletable.MouseIn(editorEvent);
                }

                switch (e.type)
                {
                    case EventType.MouseDown:
                        seletable.MouseDown(editorEvent);
                        break;
                    case EventType.MouseUp:
                        seletable.MouseUp(editorEvent);
                        if (_mouseDownList.Contains(seletable))
                        {
                            if (editorEvent.Event.button == 0)
                            {
                                seletable.Click(editorEvent);
                            }
                            else if(editorEvent.Event.button == 1)
                            {
                                seletable.RightClick(editorEvent);
                                EditorPopupMenu menu = (seletable as EditorControl)?.Context;
                                if (menu != null)
                                {
                                    PopupMenu(seletable as EditorControl, menu, editorEvent.Event.mousePosition);
                                    editorEvent.Use();
                                }
                            }
                            if (_dragOutCtrl != null)
                            {
                                _dragOutCtrl.DragOut(seletable, editorEvent);
                                seletable.DragIn(_dragOutCtrl, editorEvent);
                            }
                        }
                        break;
                    case EventType.KeyUp:
                        seletable.KeyDown(editorEvent);
                        break;
                    case EventType.MouseMove:
                        seletable.MouseMove(editorEvent);
                        break;
                    case EventType.MouseDrag:
                        seletable.MouseDrag(editorEvent);
                        break;
                }
            }

            switch (e.type)
            {
                case EventType.MouseDown:
                    _mouseDownList = list;
                    break;
                case EventType.MouseUp:
                    _dragOutCtrl = null;
                    break;
            }
            _lastFrameList = list;
        }

        public void PopupMenu(EditorControl ctrl, EditorPopupMenu menu, Vector2 pos)
        {
            _curPopup?.SetParent(null);
            _curPopup = menu;
            _curPopup.SetParent(ctrl.Root);
            _curPopup.LocalPosition = pos;
        }

        public void HidePopupMenu()
        {
            _curPopup.SetParent(null);
            _curPopup = null;
        }

        private void RunEvent(List<IEditorSeletable> selectable, Event e, Action<IEditorSeletable, EditorEvent> act)
        {
            EditorEvent ev = new EditorEvent(e);
            foreach (IEditorSeletable ies in selectable)
            {
                if (ev.IsUsed())
                {
                    return;
                }

                act(ies, ev);
            }
        }
    }
}
