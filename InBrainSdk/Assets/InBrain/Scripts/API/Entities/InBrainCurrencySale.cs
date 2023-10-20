using System;
using System.Globalization;
using UnityEngine;

namespace InBrain
{
	[Serializable]
	public class InBrainCurrencySale
	{
		[SerializeField] public string title;
		[SerializeField] public float multiplier;
		[SerializeField] public string description;
		[SerializeField] public string startOn;
		[SerializeField] public string endOn;

		[Obsolete("This property is deprecated. Use `startOn` instead")]
		[SerializeField] public string start;
		[Obsolete("This property is deprecated. Use `endOn` instead")]
		[SerializeField] public string end;

		public DateTime StartDate => DateTime.ParseExact(startOn, "o", CultureInfo.InvariantCulture, DateTimeStyles.None);
		public DateTime EndDate => DateTime.ParseExact(endOn, "o", CultureInfo.InvariantCulture, DateTimeStyles.None);

		public InBrainCurrencySale(string title, float multiplier, string description, string start, string end)
		{
			this.title = title;
			this.multiplier = multiplier;
			this.description = description;
			this.startOn = start;
			this.endOn = end;
		}

		public static InBrainCurrencySale FromAJO(AndroidJavaObject ajo)
		{
			return new InBrainCurrencySale(ajo.Get<string>("description"),
				ajo.Get<float>("multiplier"),
				ajo.Get<string>("description"),
				ajo.Get<string>("startOn"),
				ajo.Get<string>("endOn"));
		}

		public AndroidJavaObject ToAJO()
		{
			return Application.platform == RuntimePlatform.Android
				? new AndroidJavaObject("com.inbrain.sdk.model.CurrencySale", startOn, endOn, description, multiplier)
				: null;
		}

		public override string ToString()
		{
			return string.Format("title: {0}, multiplier: {1}, description: {2}, start: {3}, end: {4}", title, multiplier, description, startOn, endOn);
		}
	}
}