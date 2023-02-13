using System;
using UnityEngine;

namespace InBrain
{
	public enum InBrainSurveyConversionLevel
	{
		NewSurvey = 0,
		VeryPoorConversion,
		PoorConversion,
		FairConversion,
		GoodConversion,
		VeryGoodConversion,
		ExcellentConversion
	}

	static class InBrainSurveyConversionLevelExtensions 
	{
		public static AndroidJavaObject ToAJO(this InBrainSurveyConversionLevel conversionLevel)
		{
			using (var inBrainSurveyConversionLevelClass = new AndroidJavaClass(Constants.InBrainSurveyConversionLevelJavaClass))
			{
				return inBrainSurveyConversionLevelClass.CallStatic<AndroidJavaObject>(Constants.FromLevelJavaMethod, (int) conversionLevel);
			}
		}

		public static InBrainSurveyConversionLevel FromSurveyConversionLevelAJO(this AndroidJavaObject category)
		{
			return (InBrainSurveyConversionLevel) category.Call<int>(Constants.GetLevelJavaMethod);
		}
	}
} 