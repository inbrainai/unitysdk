using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class JniUtils
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
	
	public static void RunOnUiThread(Action action)
	{
		Activity.Call("runOnUiThread", new AndroidJavaRunnable(action));
	}
	
	public static bool IsJavaNull(this AndroidJavaObject ajo)
	{
		return ajo == null || ajo.GetRawObject().ToInt32() == 0;
	}
}