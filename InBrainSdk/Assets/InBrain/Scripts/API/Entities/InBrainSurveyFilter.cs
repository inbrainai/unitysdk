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
	}
}