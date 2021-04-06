using UnityEngine;
  using System.Collections;
  using System;
  using UnityEditor;
 
  [CustomPropertyDrawer(typeof(EnumFlagsAttribute))]
   public class EnumFlagsAttributeDrawer : PropertyDrawer
   {
       public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label)
       {
           _property.intValue = EditorGUI.MaskField( _position, _label, _property.intValue, _property.enumNames );
       }
   }