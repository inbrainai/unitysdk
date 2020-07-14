using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
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
		const bool DemoShouldUseLegacyFramework = false;

		static InBrainSettings _instance;

		[SerializeField] string clientId = DemoClientId;
		[SerializeField] string clientSecretKey = DemoClientSecretKey;
		[SerializeField] bool isServer = DemoIsServer;
		[SerializeField] bool shouldUseLegacy = DemoShouldUseLegacyFramework;

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
		/// Flag indicating whether the legacy iOS framework should be linked during the build (iOS only)
		/// </summary>
		public static bool ShouldUseLegacyFramework
		{
			get { return Instance.shouldUseLegacy; }
			set
			{
				if (Instance.shouldUseLegacy == value)
					return;
				Instance.shouldUseLegacy = value;
				MarkAssetDirty();
				ChangeMetaFiles();
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

		static void ChangeMetaFiles()
		{
#if UNITY_EDITOR
			const string frameWorkName = "InBrainSurveys_SDK_Swift.framework";
			var directoryPath = Path.Combine(Application.dataPath, "Plugins/iOS/InBrainSurveys_SDK_Swift/Editor");

			var frameworkMetaFilePath = Path.Combine(directoryPath, "Actual", $"{frameWorkName}.meta");
			var legacyFrameworkMetaFilePath = Path.Combine(directoryPath, "Legacy", $"{frameWorkName}.meta");

			var shouldUseLegacy = Instance.shouldUseLegacy;

			TryNTimes(() =>
			{
				ChangeMetafile(frameworkMetaFilePath, !shouldUseLegacy);
				ChangeMetafile(legacyFrameworkMetaFilePath, shouldUseLegacy);
			}, 20, 10);

			AssetDatabase.Refresh();
#endif
		}

		static bool TryNTimes(Action action, int n, int sleepMillis)
		{
			Exception e = null;

			while (n-- > 0)
			{
				try
				{
					action();
					return true;
				}
				catch (Exception exception)
				{
					e = exception;
					Thread.Sleep(sleepMillis);
				}
			}

			Debug.LogException(e);
			return false;
		}

		static void ChangeMetafile(string path, bool includeInBuild)
		{
			var metadataStrings = (includeInBuild ? MetadataResources.UseMetadata : MetadataResources.DoNotUseMetadata)
				.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
			var frameworkMetaStrings = File.ReadAllLines(path);
			var foundPluginImports = false;
			var resultingLines = new List<string>();
			foreach (var line in frameworkMetaStrings)
			{
				if (foundPluginImports)
				{
					break;
				}

				resultingLines.Add(line);

				if (line.Contains("PluginImporter:"))
				{
					foundPluginImports = true;
				}
			}

			resultingLines.AddRange(metadataStrings);

			File.Delete(path);
			File.WriteAllLines(path, resultingLines);
		}
	}
}