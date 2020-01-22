using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using System.Linq;
using System.Runtime.InteropServices;

namespace InBrain
{
	[PublicAPI]
	public class InBrain : MonoSingleton<InBrain>
	{
		public void Init(string clientId, string clientSecret)
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				using (var inBrain = new AndroidJavaClass("com.inbrain.sdk.InBrain"))
				{
					var inBrainInst = inBrain.CallStatic<AndroidJavaObject>("getInstance");
					inBrainInst.Call("init", JniUtils.Activity, clientId, clientSecret);
				}
			}

#if UNITY_IOS && !UNITY_EDITOR
			_ib_Init(clientSecret);
#endif
		}
		
		public void SetAppUserId(string appUserId)
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				using (var inBrain = new AndroidJavaClass("com.inbrain.sdk.InBrain"))
				{
					var inBrainInst = inBrain.CallStatic<AndroidJavaObject>("getInstance");
					inBrainInst.Call("setAppUserId", appUserId);
				}
			}

#if UNITY_IOS && !UNITY_EDITOR
			_ib_SetAppUserId(appUserId);
#endif
		}

		public void AddCallback(Action<RewardsResult> onRewardsReceived, Action onRewardsViewDismissed, bool confirmRewardsAutomatically = false)
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				using (var inBrain = new AndroidJavaClass("com.inbrain.sdk.InBrain"))
				{
					var inBrainInst = inBrain.CallStatic<AndroidJavaObject>("getInstance");
					inBrainInst.Call("addCallback", new InBrainCallbackProxy(onRewardsReceived, onRewardsViewDismissed, confirmRewardsAutomatically));
				}
			}

#if UNITY_IOS && !UNITY_EDITOR
			Action<string> onRewardsReceivedNative = rewardsJson =>
			{
				var rewardsResult = JsonUtility.FromJson<RewardsResult>(rewardsJson);
				onRewardsReceived?.Invoke(rewardsResult);

				if (confirmRewardsAutomatically && rewardsResult.rewards.Any())
					ConfirmRewards(rewardsResult.rewards);
			};

	       _ib_SetCallback(Callbacks.ActionStringCallback, onRewardsReceivedNative.GetPointer(), Callbacks.ActionVoidCallback, onRewardsViewDismissed.GetPointer());
#endif
		}

		public void ShowSurveys()
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

		public void GetRewards()
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

		public void ConfirmRewards(List<InBrainReward> rewards)
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
			var rewardsJson = JsonUtility.ToJson(new RewardIds(rewardsIds));

			_ib_ConfirmRewards(rewardsJson);
#endif
		}

#if UNITY_IOS
		[DllImport("__Internal")]
		static extern void _ib_Init(string secret);
		
		[DllImport("__Internal")]
		static extern void _ib_SetAppUserId(string appId);

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
}