using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace InBrain
{
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class InBrainCallbackProxy : AndroidJavaProxy
	{
		readonly Action<RewardsResult> _onRewardsReceived;
		readonly Action _onRewardsViewDismissed;

		readonly bool _confirmRewardsAutomatically;

		public InBrainCallbackProxy(Action<RewardsResult> onRewardsReceived, Action onRewardsViewDismissed, bool confirmRewardsAutomatically = false)
			: base("com.inbrain.sdk.callback.InBrainCallback")
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
			InBrainSceneHelper.Queue(() => _onRewardsReceived(new RewardsResult(rewardsList)));
			return _confirmRewardsAutomatically;
		}
	}
}