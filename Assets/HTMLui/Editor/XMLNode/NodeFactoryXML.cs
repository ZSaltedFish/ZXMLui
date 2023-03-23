using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using UnityEditor;
using UnityEngine;

namespace ZKnight.HTMLui
{
    public static class NodeFactoryXML
    {
        private static Dictionary<string, IDeserializeXML> _node2Desers = new Dictionary<string, IDeserializeXML>();

        public static void Init()
        {
            _node2Desers.Clear();
            Type[] types = typeof(IDeserializeXML).Assembly.GetTypes();
            foreach (Type type in types)
            {
                object[] attrs = type.GetCustomAttributes(typeof(XMLNodeAttribute), false);
                foreach (XMLNodeAttribute attr in attrs)
                {
                    IDeserializeXML xml = Activator.CreateInstance(type) as IDeserializeXML;
                    _node2Desers[attr.NodeType] = xml;
                }
            }
        }

        public static object DeserializeXML(XmlAttribute node, IEditorControl ctrl, Type srcType)
        {
            if (_node2Desers.Count == 0)
            {
                Init();
            }

            if (!_node2Desers.TryGetValue(node.Name, out IDeserializeXML xml))
            {
                return DefaultXMLNode.GetDefaultTypeData(srcType, node.Value);
            }
            return xml.Deserialize(node, ctrl);
        }

        public static void InitControl(IEditorControl ctrl)
        {
            Type type = ctrl.GetType();
            string path = ctrl.XMLNodePath;
            try
            {
                TextAsset ass = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
                if (ass == null)
                {
                    (ctrl as EditorControl)?.InitFinish();
                    return;
                }

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(ass.text);

                XmlNode root = doc.ChildNodes[0];
                TreeDeserialize(ctrl, root, ctrl);
                AttributeDeserialize(root, ctrl, ctrl);
                (ctrl as EditorControl)?.InitFinish();
            }
            catch (Exception err)
            {
                Debug.LogError($"解析{type.Name}时候错误, {err}");
            }
        }

        public static void TreeDeserialize(IEditorControl parent, XmlNode parentNode, IEditorControl root)
        {
            foreach (XmlNode node in parentNode)
            {
                try
                {
                    EditorControl subCtrl = Activator.CreateInstance(typeof(IEditorControl).Assembly.GetType(node.Name)) as EditorControl;
                    subCtrl.SetParent(parent);
                    InitControl(subCtrl);
                    AttributeDeserialize(node, subCtrl, root);

                    Type type = parent.GetType();
                    if (!string.IsNullOrEmpty(subCtrl.Name))
                    {
                        MemberInfo[] infos = type.GetMember(subCtrl.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                        if (infos.Length > 0)
                        {
                            MemberInfo info = infos[0];
                            switch (info.MemberType)
                            {
                                case MemberTypes.Field:
                                    (info as FieldInfo).SetValue(parent, subCtrl);
                                    break;
                                case MemberTypes.Property:
                                    (info as PropertyInfo).SetValue(parent, subCtrl);
                                    break;
                            }
                        }

                        TreeDeserialize(subCtrl, node, root);
                    }
                }
                catch(Exception err)
                {
                    throw new Exception($"{node.Name}", err);
                }
            }
        }

        private static void AttributeDeserialize(XmlNode node, IEditorControl ctrl, IEditorControl root)
        {
            Type type = ctrl.GetType();

            FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (FieldInfo field in fields)
            {
                XmlAttribute attr = node.Attributes[field.Name];
                if (attr == null) continue;
                object data = DeserializeXML(node.Attributes[field.Name], root, field.FieldType);
                field.SetValue(ctrl, data);
            }

            PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (PropertyInfo property in properties)
            {
                XmlAttribute attr = node.Attributes[property.Name];
                if (attr == null) continue;
                object data = DeserializeXML(node.Attributes[property.Name], root, property.PropertyType);
                property.SetValue(ctrl, data);
            }
        }

        public static T CreateEditorControl<T>() where T : IEditorControl
        {
            IEditorControl ctrl = Activator.CreateInstance<T>();
            InitControl(ctrl);
            return (T)ctrl;
        }

        public static IEditorControl CreateEditorControl(Type type)
        {
            IEditorControl ctrl = Activator.CreateInstance(type) as IEditorControl;
            InitControl(ctrl);
            return ctrl;
        }
    }
}
