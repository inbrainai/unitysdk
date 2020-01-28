using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using UnityEngine;

namespace InBrain
{
	public class InBrainIosImpl : IInBrainImpl
	{
		public void Init(string clientId, string clientSecret)
		{
			_ib_Init(clientSecret);
		}

		public void SetAppUserId(string appUserId)
		{
			_ib_SetAppUserId(appUserId);
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

			_ib_SetCallback(Callbacks.ActionStringCallback, onRewardsReceivedNative.GetPointer(),
				Callbacks.ActionVoidCallback, onRewardsViewDismissed.GetPointer());
		}

		public void ShowSurveys()
		{
			_ib_ShowSurveys();
		}

		public void GetRewards()
		{
			_ib_GetRewards();
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

			_ib_GetRewardsWithCallback(Callbacks.ActionStringCallback, onRewardsReceivedNative.GetPointer(),
				Callbacks.ActionVoidCallback, onFailedToReceiveRewards.GetPointer());
		}

		public void ConfirmRewards(List<InBrainReward> rewards)
		{
			var rewardsIds = rewards.Select(reward => reward.transactionId).ToList();
			var rewardsJson = JsonUtility.ToJson(new InBrainRewardIds(rewardsIds));

			_ib_ConfirmRewards(rewardsJson);
		}

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
		static extern void _ib_GetRewardsWithCallback(Callbacks.ActionStringCallbackDelegate rewardReceivedCallback, IntPtr rewardReceivedActionPtr,
			Callbacks.ActionVoidCallbackDelegate failedToReceiveRewardsCallback, IntPtr failedToReceiveRewardsActionPtr);

		[DllImport("__Internal")]
		static extern void _ib_ConfirmRewards(string rewardsJson);
	}
}