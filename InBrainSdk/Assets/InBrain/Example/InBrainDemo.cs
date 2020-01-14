using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InBrainDemo : MonoBehaviour
{
	[SerializeField] string ClientId = "9c367c28-c8a4-498d-bf22-1f3682fc73aa"; // your client id obtained by your account manager

	[SerializeField] string ClientSecret = "90MB8WyMZyYykgs0TaR21SqCcCZz3YTTXio9FoN5o5NJ6+svp3Q2tO8pvM9CjbskCaLAog0msmVTcIigKPQw4A=="; // your client secret obtained by your account manager

	[Space] [SerializeField] Text BalanceText;

	void Start()
	{
		InBrain.Init(ClientId, ClientSecret);

		InBrain.AddCallback(rewardsResult =>
		{
			Debug.Log("REWARDS CALLBACK RECEIVED");

			if (rewardsResult.rewards.Any())
			{
				var rewardsToConfirm = new List<int>();
				float balance = 0.0f;

				foreach (var reward in rewardsResult.rewards)
				{
					balance += reward.amount;
					rewardsToConfirm.Add(reward.transactionId);
				}

				BalanceText.text = string.Format("Your banance: {0}", balance);

				InBrain.ConfirmRewards(rewardsToConfirm);
			}
			else
			{
				Debug.Log("REWARDS LIST IS EMPTY");
			}
		}, () => { Debug.Log("SURVEYS VIEW DISMISSED"); });
	}

	public void OnShowSurveysClicked()
	{
		Debug.Log("ShowSurveys button clicked!");
		InBrain.ShowSurveys();
	}

	public void OnGetRewardsClicked()
	{
		Debug.Log("GetRewards button clicked!");
		InBrain.GetRewards();
	}
}