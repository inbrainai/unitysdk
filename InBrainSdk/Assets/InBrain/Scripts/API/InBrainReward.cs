using System;
using System.Collections.Generic;
using UnityEngine;

namespace InBrain
{
	[Serializable]
	public class InBrainReward
	{
		[SerializeField] public long transactionId;
		[SerializeField] public float amount;
		[SerializeField] public string currency;
		[SerializeField] public int transactionType;

		public InBrainReward(long transactionId, float amount, string currency, int transactionType)
		{
			this.transactionId = transactionId;
			this.amount = amount;
			this.currency = currency;
			this.transactionType = transactionType;
		}

		public static InBrainReward FromAJO(AndroidJavaObject ajo)
		{
			return new InBrainReward(ajo.Get<long>("transactionId"),
				ajo.Get<float>("amount"),
				ajo.Get<string>("currency"),
				ajo.Get<int>("transactionType"));
		}

		public AndroidJavaObject ToAJO()
		{
			return Application.platform == RuntimePlatform.Android
				? new AndroidJavaObject("com.inbrain.sdk.model.Reward", transactionId, amount, currency, transactionType)
				: null;
		}

		public override string ToString()
		{
			return string.Format("transactionId: {0}, amount: {1}, currency: {2}, transactionType: {3}", transactionId, amount, currency, transactionType);
		}
	}

	[Serializable]
	public class RewardsResult
	{
		[SerializeField] public List<InBrainReward> rewards;

		public RewardsResult(AndroidJavaObject listAJO)
		{
			rewards = listAJO.FromJavaList<InBrainReward>(InBrainReward.FromAJO);
		}
	}
}