using UnityEngine;
using UnityEngine.UI;

namespace InBrain
{
	public class InBrainSurveyListItem : MonoBehaviour
	{
		[SerializeField] Text pointsText;
		[SerializeField] Text durationText;
		[SerializeField] InBrainSurveyRating inBrainSurveyRating;

		string _surveyId;

		public void Init(InBrainSurvey data)
		{
			pointsText.text = string.Format("{0} points", data.value);
			durationText.text = string.Format("{0} min", data.time);
			inBrainSurveyRating.SetRating((int) data.rank);

			_surveyId = data.id;
		}

		public void OnStartSurveyButtonClicked()
		{
			InBrain.Instance.ShowSurvey(_surveyId);
		}
	}
}