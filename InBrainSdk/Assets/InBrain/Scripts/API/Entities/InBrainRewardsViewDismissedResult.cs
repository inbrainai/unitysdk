using System;
using System.Collections.Generic;
using UnityEngine;

namespace InBrain
{
	[Serializable]
	public class InBrainRewardsViewDismissedResult
	{
		[SerializeField] public bool byWebView;
		[SerializeField] public List<InBrainSurveyReward> rewards;

		public InBrainRewardsViewDismissedResult()
		{
			rewards = new List<InBrainSurveyReward>();
		}

		public InBrainRewardsViewDismissedResult(bool byWebView, AndroidJavaObject listAJO)
		{
			this.byWebView = byWebView;

			rewards = listAJO.FromJavaList<InBrainSurveyReward>(InBrainSurveyReward.FromAJO);
		}
	}
}