using UnityEngine;

namespace ZKnight.ZXMLui
{
    public enum EditorImageType
    {
        /// <summary>
        /// 平铺
        /// </summary>
        Tile,
        /// <summary>
        /// 拉伸
        /// </summary>
        Stretching,
        /// <summary>
        /// 居中
        /// </summary>
        Center
    }

    public sealed class EditorImage : EditorControl
    {
        public EditorImage()
        {
            OnSizeChange.Add(OnSizeChangeResetImageType);
        }
        private Rect _rectSize = new Rect();
        private Texture2D _bg;
        public Texture2D Bg
        {
            get { return _bg; }
            set
            {
                _bg = value;
                SetScaleMode();
            }
        }

        private Rect _bgRect;
        private Vector2 _v = Vector2.zero;
        private EditorImageType _imageScaleModel = EditorImageType.Tile;
        public EditorImageType ImageScaleMode
        {
            get { return _imageScaleModel; }
            set
            {
                _imageScaleModel = value;
                SetScaleMode();
            }
        }

        protected override void Draw()
        {
            if (Bg != null)
            {
                GUI.DrawTextureWithTexCoords(_rectSize, Bg, _bgRect);
            }
        }

        private void SetScaleMode()
        {
            if (_bg == null)
            {
                return;
            }
            switch (ImageScaleMode)
            {
                case EditorImageType.Center:
                    Vector2 size = new Vector2(_bg.width, _bg.height);
                    Vector2 start = (LocalRect.size - size) * 0.5f;
                    _bgRect = new Rect(start, size);
                    break;
                case EditorImageType.Stretching:
                    _bgRect = new Rect(Vector2.zero, Vector2.one);
                    break;
                case EditorImageType.Tile:
                    _bgRect = new Rect(0, 0, Size.x / _bg.width, Size.y / _bg.height);
                    break;
            }
        }

        private void OnSizeChangeResetImageType(EditorControl obj, Rect local)
        {
            _rectSize = new Rect(Vector2.zero, Size);
            SetScaleMode();
        }
    }
}
