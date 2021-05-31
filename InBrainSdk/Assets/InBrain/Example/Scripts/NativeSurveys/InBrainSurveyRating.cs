using UnityEngine;
using UnityEngine.UI;

namespace InBrain
{
	public class InBrainSurveyRating : MonoBehaviour
	{
		[SerializeField] Image[] stars = null;
		[SerializeField] Color activeColor = new Color(221, 142, 28, 255);
		[SerializeField] Color inactiveColor = new Color(142, 142, 142, 255);

		public void SetRating(int rating)
		{
			for (var i = 0; i < stars.Length; i++)
			{
				stars[i].color = i < rating ? activeColor : inactiveColor;
			}
		}
	}
}