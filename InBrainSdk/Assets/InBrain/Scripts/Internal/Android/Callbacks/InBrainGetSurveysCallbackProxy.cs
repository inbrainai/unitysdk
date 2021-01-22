using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace InBrain
{
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class InBrainGetSurveysCallbackProxy : AndroidJavaProxy
	{
		readonly Action<List<InBrainSurvey>> _onSurveysReceived;
		
		public InBrainGetSurveysCallbackProxy(Action<List<InBrainSurvey>> onSurveysReceived) : base(Constants.GetSurveysCallbackJavaClass)
		{
			_onSurveysReceived = onSurveysReceived;
		}
		
		public void nativeSurveysReceived(AndroidJavaObject surveyList /* List<Survey> rewards */)
		{
			InBrainSceneHelper.Queue(() => _onSurveysReceived(new InBrainGetSurveysResult(surveyList).surveys));
		}
	}
}