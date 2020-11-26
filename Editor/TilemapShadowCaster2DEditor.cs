using TilemapShadowCaster.Runtime;
using UnityEditor;
using UnityEngine;

namespace TilemapShadowCaster.Editor
{
    [CustomEditor(typeof(TilemapShadowCaster2D))]
    public class TilemapShadowCaster2DEditor : UnityEditor.Editor
    {
        private SerializedProperty m_selfShadowsProperty;

        void OnEnable()
        {
            m_selfShadowsProperty = serializedObject.FindProperty("m_SelfShadows");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(m_selfShadowsProperty, new GUIContent("Self Shadows"));
            if (serializedObject.hasModifiedProperties)
            {
                serializedObject.ApplyModifiedProperties();
                ((MonoBehaviour) target).GetComponent<TilemapShadowCaster2D>().ReinitializeShapes();
            }
        }
    }
}