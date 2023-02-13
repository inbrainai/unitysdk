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

			// Comment following line in order to proceed with native surveys filtering
			InBrain.Instance.GetSurveys(OnSurveysFetched);

			// Uncomment following lines of code in order to filter native surveys
			// var filter = new InBrainSurveyFilter("76f52733-62e0-4b0d-bb62-72ebf1b42edf", 
			// 	new List<InBrainSurveyCategory> { InBrainSurveyCategory.SocialResearch });
			// InBrain.Instance.GetSurveysWithFilter(filter, OnSurveysFetched);
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

				Debug.Log(survey);
			}
		}
	}
}