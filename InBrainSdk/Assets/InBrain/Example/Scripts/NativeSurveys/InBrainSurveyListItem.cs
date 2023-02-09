using UnityEngine;
using UnityEngine.UI;

namespace InBrain
{
	public class InBrainSurveyListItem : MonoBehaviour
	{
		[SerializeField] Text pointsText = null;
		[SerializeField] Text durationText = null;
		[SerializeField] InBrainSurveyRating inBrainSurveyRating = null;

		string _surveyId;
		string _searchId;

		public void Init(InBrainSurvey data)
		{
			pointsText.text = string.Format("{0} points", data.value);
			durationText.text = string.Format("{0} min", data.time);
			inBrainSurveyRating.SetRating((int) data.rank);

			_surveyId = data.id;
			_searchId = data.searchId;
		}

		public void OnStartSurveyButtonClicked()
		{
			InBrain.Instance.ShowSurvey(_surveyId, _searchId);
		}
	}
}