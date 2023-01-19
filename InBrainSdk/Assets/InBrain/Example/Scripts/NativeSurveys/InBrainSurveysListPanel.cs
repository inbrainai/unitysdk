using System.Collections.Generic;
using UnityEngine;

namespace InBrain
{
	public class InBrainSurveysListPanel : MonoBehaviour
	{
		[SerializeField] GameObject loadingIcon = null;
		[SerializeField] Transform surveysContent = null;
		[SerializeField] InBrainSurveyListItem inBrainSurveyItemPrefab = null;

		readonly List<InBrainSurveyListItem> _surveys = new List<InBrainSurveyListItem>();

		public void Show()
		{
			gameObject.SetActive(true);
		}

		public void OnBackButtonClicked()
		{
			gameObject.SetActive(false);
		}

		void OnEnable()
		{
			loadingIcon.SetActive(true);

			// Uncomment following lines of code in order to filter native surveys
			// var filter = new InBrainSurveyFilter();
			// filter.placementId = "76f52733-62e0-4b0d-bb62-72ebf1b42edf";
			// filter.categoryIds = new List<InBrainSurveyCategory>() { InBrainSurveyCategory.HealthCare, InBrainSurveyCategory.Food };
			// filter.excludedCategoryIds = new List<InBrainSurveyCategory>() { InBrainSurveyCategory.Business };
			//
			// InBrain.Instance.GetSurveysWithFilter(filter, OnSurveysFetched);

			InBrain.Instance.GetSurveys(OnSurveysFetched);
		}

		void OnDisable()
		{
			ClearList();
		}

		void ClearList()
		{
			_surveys.ForEach(survey => Destroy(survey.gameObject));
			_surveys.Clear();
		}

		void OnSurveysFetched(List<InBrainSurvey> surveys)
		{
			loadingIcon.SetActive(false);

			foreach (var survey in surveys)
			{
				var surveyItem = Instantiate(inBrainSurveyItemPrefab, surveysContent);
				surveyItem.Init(survey);
				_surveys.Add(surveyItem);
			}
		}
	}
}