using UnityEditor;
using UnityEngine;

namespace UnityTools.AI.BehaviourTree
{
	[CustomPropertyDrawer(typeof(BehaviourTreePicker))]
	public class BehaviourTreePickerDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			SerializedProperty typeProperty = property.FindPropertyRelative(BehaviourTreePicker.BEHAVIOUR_TREE_FIELDNAME);
			Debug.Log(typeProperty != null ? typeProperty.ToString() : "null");
		}
	}
}
