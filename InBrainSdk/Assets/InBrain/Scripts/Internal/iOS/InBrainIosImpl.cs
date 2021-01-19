using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using UnityEngine;

namespace InBrain
{
	public class InBrainIosImpl : IInBrainImpl
	{
		public void Init(string clientId, string clientSecret, bool isS2S, string userId)
		{
#if UNITY_IOS && !UNITY_EDITOR
			_ib_SetInBrain(clientId, clientSecret, isS2S, userId);
#endif
		}

		public void AddCallback(Action<List<InBrainReward>> onRewardsReceived, Action onRewardsViewDismissed, bool confirmRewardsAutomatically = false)
		{
			Action<string> onRewardsReceivedNative = rewardsJson =>
			{
				var rewardsResult = JsonUtility.FromJson<InBrainGetRewardsResult>(rewardsJson);
				onRewardsReceived?.Invoke(rewardsResult.rewards);

				if (confirmRewardsAutomatically && rewardsResult.rewards.Any())
				{
					ConfirmRewards(rewardsResult.rewards);
				}
			};

#if UNITY_IOS && !UNITY_EDITOR
			_ib_SetCallback(Callbacks.ActionStringCallback, onRewardsReceivedNative.GetPointer(),
				Callbacks.ActionVoidCallback, onRewardsViewDismissed.GetPointer());
#endif
		}

		public void RemoveCallback()
		{
#if UNITY_IOS && !UNITY_EDITOR
			_ib_RemoveCallback();
#endif
		}

		public void ShowSurveys()
		{
#if UNITY_IOS && !UNITY_EDITOR
			_ib_ShowSurveys();
#endif
		}

		public void GetRewards()
		{
#if UNITY_IOS && !UNITY_EDITOR
			_ib_GetRewards();
#endif
		}

		public void GetRewards(Action<List<InBrainReward>> onRewardsReceived, Action onFailedToReceiveRewards, bool confirmRewardsAutomatically = false)
		{
			Action<string> onRewardsReceivedNative = rewardsJson =>
			{
				var rewardsResult = JsonUtility.FromJson<InBrainGetRewardsResult>(rewardsJson);
				onRewardsReceived?.Invoke(rewardsResult.rewards);

				if (confirmRewardsAutomatically && rewardsResult.rewards.Any())
				{
					ConfirmRewards(rewardsResult.rewards);
				}
			};

#if UNITY_IOS && !UNITY_EDITOR
			_ib_GetRewardsWithCallback(Callbacks.ActionStringCallback, onRewardsReceivedNative.GetPointer(),
				Callbacks.ActionVoidCallback, onFailedToReceiveRewards.GetPointer());
#endif
		}

		public void ConfirmRewards(List<InBrainReward> rewards)
		{
			var rewardsIds = rewards.Select(reward => reward.transactionId).ToList();
			var rewardsJson = JsonUtility.ToJson(new InBrainRewardIds(rewardsIds));

#if UNITY_IOS && !UNITY_EDITOR
			_ib_ConfirmRewards(rewardsJson);
#endif
		}

		public void SetLanguage(string language)
		{
#if UNITY_IOS && !UNITY_EDITOR
			_ib_SetLanguage(language);
#endif
		}

		public void SetToolbarConfig(InBrainToolbarConfig config)
		{
			//TODO
		}

		public void SetStatusBarConfig(InBrainStatusBarConfig config)
		{
			//TODO
		}

#if UNITY_IOS && !UNITY_EDITOR
		[DllImport("__Internal")]
		static extern void _ib_SetInBrain(string clientId, string secret, bool isS2S, string userId);

		[DllImport("__Internal")]
		static extern void _ib_ShowSurveys();

		[DllImport("__Internal")]
		static extern void _ib_SetCallback(Callbacks.ActionStringCallbackDelegate rewardReceivedCallback, IntPtr rewardReceivedActionPtr,
			Callbacks.ActionVoidCallbackDelegate rewardViewDismissedCallback, IntPtr rewardViewDismissedActionPtr);

		[DllImport("__Internal")]
		static extern void _ib_RemoveCallback();

		[DllImport("__Internal")]
		static extern void _ib_GetRewards();

		[DllImport("__Internal")]
		static extern void _ib_GetRewardsWithCallback(Callbacks.ActionStringCallbackDelegate rewardReceivedCallback, IntPtr rewardReceivedActionPtr,
			Callbacks.ActionVoidCallbackDelegate failedToReceiveRewardsCallback, IntPtr failedToReceiveRewardsActionPtr);

		[DllImport("__Internal")]
		static extern void _ib_ConfirmRewards(string rewardsJson);

		[DllImport("__Internal")]
		static extern void _ib_SetLanguage(string language);
#endif
	}
}