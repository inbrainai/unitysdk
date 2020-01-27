using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace InBrain
{
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class InBrainGetRewardsCallbackProxy : AndroidJavaProxy
	{
		readonly Action<RewardsResult> _onRewardsReceived;
		readonly Action _onFailedToReceiveFailedToReceiveRewards;
		
		readonly bool _confirmRewardsAutomatically;
		
		public InBrainGetRewardsCallbackProxy(Action<RewardsResult> onRewardsReceived, Action onFailedToReceiveRewards, bool confirmRewardsAutomatically = false)
			: base(Constants.GetRewardsCallbackJavaClass)
		{
			_onRewardsReceived = onRewardsReceived;
			_onFailedToReceiveFailedToReceiveRewards = onFailedToReceiveRewards;
			
			_confirmRewardsAutomatically = confirmRewardsAutomatically;
		}
		
		public void onFailToLoadRewards(int errorCode)
		{
			Debug.Log(string.Format("Failed to receive rewards. Error code: {0}", errorCode));
			InBrainSceneHelper.Queue(() => _onFailedToReceiveFailedToReceiveRewards());
		}

		public bool handleRewards(AndroidJavaObject rewardsList /* List<Reward> rewards */)
		{
			InBrainSceneHelper.Queue(() => _onRewardsReceived(new RewardsResult(rewardsList)));
			return _confirmRewardsAutomatically;
		}
	}
}
