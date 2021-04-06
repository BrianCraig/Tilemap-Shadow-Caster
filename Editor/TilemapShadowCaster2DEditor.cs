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
        static int flags = 0;
        static string[] options;
        static int[] values;

        void OnEnable()
        {
            m_selfShadowsProperty = serializedObject.FindProperty("m_SelfShadows");
            m_ApplyToSortingLayersProperty = serializedObject.FindProperty("m_ApplyToSortingLayers");
            options = SortingLayer.layers.Select(l => l.name).ToArray();
            values = SortingLayer.layers.Select(l => l.id).ToArray();
        }

        void getLayers(){
            m_ApplyToSortingLayersProperty.ClearArray();
            List<int> sortingLayers = new List<int>();
            int propCount = 0;
            for (int i = 0; i < options.Length; i++)
            {
                int layer = 1 << i;
                if ((flags & layer) != 0)
                {
                    m_ApplyToSortingLayersProperty.arraySize ++;
                    SerializedProperty property = m_ApplyToSortingLayersProperty.GetArrayElementAtIndex(propCount);
                    property.intValue = values[propCount];
                    propCount ++;
                }
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            int newVal = EditorGUILayout.MaskField("Apply to sorting layers", flags, options);
            if (newVal != flags){
                flags = newVal;
                getLayers();
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