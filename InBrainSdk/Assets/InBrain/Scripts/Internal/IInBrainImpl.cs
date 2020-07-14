using System;
using System.Collections.Generic;

namespace InBrain
{
	public interface IInBrainImpl
	{
		void Init(string clientId, string clientSecret, bool isS2S);
		void SetAppUserId(string appUserId);
		void AddCallback(Action<List<InBrainReward>> onRewardsReceived, Action onRewardsViewDismissed, bool confirmRewardsAutomatically = false);
		void RemoveCallback();
		void ShowSurveys();
		void GetRewards();
		void GetRewards(Action<List<InBrainReward>> onRewardsReceived, Action onFailedToReceiveRewards, bool confirmRewardsAutomatically = false);
		void ConfirmRewards(List<InBrainReward> rewards);
	}
}