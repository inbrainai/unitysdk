using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public static class InBrain
{
	public static void Init(string clientId, string clientSecret)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			using (var inBrain = new AndroidJavaClass("com.inbrain.sdk.InBrain"))
			{
				var inBrainInst = inBrain.CallStatic<AndroidJavaObject>("getInstance");
				inBrainInst.Call("init", InBrainAndroidUtils.Activity, clientId, clientSecret);
				inBrainInst.Call("addCallback", new InBrainCallbackProxy());
			}
		}
	}

	public static void AddCallback()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			using (var inBrain = new AndroidJavaClass("com.inbrain.sdk.InBrain"))
			{
				var inBrainInst = inBrain.CallStatic<AndroidJavaObject>("getInstance");
				inBrainInst.Call("addCallback", new InBrainCallbackProxy());
			}
		}
	}

	public static void ShowSurveys()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			using (var inBrain = new AndroidJavaClass("com.inbrain.sdk.InBrain"))
			{
				var inBrainInst = inBrain.CallStatic<AndroidJavaObject>("getInstance");

				inBrainInst.Call("showSurveys", InBrainAndroidUtils.Activity);
			}
		}

#if UNITY_IOS && !UNITY_EDITOR
       _ib_ShowSurveys();
#endif
	}

#if UNITY_IOS && !DISABLE_IOS_GOOGLE_MAPS
	[DllImport("__Internal")]
	static extern void _ib_ShowSurveys();
#endif
}