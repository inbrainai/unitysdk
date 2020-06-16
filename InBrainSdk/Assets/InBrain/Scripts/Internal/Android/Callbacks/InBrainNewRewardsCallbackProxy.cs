using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace InBrain
{
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class InBrainNewRewardsCallbackProxy : AndroidJavaProxy
	{
		readonly Action<List<InBrainReward>> _onRewardsReceived;

		readonly bool _confirmRewardsAutomatically;

		public InBrainNewRewardsCallbackProxy(Action<List<InBrainReward>> onRewardsReceived, bool confirmRewardsAutomatically = false)
			: base(Constants.NewRewardsCallbackJavaCLass)
		{
			_onRewardsReceived = onRewardsReceived;

			_confirmRewardsAutomatically = confirmRewardsAutomatically;
		}

		public bool handleRewards(AndroidJavaObject rewardsList /* List<Reward> rewards */)
		{
			InBrainSceneHelper.Queue(() => _onRewardsReceived(new InBrainGetRewardsResult(rewardsList).rewards));
			return _confirmRewardsAutomatically;
		}
	}
}