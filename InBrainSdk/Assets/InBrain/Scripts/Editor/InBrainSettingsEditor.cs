using UnityEditor;
using UnityEngine;

namespace InBrain
{
	[CustomEditor(typeof(InBrainSettings))]
	public class InBrainSettingsEditor : UnityEditor.Editor
	{
		const string ClientIdTooltip = "Client id obtained by your account manager.";
		const string ClientSecretKeyTooltip = "Client secret obtained by your account manager.";
		const string AppUsedIdTooltip = "Uniques app suer identifier.";

		[MenuItem("Window/InBrain/Edit Settings", false, 1000)]

		public static void Edit()
		{
			Selection.activeObject = InBrainSettings.Instance;
		}

		public override void OnInspectorGUI()
		{
			using (new EditorGUILayout.VerticalScope("box"))
			{
				GUILayout.Label( "InBrain SDK Settings" , EditorStyles.boldLabel);

				InBrainSettings.ClientId = EditorGUILayout.TextField(new GUIContent("Client ID", ClientIdTooltip), InBrainSettings.ClientId);
				InBrainSettings.ClientSecretKey = EditorGUILayout.TextField(new GUIContent("Secret key", ClientSecretKeyTooltip), InBrainSettings.ClientSecretKey);
				InBrainSettings.AppUserId = EditorGUILayout.TextField(new GUIContent("App user ID", AppUsedIdTooltip), InBrainSettings.AppUserId);
			}
			
			using (new EditorGUILayout.VerticalScope("box"))
			{
				GUILayout.Label( "InBrain SDK Settings (iOS only)" , EditorStyles.boldLabel);

				InBrainSettings.IsServer = EditorGUILayout.Toggle("Server", InBrainSettings.IsServer);
				InBrainSettings.IsProdEnvironment = EditorGUILayout.Toggle("Production environment", InBrainSettings.IsProdEnvironment);
			}
		}
	}
}