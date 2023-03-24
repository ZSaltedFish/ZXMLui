using System;
using UnityEngine;

namespace ZKnight.UnityXMLui
{
    public enum EditorAnchor
    {
        Top = 1,
        Down = 2,
        Left = 4,
        Right = 8
    }

    public interface IEditorSize
    {
        void SetAnchor(EditorAnchor anchor);
        void SetMargin(Vector4 margin);
        void ReSize(Vector2 size);
        void RePos(Vector2 size);
    }
}