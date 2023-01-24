using System;
using System.Collections.Generic;
using UnityEngine;

namespace InBrain
{
	[Serializable]
	public class InBrainSurveyFilter
	{
		[SerializeField] public string placementId;
		[SerializeField] public List<InBrainSurveyCategory> categoryIds;
		[SerializeField] public List<InBrainSurveyCategory> excludedCategoryIds;

		public InBrainSurveyFilter(string placementId, List<InBrainSurveyCategory> categoryIds = null, List<InBrainSurveyCategory> excludedCategoryIds = null)
		{
			this.placementId = placementId;
			this.categoryIds = categoryIds;
			this.excludedCategoryIds = excludedCategoryIds;
		}

		public AndroidJavaObject ToAJO()
		{
			return Application.platform == RuntimePlatform.Android
				? new AndroidJavaObject("com.inbrain.sdk.model.SurveyFilter", placementId,
					categoryIds.ToJavaList(category => category.ToAJO()), excludedCategoryIds.ToJavaList(category => category.ToAJO()))
				: null;
		}

		public override string ToString()
		{
			return string.Format("placementId: {0}, categoryIds: {1}, excludedCategoryIds: {2}", placementId, categoryIds, excludedCategoryIds);
		}
	}
}