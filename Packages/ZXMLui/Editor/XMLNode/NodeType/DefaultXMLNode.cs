using System;
using UnityEditor;
using UnityEngine;

namespace ZKnight.UnityXMLui
{
    public static class DefaultXMLNode
    {
        public static object GetDefaultTypeData(Type type, string value)
        {
            if (type == typeof(int))
            {
                return int.Parse(value);
            }
            if (type == typeof(float))
            {
                return float.Parse(value);
            }
            if (type == typeof(double))
            {
                return double.Parse(value);
            }
            if (type == typeof(Color))
            {
                float r = Convert.ToInt32(value.Substring(0, 2), 16) / (float)byte.MaxValue;
                float g = Convert.ToInt32(value.Substring(2, 2), 16) / (float)byte.MaxValue;
                float b = Convert.ToInt32(value.Substring(4, 2), 16) / (float)byte.MaxValue;
                return new Color(r, g, b, 1);
            }
            if (type == typeof(Texture2D))
            {
                Texture2D tex = AssetDatabase.LoadAssetAtPath<Texture2D>(value);
                if (tex == null)
                {
                    Debug.LogWarning($"Cannot find Asset at {value}");
                }
                return tex;
            }

            return value;
        }
    }
}
