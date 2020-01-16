﻿using System;
using System.Collections.Generic;
using System.Linq;
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
				inBrainInst.Call("init", JniUtils.Activity, clientId, clientSecret);
				inBrainInst.Call("setAppUserId", "1234-1234-1234-1234");
			}
		}

#if UNITY_IOS && !UNITY_EDITOR
       _ib_Init(clientSecret, "testing@inbrain.ai");
#endif
	}

	public static void AddCallback(Action<RewardsResult> onRewardsReceived, Action onRewardsViewDismissed)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			using (var inBrain = new AndroidJavaClass("com.inbrain.sdk.InBrain"))
			{
				var inBrainInst = inBrain.CallStatic<AndroidJavaObject>("getInstance");
				inBrainInst.Call("addCallback", new InBrainCallbackProxy(onRewardsReceived, onRewardsViewDismissed));
			}
		}

#if UNITY_IOS && !UNITY_EDITOR
		Action<string> onRewardsReceivedNative = rewardsJson =>
		{
			Debug.Log("Unity rewards json: " + rewardsJson);
			var rewardsResult = JsonUtility.FromJson<RewardsResult>(rewardsJson);
			onRewardsReceived?.Invoke(rewardsResult);
		};

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

				inBrainInst.Call("showSurveys", JniUtils.Activity);
			}
		}

#if UNITY_IOS && !UNITY_EDITOR
       _ib_ShowSurveys();
#endif
	}

	public static void GetRewards()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			using (var inBrain = new AndroidJavaClass("com.inbrain.sdk.InBrain"))
			{
				var inBrainInst = inBrain.CallStatic<AndroidJavaObject>("getInstance");

				inBrainInst.Call("getRewards");
			}
		}

#if UNITY_IOS && !UNITY_EDITOR
       _ib_GetRewards();
#endif
	}

	public static void ConfirmRewards(List<InBrainReward> rewards)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			using (var inBrain = new AndroidJavaClass("com.inbrain.sdk.InBrain"))
			{
				var inBrainInst = inBrain.CallStatic<AndroidJavaObject>("getInstance");

				inBrainInst.Call("confirmRewards", rewards.ToJavaList(reward => reward.ToAJO()));
			}
		}

#if UNITY_IOS && !UNITY_EDITOR
		var rewardsIds = rewards.Select(reward => reward.transactionId).ToList();
		var rewardsJson = JsonUtility.ToJson(rewardsIds);
		_ib_ConfirmRewards(rewardsJson);
#endif
	}

#if UNITY_IOS
	[DllImport("__Internal")]
	static extern void _ib_Init(string secret, string appId);

	[DllImport("__Internal")]
	static extern void _ib_ShowSurveys();

	[DllImport("__Internal")]
	static extern void _ib_SetCallback(Callbacks.ActionStringCallbackDelegate rewardReceivedCallback, IntPtr rewardReceivedActionPtr,
		Callbacks.ActionVoidCallbackDelegate rewardViewDismissedCallback, IntPtr rewardViewDismissedActionPtr);

	[DllImport("__Internal")]
	static extern void _ib_GetRewards();

	[DllImport("__Internal")]
	static extern void _ib_ConfirmRewards(string rewardsJson);
#endif
}