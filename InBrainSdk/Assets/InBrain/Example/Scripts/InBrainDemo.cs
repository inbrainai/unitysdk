using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace InBrain
{
	public class InBrainDemo : MonoBehaviour
	{
		[SerializeField] string appUserId = "testing-unity@inbrain.ai";

		[SerializeField] InBrainSurveysListPanel inBrainSurveysListPanel = null;

		[Space] [SerializeField] Text balanceText = null;

		List<InBrainReward> _receivedRewards;

		void Start()
		{
			_receivedRewards = new List<InBrainReward>();

			InBrain.Instance.Init();
			InBrain.Instance.SetAppUserId(appUserId);

			InBrain.Instance.AddCallback(ProcessRewards, () => { Debug.Log("InBrain: Surveys web view was dismissed"); });
			InBrain.Instance.GetRewards();

			InBrain.Instance.SetLanguage("en-us");

			// Uncomment following two lines of code in order to customize surveys wall UI
			// SetStatusBarConfiguration();
			// SetToolbarConfiguration();

			// Uncomment following line to set additional data for enhancing InBrain experience
			// SetInBrainCustomData();

			InBrain.Instance.CheckSurveysAvailability(flag =>
			{
				Debug.Log("Surveys availability: " + flag);
			});
			
			InBrain.Instance.GetCurrencySale(sale =>
			{
				if (sale != null)
				{
					Debug.Log(sale.ToString());
				}
			});
		}

		public void OnShowSurveysClicked()
		{
			Debug.Log("InBrain: ShowSurveys button clicked");
			InBrain.Instance.ShowSurveys();
		}

		public void OnShowSurveysListClicked()
		{
			inBrainSurveysListPanel?.Show();
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

			var balance = rewards.Any() ? (int) rewards.Sum(reward => reward.amount) : 0;
			balanceText.text = $"Total Points: {balance}";

			Debug.Log($"InBrain: Pending rewards amount: {balance}");
		}
		
		void SetStatusBarConfiguration()
		{
			var statusBarConfig = new InBrainStatusBarConfig
			{
				StatusBarColor = Color.green,
				LightStatusBarIcons = false,
				HideStatusBarIos = false
			};
			InBrain.Instance.SetStatusBarConfig(statusBarConfig);
		}

		void SetToolbarConfiguration()
		{
			var toolbarConfig = new InBrainToolbarConfig
			{
				Title = "InBrain Demo (Unity)",
				ElevationEnabled = false,
				TitleColor = Color.red,
				ToolbarColor = Color.white,
				BackButtonColor = Color.magenta
			};
			InBrain.Instance.SetToolbarConfig(toolbarConfig);
		}

		void SetInBrainCustomData()
		{
			var trackingData = new InBrainTrackingData
			{
				sessionId = "testing33_Session"
			};
			var demographicData = new InBrainDemographicData
			{
				gender = "male", 
				age = 34
			};
			InBrain.Instance.SetCustomData(trackingData, demographicData);
		}
	}
}