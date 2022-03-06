using System;
using System.Collections.Generic;
using UnityEngine;

namespace InBrain
{
	public class InBrainAndroidImpl : IInBrainImpl
	{
		readonly AndroidJavaObject _inBrainInst;

		InBrainCallbackProxy _callback;

		AndroidJavaObject InBrainInst
		{
			get
			{
				if (_inBrainInst == null)
				{
					Debug.LogWarning("InBrain Android implementation is not instantiated");
				}

				return _inBrainInst;
			}
		}

		public InBrainAndroidImpl()
		{
			using (var inBrainClass = new AndroidJavaClass(Constants.InBrainJavaClass))
			{
				_inBrainInst = inBrainClass.CallStatic<AndroidJavaObject>(Constants.GetInstanceJavaMethod);
			}
		}

		public void Init(string clientId, string clientSecret, bool isS2S, string userId)
		{
			JniUtils.RunOnUiThread(() =>
			{
				InBrainInst?.Call(Constants.SetInBrainJavaMethod,
					JniUtils.Activity, clientId, clientSecret, isS2S, userId);
			});
		}

		public void SetCustomData(InBrainTrackingData trackingData, InBrainDemographicData demographicData)
		{
			var demographicDataHashMap = new AndroidJavaObject("java.util.HashMap");
			demographicDataHashMap.Call<string>("put", "gender", demographicData?.gender);
			demographicDataHashMap.Call<string>("put", "age", demographicData?.age.ToString());

			var sessionId = trackingData?.sessionId;

			JniUtils.RunOnUiThread(() => { InBrainInst?.Call(Constants.SetInBrainValuesJavaMethod, sessionId, demographicDataHashMap); });
		}

		public void AddCallback(Action<List<InBrainReward>> onRewardsReceived, Action onRewardsViewDismissed, bool confirmRewardsAutomatically = false)
		{
			_callback = new InBrainCallbackProxy(onRewardsViewDismissed, onRewardsReceived, confirmRewardsAutomatically);

			JniUtils.RunOnUiThread(() => { InBrainInst?.Call(Constants.AddCallbackJavaMethod, _callback); });
		}

		public void RemoveCallback()
		{
			if (_callback == null)
			{
				Debug.LogWarning("InBrain Android callback wasn't set");
				return;
			}

			InBrainInst?.Call(Constants.RemoveCallbackJavaMethod, _callback);

			_callback = null;
		}

		public void CheckSurveysAvailability(Action<bool> onAvailabilityChecked)
		{
			JniUtils.RunOnUiThread(() => { InBrainInst?.Call(Constants.CheckSurveysAvailabilityJavaMethod, JniUtils.Activity, new InBrainCheckSurveysAvailabilityCallbackProxy(onAvailabilityChecked)); });
		}

		public void ShowSurveys()
		{
			JniUtils.RunOnUiThread(() => { InBrainInst?.Call(Constants.ShowSurveysJavaMethod, JniUtils.Activity, new InBrainStartSurveysCallbackProxy()); });
		}

		public void ShowSurvey(string surveyId)
		{
			JniUtils.RunOnUiThread(() => { InBrainInst?.Call(Constants.ShowSurveyJavaMethod, JniUtils.Activity, surveyId, new InBrainStartSurveysCallbackProxy()); });
		}

		public void ShowSurvey(string surveyId, string placementId)
		{
			JniUtils.RunOnUiThread(() => { InBrainInst?.Call(Constants.ShowSurveyJavaMethod, JniUtils.Activity, surveyId, placementId, new InBrainStartSurveysCallbackProxy()); });
		}

		public void GetRewards()
		{
			JniUtils.RunOnUiThread(() => { InBrainInst?.Call(Constants.GetRewardsJavaMethod); });
		}

		public void GetRewards(Action<List<InBrainReward>> onRewardsReceived, Action onFailedToReceiveRewards, bool confirmRewardsAutomatically = false)
		{
			JniUtils.RunOnUiThread(() =>
			{
				InBrainInst?.Call(Constants.GetRewardsJavaMethod,
					new InBrainGetRewardsCallbackProxy(onRewardsReceived, onFailedToReceiveRewards, confirmRewardsAutomatically));
			});
		}

		public void ConfirmRewards(List<InBrainReward> rewards)
		{
			JniUtils.RunOnUiThread(() => { InBrainInst?.Call(Constants.ConfirmRewardsJavaMethod, rewards.ToJavaList(reward => reward.ToAJO())); });
		}

		public void SetLanguage(string language)
		{
			JniUtils.RunOnUiThread(() => { InBrainInst?.Call(Constants.SetLanguageJavaMethod, language); });
		}

		public void SetToolbarConfig(InBrainToolbarConfig config)
		{
			var javaConfig = new AndroidJavaObject(Constants.ToolbarConfigJavaClass)
				.CallAJO("setElevationEnabled", config.ElevationEnabled)
				.CallAJO("setToolbarColor", config.ToolbarColor.ToJavaColor())
				.CallAJO("setBackButtonColor", config.BackButtonColor.ToJavaColor())
				.CallAJO("setTitleColor", config.TitleColor.ToJavaColor())
				.CallAJO("setTitle", config.Title);

			JniUtils.RunOnUiThread(() => { InBrainInst?.Call(Constants.SetToolbarConfigJavaMethod, javaConfig); });
		}

		public void SetStatusBarConfig(InBrainStatusBarConfig config)
		{
			var javaConfig = new AndroidJavaObject(Constants.StatusBarConfigJavaClass)
				.CallAJO("setStatusBarIconsLight", config.LightStatusBarIcons)
				.CallAJO("setStatusBarColor", config.StatusBarColor.ToJavaColor());

			JniUtils.RunOnUiThread(() => { InBrainInst?.Call(Constants.SetStatusBarConfigJavaMethod, javaConfig); });
		}

		public void GetSurveys(Action<List<InBrainSurvey>> onSurveysReceived)
		{
			JniUtils.RunOnUiThread(() => { InBrainInst?.Call(Constants.GetSurveysJavaMethod, new InBrainGetSurveysCallbackProxy(onSurveysReceived)); });
		}

		public void GetSurveys(string placementId, Action<List<InBrainSurvey>> onSurveysReceived)
		{
			JniUtils.RunOnUiThread(() => { InBrainInst?.Call(Constants.GetSurveysJavaMethod, placementId, new InBrainGetSurveysCallbackProxy(onSurveysReceived)); });
		}
	}
}