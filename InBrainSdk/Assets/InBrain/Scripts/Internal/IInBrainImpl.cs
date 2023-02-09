using System;
using System.Collections.Generic;

namespace InBrain
{
	public interface IInBrainImpl
	{
		void Init(string clientId, string clientSecret, bool isS2S, string userId);
		void SetCustomData(InBrainTrackingData trackingData, InBrainDemographicData demographicData);
		void AddCallback(Action<List<InBrainReward>> onRewardsReceived, Action onRewardsViewDismissed, bool confirmRewardsAutomatically = false);
		void RemoveCallback();
		void CheckSurveysAvailability(Action<bool> onAvailabilityChecked);
		void ShowSurveys();
		void ShowSurvey(string surveyId, string searchId);
		void GetRewards();
		void GetRewards(Action<List<InBrainReward>> onRewardsReceived, Action onFailedToReceiveRewards, bool confirmRewardsAutomatically = false);
		void ConfirmRewards(List<InBrainReward> rewards);
		void SetLanguage(string language);
		void SetToolbarConfig(InBrainToolbarConfig config);
		void SetStatusBarConfig(InBrainStatusBarConfig config);
		void GetSurveys(Action<List<InBrainSurvey>> onSurveysReceived);
		void GetSurveys(string placementId, Action<List<InBrainSurvey>> onSurveysReceived);
		void GetSurveysWithFilter(InBrainSurveyFilter filter, Action<List<InBrainSurvey>> onSurveysReceived);
		void GetCurrencySale(Action<InBrainCurrencySale> onCurrencySaleReceived);
	}
}