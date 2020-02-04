using System.IO;
using UnityEditor;
using UnityEngine;

namespace InBrain
{
	public class InBrainSettings : ScriptableObject
	{
		const string SettingsAssetName = "InBrainSettings";
		const string SettingsAssetPath = "Resources/";
		const string SettingsAssetExtension = ".asset";

		const string DemoClientId = "9c367c28-c8a4-498d-bf22-1f3682fc73aa";
		const string DemoClientSecretKey = "90MB8WyMZyYykgs0TaR21SqCcCZz3YTTXio9FoN5o5NJ6+svp3Q2tO8pvM9CjbskCaLAog0msmVTcIigKPQw4A==";

		const bool DemoIsServer = false;
		const bool DemoIsProdEnvironment = true;

		static InBrainSettings _instance;

		[SerializeField] string clientId = DemoClientId;
		[SerializeField] string clientSecretKey = DemoClientSecretKey;
		[SerializeField] bool isServer = DemoIsServer;
		[SerializeField] bool isProdEnvironment = DemoIsProdEnvironment;

		/// <summary>
		/// Client identifier
		/// </summary>
		public static string ClientId
		{
			get { return Instance.clientId; }
			set
			{
				Instance.clientId = value;
				MarkAssetDirty();
			}
		}

		/// <summary>
		/// Secret key used for authorizing client
		/// </summary>
		public static string ClientSecretKey
		{
			get { return Instance.clientSecretKey; }
			set
			{
				Instance.clientSecretKey = value;
				MarkAssetDirty();
			}
		}

		/// <summary>
		/// Flag indicating whether server mode is enabled (iOS only)
		/// </summary>
		public static bool IsServer
		{
			get { return Instance.isServer; }
			set
			{
				Instance.isServer = value;
				MarkAssetDirty();
			}
		}

		/// <summary>
		/// Flag indicating whether production environment is enabled (iOS only)
		/// </summary>
		public static bool IsProdEnvironment
		{
			get { return Instance.isProdEnvironment; }
			set
			{
				Instance.isProdEnvironment = value;
				MarkAssetDirty();
			}
		}

		public static InBrainSettings Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = Resources.Load(SettingsAssetName) as InBrainSettings;
					{
						if (_instance == null)
						{
							_instance = CreateInstance<InBrainSettings>();
							SaveAsset(Path.Combine(GetSdkPath(), SettingsAssetPath), SettingsAssetName);
						}
					}
				}

				return _instance;
			}
		}

		static string GetSdkPath()
		{
			return GetAbsoluteSdkPath().Replace("\\", "/").Replace(Application.dataPath, "Assets");
		}

		static string GetAbsoluteSdkPath()
		{
			return Path.GetDirectoryName(Path.GetDirectoryName(FindEditor(Application.dataPath)));
		}

		static string FindEditor(string path)
		{
			foreach (var d in Directory.GetDirectories(path))
			{
				foreach (var f in Directory.GetFiles(d))
				{
					if (f.Contains("InBrainSettingsEditor.cs"))
					{
						return f;
					}
				}

				var rec = FindEditor(d);
				if (rec != null)
				{
					return rec;
				}
			}

			return null;
		}

		static void SaveAsset(string directory, string name)
		{
#if UNITY_EDITOR
			if (!Directory.Exists(directory))
			{
				Directory.CreateDirectory(directory);
			}

			AssetDatabase.CreateAsset(Instance, directory + name + SettingsAssetExtension);
			AssetDatabase.Refresh();
#endif
		}

		static void MarkAssetDirty()
		{
#if UNITY_EDITOR
			EditorUtility.SetDirty(Instance);
#endif
		}
	}
}