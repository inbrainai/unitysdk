using UnityEngine;
using UnityEngine.UI;

namespace InBrain
{
	public class InBrainSurveyRating : MonoBehaviour
	{
		[SerializeField] Image[] stars;
		[SerializeField] Color activeColor, inactiveColor;

		public void SetRating(int rating)
		{
			for (var i = 0; i < stars.Length; i++)
			{
				stars[i].color = i < rating ? activeColor : inactiveColor;
			}
		}
	}
}