using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace InBrain
{
	public class InBrainDemo : MonoBehaviour
	{
		[SerializeField] string ClientId = "9c367c28-c8a4-498d-bf22-1f3682fc73aa"; // your client id obtained by your account manager

		[SerializeField] string ClientSecret = "90MB8WyMZyYykgs0TaR21SqCcCZz3YTTXio9FoN5o5NJ6+svp3Q2tO8pvM9CjbskCaLAog0msmVTcIigKPQw4A=="; // your client secret obtained by your account manager

		[SerializeField] string AppUserId = "testing-unity@inbrain.ai";
		
		[Space]
		
		[SerializeField] Text BalanceText;

		RewardsResult rewards;

		void Start()
		{
			rewards = new RewardsResult();
			
			InBrain.Instance.Init(ClientId, ClientSecret);
			InBrain.Instance.SetAppUserId(AppUserId);

			InBrain.Instance.AddCallback(rewardsResult =>
			{
				Debug.Log("InBrain: Rewards callback received");
				rewards = rewardsResult;

				if (rewardsResult.rewards.Any())
				{
					float balance = rewardsResult.rewards.Sum(reward => reward.amount);
					BalanceText.text = string.Format("Your unconfirmed rewards balance: {0}", balance);
				}
				else
				{
					Debug.Log("InBrain: There are no pending rewards");
				}
			}, () => { Debug.Log("InBrain: Surveys web view was dismissed"); }, false);
		}

		public void OnShowSurveysClicked()
		{
			Debug.Log("InBrain: ShowSurveys button clicked");
			InBrain.Instance.ShowSurveys();
		}

		public void OnGetRewardsClicked()
		{
			Debug.Log("InBrain: GetRewards button clicked");
			InBrain.Instance.GetRewards();
		}

		public void OnConfirmRewardsClicked()
		{
			Debug.Log("InBrain: ConfirmRewards button clicked");

			if (rewards.rewards.Any())
			{
				InBrain.Instance.ConfirmRewards(rewards.rewards);
				BalanceText.text = "Your unconfirmed rewards balance: 0";
			}
			else
			{
				Debug.Log("InBrain: There are no rewards to confirm");
			}
		}
	}
}