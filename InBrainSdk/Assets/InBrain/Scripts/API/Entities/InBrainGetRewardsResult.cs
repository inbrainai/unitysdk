using System;
using System.Collections.Generic;
using UnityEngine;

namespace InBrain
{
	[Serializable]
	public class InBrainGetRewardsResult
	{
		[SerializeField] public List<InBrainReward> rewards;

		public InBrainGetRewardsResult()
		{
			rewards = new List<InBrainReward>();
		}

		public InBrainGetRewardsResult(AndroidJavaObject listAJO)
		{
			rewards = listAJO.FromJavaList<InBrainReward>(InBrainReward.FromAJO);
		}
	}
}
