using System;
using UnityEngine;

namespace ZKnight.ZXMLui
{
    public class EditorText : EditorControl
    {
        public bool Editable { get; set; } = false;

        private GUIStyle _style;

        private FontStyle _fontStyle;
        private bool _richText = true;
        private int _fontSize;
        private Color _textColor = Color.gray;
        private Font _font;

        public FontStyle TextFontStyle { get { return _fontStyle; } set { _fontStyle = value; InitStyle(); } }
        public bool RichText { get { return _richText; } set { _richText = value; InitStyle(); } }
        public int FontSize { get { return _fontSize; } set { _fontSize = value; InitStyle(); } }
        public Color Color { get { return _textColor; } set { _textColor = value; InitStyle(); } }
        public Font Font { get { return _font; } set { _font = Font; InitStyle(); } }

        private Rect _drawRect;

        public Action OnContentChange;

        public EditorText()
        {
            InitStyle();
            OnPositionChange.Add(PositionChange);
            OnSizeChange.Add(OnRectChange);
            Content = "";

            OnKeyDown.Add((ec, ee) =>
            {
                if (Editable)
                {
                    ee.Use();
                }
            });
        }

        private void PositionChange(EditorControl ec)
        {
            OnRectChange(ec, LocalRect);
        }

        private void OnRectChange(EditorControl obj, Rect local)
        {
            Vector2 end = LocalPosition + Size;
            _drawRect = new Rect(Vector2.zero, end);
        }

        protected override void Draw()
        {
            base.Draw();
            if (Editable)
            {
                GUI.SetNextControlName(ID.ToString());
                string str = GUI.TextField(_drawRect, Content?.ToString(), _style);
                if (str != Content.ToString())
                {
                    Content = str;
                    OnContentChange?.Invoke();
                }
            }
            else
            {
                GUI.SetNextControlName(ID.ToString());
                GUI.Label(_drawRect, Content?.ToString(), _style);
            }
        }

        private void InitStyle()
        {
            GUIStyle style = new GUIStyle()
            {
                font = _font,
                fontStyle = _fontStyle,
                richText = true,
                fontSize = _fontSize
            };
            style.normal = new GUIStyleState()
            {
                textColor = _textColor
            };
            _style = style;
        }
    }
}
