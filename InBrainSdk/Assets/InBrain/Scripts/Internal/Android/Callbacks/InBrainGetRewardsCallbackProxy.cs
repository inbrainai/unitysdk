using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace InBrain
{
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class InBrainGetRewardsCallbackProxy : AndroidJavaProxy
	{
		readonly Action<List<InBrainReward>> _onRewardsReceived;
		readonly Action _onFailedToReceiveFailedToReceiveRewards;
		
		readonly bool _confirmRewardsAutomatically;
		
		public InBrainGetRewardsCallbackProxy(Action<List<InBrainReward>> onRewardsReceived, Action onFailedToReceiveRewards, 
			bool confirmRewardsAutomatically = false) : base(Constants.GetRewardsCallbackJavaClass)
		{
			_onRewardsReceived = onRewardsReceived;
			_onFailedToReceiveFailedToReceiveRewards = onFailedToReceiveRewards;
			
			_confirmRewardsAutomatically = confirmRewardsAutomatically;
		}
		
		public void onFailToLoadRewards(AndroidJavaObject throwable)
		{
			Debug.Log("Failed to load rewards: " + throwable?.CallStr("getMessage"));
			InBrainSceneHelper.Queue(() => _onFailedToReceiveFailedToReceiveRewards());
		}

		public bool handleRewards(AndroidJavaObject rewardsList /* List<Reward> rewards */)
		{
			InBrainSceneHelper.Queue(() => _onRewardsReceived(new InBrainGetRewardsResult(rewardsList).rewards));
			return _confirmRewardsAutomatically;
		}
	}
}
