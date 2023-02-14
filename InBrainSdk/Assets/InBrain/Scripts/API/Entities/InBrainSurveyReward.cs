using System;
using System.Collections.Generic;
using UnityEngine;

namespace InBrain
{
	[Serializable]
	public class InBrainSurveyReward
	{
		[SerializeField] public string surveyId;
		[SerializeField] public string placementId;
		[SerializeField] public List<InBrainSurveyCategory> categories;
		[SerializeField] public double userReward;
		[SerializeField] public InBrainSurveyOutcomeType outcomeType;
		
		public InBrainSurveyReward(string surveyId, string placementId, List<InBrainSurveyCategory> categories, double userReward, InBrainSurveyOutcomeType outcomeType)
		{
			this.surveyId = surveyId;
			this.placementId = placementId;
			this.categories = categories;
			this.userReward = userReward;
			this.outcomeType = outcomeType;
		}

		public static InBrainSurveyReward FromAJO(AndroidJavaObject ajo)
		{
			return new InBrainSurveyReward(ajo.Get<string>("surveyId"),
				ajo.Get<string>("placementId"),
				ajo.GetAJO("categories").FromJavaList(category => category.FromSurveyCategoryAJO()),
				ajo.Get<double>("userReward"),
				ajo.GetAJO("outcomeType").FromSurveyOutcomeTypeAJO());
		}

		public AndroidJavaObject ToAJO()
		{
			return Application.platform == RuntimePlatform.Android
				? new AndroidJavaObject("com.inbrain.sdk.model.InBrainSurveyReward", JsonUtility.ToJson(this))
				: null;
		}

		public override string ToString()
		{
			return string.Format("surveyId: {0}, placementId: {1}, userReward: {2}, outcomeType: {3}, categories: {4}",
				surveyId, placementId, userReward, outcomeType, string.Join(";", categories.ToArray()));
		}
	}
}