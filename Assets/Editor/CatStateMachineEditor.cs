using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CatStateMachine))]
public class CatStateMachineEditor : Editor
{
	CatStateMachine catFSM;
	ECatState changeToState = ECatState.Idle;

	private void OnEnable()
	{
		catFSM = (CatStateMachine)target;
	}

	public override void OnInspectorGUI()
	{
		if (Application.isPlaying)
		{
			//Draw State Data
			GUILayout.BeginVertical();
			{
				GUILayout.Label($"Current State: {catFSM.CurrentState}");
				GUILayout.Label($"Previous State: {catFSM.PreviousState}");


				GUILayout.Space(15);
				GUILayout.BeginHorizontal();
				{
					GUILayout.FlexibleSpace();
					if (GUILayout.Button("Set State"))
					{
						catFSM.ChangeState(changeToState);
					}
					changeToState = (ECatState)EditorGUILayout.EnumPopup(changeToState);
					GUILayout.FlexibleSpace();
				}
				GUILayout.EndHorizontal();
			}
			GUILayout.EndVertical();
		}

	}

	private void OnSceneGUI()
	{

	}
}
