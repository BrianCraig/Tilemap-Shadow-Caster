using TilemapShadowCaster.Runtime;
using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace TilemapShadowCaster.Editor
{
    [CustomEditor(typeof(TilemapShadowCaster2D))]
    public class TilemapShadowCaster2DEditor : UnityEditor.Editor
    {
        private SerializedProperty m_selfShadowsProperty;
        private SerializedProperty m_ApplyToSortingLayersProperty;

        static string[] options;

        void OnEnable()
        {
            m_selfShadowsProperty = serializedObject.FindProperty("m_SelfShadows");
            m_ApplyToSortingLayersProperty = serializedObject.FindProperty("m_ApplyToSortingLayers");
            options = SortingLayer.layers.Select(l => l.name).ToArray();
        }

        public override void OnInspectorGUI()
        {
            int newVal = EditorGUILayout.MaskField("Apply to sorting layers", m_ApplyToSortingLayersProperty.intValue, options);
            serializedObject.Update();
            if (newVal != m_ApplyToSortingLayersProperty.intValue){
                m_ApplyToSortingLayersProperty.intValue = newVal;
            };

            EditorGUILayout.PropertyField(m_selfShadowsProperty, new GUIContent("Self Shadows"));
            
            if (serializedObject.hasModifiedProperties)
            {
                serializedObject.ApplyModifiedProperties();
                ((MonoBehaviour) target).GetComponent<TilemapShadowCaster2D>().ReinitializeShapes();
            }
        }
    }
}
