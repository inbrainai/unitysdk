using UnityEditor;
using UnityEngine;

namespace InBrain
{
	[CustomEditor(typeof(InBrainSettings))]
	public class InBrainSettingsEditor : UnityEditor.Editor
	{
		const string ClientIdTooltip = "Client id obtained by your account manager.";
		const string ClientSecretKeyTooltip = "Client secret obtained by your account manager.";
		const string S2SKeyTooltip = "Whether to enable server to server callbacks and not handle them in the application";

		[MenuItem("Window/InBrain/Edit Settings", false, 1000)]
		public static void Edit()
		{
			Selection.activeObject = InBrainSettings.Instance;
		}

		public override void OnInspectorGUI()
		{
			using (new EditorGUILayout.VerticalScope("box"))
			{
				GUILayout.Label("InBrain SDK Settings", EditorStyles.boldLabel);

				InBrainSettings.ClientId = EditorGUILayout.TextField(new GUIContent("Client ID", ClientIdTooltip), InBrainSettings.ClientId);
				InBrainSettings.ClientSecretKey = EditorGUILayout.TextField(new GUIContent("Secret key", ClientSecretKeyTooltip), InBrainSettings.ClientSecretKey);
				InBrainSettings.IsS2S = EditorGUILayout.Toggle(new GUIContent("Server To Server", S2SKeyTooltip), InBrainSettings.IsS2S);
			}

			using (new EditorGUILayout.VerticalScope("box"))
			{
				GUILayout.Label("InBrain SDK Settings (iOS only)", EditorStyles.boldLabel);

				InBrainSettings.IsServer = EditorGUILayout.Toggle("Server", InBrainSettings.IsServer);
			}
		}
	}
}