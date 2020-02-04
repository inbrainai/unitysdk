using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace InBrain
{
	public class InBrainDemo : MonoBehaviour
	{
		[SerializeField] string appUserId = "testing-unity@inbrain.ai";

		[Space] [SerializeField] Text balanceText;

		List<InBrainReward> _receivedRewards;

		void Start()
		{
			_receivedRewards = new List<InBrainReward>();

			InBrain.Instance.SetAppUserId(appUserId);

			InBrain.Instance.AddCallback(ProcessRewards, () => { Debug.Log("InBrain: Surveys web view was dismissed"); });
		}

		public void OnShowSurveysClicked()
		{
			Debug.Log("InBrain: ShowSurveys button clicked");
			InBrain.Instance.ShowSurveys();
		}

		public void OnGetRewardsClicked()
		{
			Debug.Log("InBrain: GetRewards button clicked");
			InBrain.Instance.GetRewards(ProcessRewards, () => { Debug.LogError("InBrain: Failed to receive rewards"); });
		}

		public void OnConfirmRewardsClicked()
		{
			Debug.Log("InBrain: ConfirmRewards button clicked");

			if (_receivedRewards.Any())
			{
				InBrain.Instance.ConfirmRewards(_receivedRewards);
				_receivedRewards.Clear();
				balanceText.text = "0";
			}
			else
			{
				Debug.Log("InBrain: There are no rewards to confirm");
			}
		}

		void ProcessRewards(List<InBrainReward> rewards)
		{
			Debug.Log("InBrain: Rewards callback received");

			_receivedRewards = rewards;

			if (rewards.Any())
			{
				var balance = rewards.Sum(reward => reward.amount);
				balanceText.text = ((int) balance).ToString();
			}
			else
			{
				Debug.Log("InBrain: There are no pending rewards");
			}
		}
	}
}