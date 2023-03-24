using System;
using System.Collections.Generic;

namespace ZKnight.ZXMLui
{
    internal static class AssemblyHelper
    {
        private static Dictionary<string, Type> _name2Type;

        public static bool TryGetType(string name, out Type type)
        {
            if (_name2Type == null)
            {
                InitAssembly();
            }
            return _name2Type.TryGetValue(name, out type);
        }

        public static void InitAssembly()
        {
            var domain = AppDomain.CurrentDomain;
            var assemblies = domain.GetAssemblies();
            var baseType = typeof(EditorControl);
            _name2Type = new Dictionary<string, Type>();
            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.IsSubclassOf(baseType))
                    {
                        _name2Type.Add(type.FullName, type);
                    }
                }
            }
        }
    }
}
