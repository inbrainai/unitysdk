using System.Collections.Generic;
using UnityEngine;

namespace InBrain
{
	public class InBrainSurveysListPanel : MonoBehaviour
	{
		[SerializeField] GameObject loadingIcon;
		[SerializeField] Transform surveysContent;
		[SerializeField] InBrainSurveyListItem inBrainSurveyItemPrefab;

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