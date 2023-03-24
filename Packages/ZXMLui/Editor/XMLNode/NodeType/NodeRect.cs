using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;
using UnityEditor;
using UnityEngine;

namespace ZKnight.ZXMLui
{
    [XMLNode("Rect")]
    public class NodeRect : IDeserializeXML
    {
        public object Deserialize(XmlAttribute data, IEditorControl ctrl)
        {
            string rect = data.InnerText;
            if (!string.IsNullOrEmpty(rect))
            {
                Regex gex = new Regex("[0-9]+");
                float[] rectSize = new float[4];
                int index = 0;
                foreach (Match match in gex.Matches(rect))
                {
                    rectSize[index++] = float.Parse(match.Value);
                }
                return new Rect(rectSize[0], rectSize[1], rectSize[2], rectSize[3]);
            }
            return new Rect();
        }
    }

    [XMLNode("Margin")]
    public class NodeVector4 : IDeserializeXML
    {
        public object Deserialize(XmlAttribute data, IEditorControl ctrl)
        {
            string rect = data.InnerText;
            if (!string.IsNullOrEmpty(rect))
            {
                Regex gex = new Regex("[0-9]+");
                float[] rectSize = new float[4];
                int index = 0;
                foreach (Match match in gex.Matches(rect))
                {
                    rectSize[index++] = float.Parse(match.Value);
                }
                return new Vector4(rectSize[0], rectSize[1], rectSize[2], rectSize[3]);
            }
            return new Vector4();
        }
    }

    [XMLNode("Size")]
    [XMLNode("LocalPosition")]
    public class NodeVector2 : IDeserializeXML
    {
        public object Deserialize(XmlAttribute data, IEditorControl ctrl)
        {
            string v = data.InnerText;
            if (string.IsNullOrEmpty(v))
            {
                return new Vector2();
            }

            Regex gex = new Regex("[0-9]+");
            var matchs = gex.Matches(v);
            return new Vector2(float.Parse(matchs[0].Value), float.Parse(matchs[1].Value));
        }
    }

    [XMLNode("Name")]
    [XMLNode("Content")]
    [XMLNode("Tips")]
    public class NodeName : IDeserializeXML
    {
        public object Deserialize(XmlAttribute data, IEditorControl ctrl)
        {
            return data.InnerText;
        }
    }

    [XMLNode("Icon")]
    [XMLNode("Bg")]
    [XMLNode("Hover")]
    [XMLNode("Down")]
    public class NodeIcon : IDeserializeXML
    {
        public object Deserialize(XmlAttribute data, IEditorControl ctrl)
        {
            return AssetDatabase.LoadAssetAtPath<Texture2D>(data.InnerText);
        }
    }

    [XMLNode("Active")] 
    [XMLNode("IsGroupType")]
    public class NodeActive : IDeserializeXML
    {
        public object Deserialize(XmlAttribute data, IEditorControl ctrl)
        {
            return bool.Parse(data.InnerText);
        }
    }

    [XMLNode("OnBtnClick")]
    public class NodeOnBtnClick : IDeserializeXML
    {
        public object Deserialize(XmlAttribute data, IEditorControl ctrl)
        {
            Type type = ctrl.GetType();
            MethodInfo info = type.GetMethod(data.InnerText, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (info == null)
            {
                return null;
            }

            ParameterExpression p = Expression.Parameter(typeof(EditorControl));
            var cons = Expression.Constant(ctrl);
            MethodCallExpression mcExpr = Expression.Call(cons, info, p);
            Expression<Action<EditorControl>> expr = Expression.Lambda<Action<EditorControl>>(mcExpr, p);
            var v = expr.Compile();
            return v;
        }
    }

    [XMLNode("ValueType")]
    public class NodeGetType : IDeserializeXML
    {
        public object Deserialize(XmlAttribute data, IEditorControl ctrl)
        {
            string typeName = data.InnerText;
            Assembly[] asses = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var ass in asses)
            {
                Type type = ass.GetType(typeName);
                if (type != null)
                {
                    return type;
                }
            }
            return null;
        }
    }

    [XMLNode("FontSize")]
    public class IntType : IDeserializeXML
    {
        public object Deserialize(XmlAttribute data, IEditorControl ctrl)
        {
            return int.Parse(data.InnerText);
        }
    }

    [XMLNode("ImageScaleMode")]
    public class NodeImageScaleMode : IDeserializeXML
    {
        public object Deserialize(XmlAttribute data, IEditorControl ctrl)
        {
            return Enum.Parse(typeof(EditorImageType), data.InnerText);
        }
    }

    [XMLNode("Anchor")]
    public class AnchorMode : IDeserializeXML
    {
        public object Deserialize(XmlAttribute data, IEditorControl ctrl)
        {
            string[] types = data.InnerText.Replace(" ", "").Split(',');
            EditorAnchor anchor = 0;
            foreach (string type in types)
            {
                EditorAnchor anchorType = FromString<EditorAnchor>(type);
                anchor |= anchorType;
            }
            return anchor;
        }
        public static T FromString<T>(string str)
        {
            if (!Enum.IsDefined(typeof(T), str))
            {
                return default;
            }
            return (T)Enum.Parse(typeof(T), str);
        }
    }

    [XMLNode("Color")]
    public class ColorMode : IDeserializeXML
    {
        public object Deserialize(XmlAttribute data, IEditorControl ctrl)
        {
            string value = data.Value;
            float r = Convert.ToInt32(value.Substring(0, 2), 16) / (float)byte.MaxValue;
            float g = Convert.ToInt32(value.Substring(2, 2), 16) / (float)byte.MaxValue;
            float b = Convert.ToInt32(value.Substring(4, 2), 16) / (float)byte.MaxValue;
            return new Color(r, g, b);
        }
    }
}
