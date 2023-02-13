using System;
using UnityEngine;

namespace InBrain
{
	public enum InBrainSurveyOutcomeType
	{
		Completed = 0,
		Terminated
	}

	static class InBrainSurveyOutcomeTypeExtensions 
	{
		public static AndroidJavaObject ToAJO(this InBrainSurveyOutcomeType outcomeType)
		{
			using (var inBrainSurveyOutcomeTypeClass = new AndroidJavaClass(Constants.InBrainSurveyOutcomeTypeJavaClass))
			{
				return inBrainSurveyOutcomeTypeClass.CallStatic<AndroidJavaObject>(Constants.FromTypeJavaMethod, (int) outcomeType);
			}
		}

		public static InBrainSurveyOutcomeType FromSurveyOutcomeTypeAJO(this AndroidJavaObject category)
		{
			return (InBrainSurveyOutcomeType) category.Call<int>(Constants.GetTypeJavaMethod);
		}
	}
}