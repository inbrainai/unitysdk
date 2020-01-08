using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InBrainAndroidUtils
{
	static AndroidJavaObject _activity;

	public static AndroidJavaObject Activity
	{
		get
		{
			if (_activity == null)
			{
				var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
				_activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
			}

			return _activity;
		}
	}
}