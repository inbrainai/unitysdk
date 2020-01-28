using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace InBrain
{
	[PublicAPI]
	public class InBrain : MonoSingleton<InBrain>
	{
		IInBrainImpl _inBrainImpl;
		
		IInBrainImpl InBrainImpl
		{
			get
			{
				if (_inBrainImpl == null)
				{
					Debug.LogWarning("InBrain implementation is not instantiated or current platform is not supported");
				}

				return _inBrainImpl;
			}
		}

		protected override void Initialize()
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				_inBrainImpl = new InBrainAndroidImpl();
			}

#if UNITY_IOS && !UNITY_EDITOR
			_inBrainImpl = new InBrainIosImpl();
#endif
		}

		public void Init(string clientId, string clientSecret)
		{
			InBrainImpl?.Init(clientId, clientSecret);
		}

		public void SetAppUserId(string appUserId)
		{
			InBrainImpl?.SetAppUserId(appUserId);
		}

		public void AddCallback(Action<List<InBrainReward>> onRewardsReceived, Action onRewardsViewDismissed, bool confirmRewardsAutomatically = false)
		{
			InBrainImpl?.AddCallback(onRewardsReceived, onRewardsViewDismissed, confirmRewardsAutomatically);
		}

		public void ShowSurveys()
		{
			InBrainImpl?.ShowSurveys();
		}

		public void GetRewards()
		{
			InBrainImpl?.GetRewards();
		}

		public void GetRewards(Action<List<InBrainReward>> onRewardsReceived, Action onFailedToReceiveRewards, bool confirmRewardsAutomatically = false)
		{
			InBrainImpl?.GetRewards(onRewardsReceived, onFailedToReceiveRewards, confirmRewardsAutomatically);
		}

		public void ConfirmRewards(List<InBrainReward> rewards)
		{
			InBrainImpl?.ConfirmRewards(rewards);
		}
	}
}