using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ColorGame.Scripts.Utils
{
    [CreateAssetMenu(fileName = "ConfigsHolder", menuName = "ScriptableObjects/ConfigsHolder")]
    public class ConfigsHolder : ScriptableObject
    {
        [SerializeField] private List<ScriptableObject> configs;
        
        public List<ScriptableObject> Configs
        {
            get => configs;
            set => configs = value;
        }
    }
    
#if UNITY_EDITOR
    [CustomEditor(typeof(ConfigsHolder))]
    public class ConfigHolderEditor : Editor
    {
        private static readonly string Filter = $"t: {nameof(ScriptableObject)}";
        private static readonly string[] Folders = {"Assets/ColorGame"};
        private static readonly string[] Exclude = {$"{nameof(ConfigsHolder)}"};
        private static ConfigsHolder _targetClass;

        public override void OnInspectorGUI()
        {
            TryInitClass();
            
            if(GUILayout.Button("Redraw Config Instances", GUILayout.Height(40)))
            {
                _targetClass.Configs = RedrawConfigInstances();
            }
            
            GUILayout.Space(20);
            
            base.OnInspectorGUI();
        }
        
        [MenuItem("---Tools---/Open Configs Holder")]
        public static void OpenSceneProperties()
        {
            var name = $"{Exclude[0]} {Filter}";
            var scriptable = FindAssets(name, Folders).FirstOrDefault();
            EditorUtility.OpenPropertyEditor(scriptable);
        }
        
        private static List<ScriptableObject> RedrawConfigInstances()
        {
            var configs = FindAssets(Filter, Folders);

            foreach (var exclude in Exclude)
            {
                var name = $"{exclude} {Filter}";
                configs.Remove(FindAssets(name, Folders).FirstOrDefault());
            }

            return configs;
        }
        
        private static List<ScriptableObject> FindAssets(string filter, string[] folders)
        {
            return AssetDatabase.FindAssets(filter, folders)
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<ScriptableObject>)
                .ToList();
        }

        private void TryInitClass()
        {
            _targetClass ??= (ConfigsHolder)target;
        }
    }
#endif
}
