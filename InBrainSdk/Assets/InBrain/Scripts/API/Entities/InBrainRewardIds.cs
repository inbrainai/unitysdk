using System;
using System.Collections.Generic;
using UnityEngine;

namespace InBrain
{
	[Serializable]
	public class InBrainRewardIds
	{
		[SerializeField] public List<long> ids;

		public InBrainRewardIds(List<long> rewardIds)
		{
			ids = rewardIds;
		}
	}
}
