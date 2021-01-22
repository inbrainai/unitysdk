using System;
using System.Collections.Generic;
using UnityEngine;

namespace InBrain
{
	[Serializable]
	public class InBrainGetSurveysResult
	{
		[SerializeField] public List<InBrainSurvey> surveys;

		public InBrainGetSurveysResult()
		{
			surveys = new List<InBrainSurvey>();
		}

		public InBrainGetSurveysResult(AndroidJavaObject listAJO)
		{
			surveys = listAJO.FromJavaList<InBrainSurvey>(InBrainSurvey.FromAJO);
		}
	}
}