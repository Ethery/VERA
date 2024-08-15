using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ScenePicker))]
public class ScenePickerDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (m_sceneIdProperty == null)
        {
            m_sceneIdProperty = property.FindPropertyRelative(ScenePicker.SCENE_PATH_FIELD_NAME);
        }
        
        if (m_scenesNames == null)
        {
            m_scenesNames = new List<string>();
        }
        else
        {
            m_scenesNames.Clear();
		}

		if (m_scenesIDs == null)
		{
			m_scenesIDs = AssetDatabase.FindAssets("t:scene");
		}

		int currentIndex = 0;
        
        for (int i = 0; i < m_scenesIDs.Length; i++)
        {
            string sceneID = m_scenesIDs[i];
            string sceneName = AssetDatabase.GUIDToAssetPath(sceneID);
            m_scenesNames.Add(sceneName);

            if (sceneName == m_sceneIdProperty.stringValue)
            {
                currentIndex = i;
            }
        }

        currentIndex = EditorGUI.Popup(position, currentIndex, m_scenesNames.ToArray());

        m_sceneIdProperty.stringValue = m_scenesNames[currentIndex];

        property.serializedObject.ApplyModifiedProperties();
    }

	string[] m_scenesIDs = null;
    List<string> m_scenesNames;
    SerializedProperty m_sceneIdProperty;
}
