using System;
using UnityEngine;

[Serializable]
public class InBrainReward
{
	[SerializeField]
	public int transactionId;
	[SerializeField]
	public float amount;
	[SerializeField]
	public string currency;
	[SerializeField]
	public int transactionType;

	public override string ToString()
	{
		return string.Format("transactionId: {0}, amount: {1}, currency: {2}, transactionType: {3}", transactionId, amount, currency, transactionType);
	}
}