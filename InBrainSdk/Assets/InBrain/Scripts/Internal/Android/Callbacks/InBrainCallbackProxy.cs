using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace InBrain
{
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class InBrainCallbackProxy : AndroidJavaProxy
	{
		readonly Action _onRewardsViewDismissed;

		public InBrainCallbackProxy(Action onRewardsViewDismissed)
			: base(Constants.InBrainCallbackJavaCLass)
		{
			_onRewardsViewDismissed = onRewardsViewDismissed;
		}

		public void onClosed()
		{
			InBrainSceneHelper.Queue(() => _onRewardsViewDismissed());
		}

		public void onClosedFromPage()
		{
			InBrainSceneHelper.Queue(() => _onRewardsViewDismissed());
		}
	}
}