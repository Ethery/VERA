using UnityEditor;

[CustomEditor(typeof(AIEntityProperty))]
public class AiEntityPropertyInspector : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		AIEntityProperty castedTarget = target as AIEntityProperty;
		if (castedTarget != null)
		{
			castedTarget.BehaviourTree.OnInspectorGUI();
		}
	}
}
