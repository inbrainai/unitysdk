using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public class InBrainCallbackProxy : AndroidJavaProxy
{
	public InBrainCallbackProxy() : base("com.inbrain.sdk.callback.InBrainCallback")
	{
		void onClosed()
		{
		}

		bool handleRewards(AndroidJavaObject rewardsList /* List<Reward> rewards */)
		{
			return true;
		}
	}
}