using NUnit.Framework;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;

[CustomEditor(typeof(IdentifierEntityProperty))]
public class IdentifierEntityPropertyInspector : Editor
{
    public override void OnInspectorGUI()
    {
        if(m_identifierProperty == null)
        {
            m_identifierProperty = serializedObject.FindProperty(IdentifierEntityProperty.IDENTIFIER_FIELD_NAME);
        }
        m_values.Clear();
        m_values.Add("Custom");
        m_foundIndex = 0;
        foreach (FieldInfo info in typeof(IdentifierEntityProperty.Identifiers).GetFields(
        BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy))
        {
            m_values.Add((string)info.GetValue(null));
            if (m_values[m_values.Count-1] == m_identifierProperty.stringValue)
            {
                m_foundIndex = m_values.Count - 1;
            } 
        }
        m_foundIndex = EditorGUILayout.Popup(m_foundIndex, m_values.ToArray());
        if(m_foundIndex == 0)
        {
            EditorGUILayout.PropertyField(m_identifierProperty);
        }
        else
        {
            m_identifierProperty.stringValue = m_values[m_foundIndex];
        }
        serializedObject.ApplyModifiedProperties();
    }

    List<string> m_values = new List<string>();

    int m_foundIndex;
    private SerializedProperty m_identifierProperty;
}

