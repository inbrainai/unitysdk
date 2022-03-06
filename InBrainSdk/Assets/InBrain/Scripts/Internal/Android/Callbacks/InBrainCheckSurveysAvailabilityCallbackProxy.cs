using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace InBrain
{
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class InBrainCheckSurveysAvailabilityCallbackProxy : AndroidJavaProxy
	{
		readonly Action<bool> _onSurveysAvailabilityChecked;

		public InBrainCheckSurveysAvailabilityCallbackProxy(Action<bool> onAvailabilityChecked) : base(Constants.CheckSurveysAvailabilityCallbackJavaClass)
		{
			_onSurveysAvailabilityChecked = onAvailabilityChecked;
		}

		public void onSurveysAvailable(bool available)
		{
			InBrainSceneHelper.Queue(() => _onSurveysAvailabilityChecked(available));
		}
	}
}