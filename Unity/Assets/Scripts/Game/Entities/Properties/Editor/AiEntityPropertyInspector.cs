using UnityEditor;

[CustomEditor(typeof(AiEntityProperty))]
public class AiEntityPropertyInspector : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		AiEntityProperty castedTarget = target as AiEntityProperty;
		if (castedTarget != null)
		{
			castedTarget.BehaviourTree.OnInspectorGUI();
		}
	}
}
