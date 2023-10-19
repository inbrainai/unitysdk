using System;
using System.Collections.Generic;
using UnityEngine;

namespace InBrain
{
	[Serializable]
	public class InBrainSurvey
	{
		[SerializeField] public string id;
		[SerializeField] public string searchId;
		[SerializeField] public long rank;
		[SerializeField] public long time;
		[SerializeField] public float value;
		[SerializeField] public bool currencySale;
		[SerializeField] public float multiplier;
		[SerializeField] public List<InBrainSurveyCategory> categories;
		[SerializeField] public InBrainSurveyConversionLevel conversionLevel;
		[SerializeField] public bool isProfilerSurvey;

		public InBrainSurvey(string id, string searchId, long rank, long time, float value, bool currencySale, float multiplier,
			List<InBrainSurveyCategory> categories, InBrainSurveyConversionLevel conversionLevel, bool isProfilerSurvey)
		{
			this.id = id;
			this.searchId = searchId;
			this.rank = rank;
			this.time = time;
			this.value = value;
			this.currencySale = currencySale;
			this.multiplier = multiplier;
			this.categories = categories;
			this.conversionLevel = conversionLevel;
			this.isProfilerSurvey = isProfilerSurvey;
		}

		public static InBrainSurvey FromAJO(AndroidJavaObject ajo)
		{
			return new InBrainSurvey(ajo.Get<string>("id"),
				ajo.Get<string>("searchId"),
				ajo.Get<long>("rank"),
				ajo.Get<long>("time"),
				ajo.Get<float>("value"),
				ajo.Get<bool>("currencySale"),
				ajo.Get<float>("multiplier"),
				ajo.GetAJO("categories").FromJavaList(category => category.FromSurveyCategoryAJO()),
				ajo.GetAJO("conversionLevel").FromSurveyConversionLevelAJO(),
				ajo.Get<bool>("isProfilerSurvey"));
		}

		public AndroidJavaObject ToAJO()
		{
			return Application.platform == RuntimePlatform.Android
				? new AndroidJavaObject("com.inbrain.sdk.model.Survey",
					id, rank, time, value, currencySale, multiplier, conversionLevel.ToAJO(), searchId, categories.ToJavaList(category => category.ToAJO()), isProfilerSurvey)
				: null;
		}

		public override string ToString()
		{
			return string.Format("id: {0}, searchId: {1}, rank: {2}, time: {3}, value: {4}, currencySale: {5}, multiplier: {6}, conversionLevel: {7}, categories: {8}, isProfilerSurvey: {9}",
				id, searchId, rank, time, value, currencySale, multiplier, conversionLevel, string.Join(";", categories.ToArray()), isProfilerSurvey);
		}
	}
}