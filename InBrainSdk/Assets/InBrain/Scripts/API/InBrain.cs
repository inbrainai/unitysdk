using System;
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

#if UNITY_IOS && !UNITY_EDITOR
       _ib_Init(clientSecret, "testing@inbrain.ai");
#endif
	}

	public static void AddCallback(Action<List<InBrainReward>> onRewardsReceived, Action onRewardsViewDismissed)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			using (var inBrain = new AndroidJavaClass("com.inbrain.sdk.InBrain"))
			{
				var inBrainInst = inBrain.CallStatic<AndroidJavaObject>("getInstance");
				inBrainInst.Call("addCallback", new InBrainCallbackProxy());
			}
		}

#if UNITY_IOS && !UNITY_EDITOR
		Action<string> onRewardsReceivedNative = rewardsJson => { Debug.Log(rewardsJson); };
       _ib_SetCallback(Callbacks.ActionStringCallback, onRewardsReceivedNative.GetPointer(), Callbacks.ActionVoidCallback, onRewardsViewDismissed.GetPointer());
#endif
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
	static extern void _ib_Init(string secret, string appId);

	[DllImport("__Internal")]
	static extern void _ib_ShowSurveys();

	[DllImport("__Internal")]
	static extern void _ib_SetCallback(Callbacks.ActionStringCallbackDelegate rewardReceivedCallback, IntPtr rewardReceivedActionPtr,
		Callbacks.ActionVoidCallbackDelegate rewardViewDismissedCallback, IntPtr rewardViewDismissedActionPtr);
#endif
}