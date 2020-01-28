using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace InBrain
{
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class InBrainCallbackProxy : AndroidJavaProxy
	{
		readonly Action<List<InBrainReward>> _onRewardsReceived;
		readonly Action _onRewardsViewDismissed;

		readonly bool _confirmRewardsAutomatically;

		public InBrainCallbackProxy(Action<List<InBrainReward>> onRewardsReceived, Action onRewardsViewDismissed, bool confirmRewardsAutomatically = false)
			: base(Constants.InBrainCallbackJavaCLass)
		{
			_onRewardsReceived = onRewardsReceived;
			_onRewardsViewDismissed = onRewardsViewDismissed;

			_confirmRewardsAutomatically = confirmRewardsAutomatically;
		}

		public void onClosed()
		{
			InBrainSceneHelper.Queue(() => _onRewardsViewDismissed());
		}

		public bool handleRewards(AndroidJavaObject rewardsList /* List<Reward> rewards */)
		{
			InBrainSceneHelper.Queue(() => _onRewardsReceived(new InBrainGetRewardsResult(rewardsList).rewards));
			return _confirmRewardsAutomatically;
		}
	}
}