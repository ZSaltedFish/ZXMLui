using System;
using UnityEditor;
using UnityEngine;

namespace ZKnight.UnityXMLui
{
    public class EditorButton : EditorControl
    {
        private Texture2D _bg;
        private Rect _localSize;

        public event Action<EditorButton> OnBtnClick;

        private FontStyle _fontStyle = FontStyle.Normal;
        private bool _richText = true;
        private int _fontSize;
        private Texture2D _hover;
        private Texture2D _onDown;
        private Color _contentColor = Color.black;
        private Vector2 _contentOffset = new Vector2(10, 10);

        public Color ContentColor { get { return _contentColor; } set { _contentColor = value; InitStyle(); } }
        public FontStyle TextFontStyle { get { return _fontStyle; } set { _fontStyle = value; InitStyle(); } }
        public bool RichText { get { return _richText; } set { _richText = value; InitStyle(); } }
        public int FontSize { get { return _fontSize; } set { _fontSize = value; InitStyle(); } }
        public Texture2D Bg { get { return _bg; } set { _bg = value; InitStyle(); } }
        public Texture2D Hover { get { return _hover; } set { _hover = value; InitStyle(); } }
        public Texture2D Down { get { return _onDown; } set { _onDown = value; InitStyle(); } }
        public Vector2 ContentOffset { get { return _contentOffset; } set { _contentOffset = value; InitStyle(); } }

        public EditorButton()
        {
            _localSize = new Rect(Vector2.zero, Size);
            OnSizeChange.Add(OnRectChange);
            Style = "Button";
            OnMouseClick.Add((c, e) =>
            {
                e.Use();
            });
            InitStyle();
        }

        private void OnRectChange(EditorControl obj, Rect local)
        {
            _localSize = new Rect(Vector2.zero, Size);
        }

        protected override void Draw()
        {
            if (GUI.Button(_localSize, Content?.ToString(), Style))
            {
                OnBtnClick?.Invoke(this);
            }
        }

        private void InitStyle()
        {
            GUIStyle style = new GUIStyle()
            {
                fontStyle = _fontStyle,
                richText = true,
                fontSize = _fontSize,
                contentOffset = _contentOffset
            };
            style.normal = new GUIStyleState()
            {
                background = _bg,
                textColor = _contentColor
            };
            style.hover = new GUIStyleState()
            {
                background = _hover,
                textColor = Color.gray
            };
            style.active = new GUIStyleState()
            {
                background = _onDown,
                textColor = Color.gray
            };
            Style = style;
        }
    }
}
