using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace InBrain
{
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class InBrainStartSurveysCallbackProxy : AndroidJavaProxy
	{
		public InBrainStartSurveysCallbackProxy() : base(Constants.StartSurveysCallbackJavaClass)
		{
		}

		public void onSuccess()
		{
			Debug.Log("InBrain rewards started succesfully");
		}
		
		public void onFail(String error)
		{
			Debug.Log(string.Format("Failed to start inBrain: {0}", error));
		}
	}
}