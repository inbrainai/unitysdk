using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace InBrain
{
	public class InBrainDemo : MonoBehaviour
	{
		[SerializeField] string ClientId = "9c367c28-c8a4-498d-bf22-1f3682fc73aa"; // your client id obtained by your account manager

		[SerializeField] string ClientSecret = "90MB8WyMZyYykgs0TaR21SqCcCZz3YTTXio9FoN5o5NJ6+svp3Q2tO8pvM9CjbskCaLAog0msmVTcIigKPQw4A=="; // your client secret obtained by your account manager

		[Space]
		
		[SerializeField] Text BalanceText;

		void Start()
		{
			InBrain.Instance.Init(ClientId, ClientSecret);

			InBrain.Instance.AddCallback(rewardsResult =>
			{
				Debug.Log("REWARDS CALLBACK RECEIVED");

				if (rewardsResult.rewards.Any())
				{
					float balance = rewardsResult.rewards.Sum(reward => reward.amount);

					BalanceText.text = string.Format("Your balance: {0}", balance);

					//InBrain.ConfirmRewards(rewardsResult.rewards);
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
			InBrain.Instance.ShowSurveys();
		}

		public void OnGetRewardsClicked()
		{
			Debug.Log("GetRewards button clicked!");
			InBrain.Instance.GetRewards();
		}
	}
}