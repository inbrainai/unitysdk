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
		[SerializeField] public string start;
		[SerializeField] public string end;

		public DateTime StartDate => DateTime.ParseExact(start, "o", CultureInfo.InvariantCulture, DateTimeStyles.None);
		public DateTime EndDate => DateTime.ParseExact(end, "o", CultureInfo.InvariantCulture, DateTimeStyles.None);

		public InBrainCurrencySale(string title, float multiplier, string description, string start, string end)
		{
			this.title = title;
			this.multiplier = multiplier;
			this.description = description;
			this.start = start;
			this.end = end;
		}

		public static InBrainCurrencySale FromAJO(AndroidJavaObject ajo)
		{
			return new InBrainCurrencySale(ajo.Get<string>("description"),
				ajo.Get<float>("multiplier"),
				ajo.Get<string>("description"),
				ajo.Get<string>("start"),
				ajo.Get<string>("end"));
		}

		public AndroidJavaObject ToAJO()
		{
			return Application.platform == RuntimePlatform.Android
				? new AndroidJavaObject("com.inbrain.sdk.model.CurrencySale", start, end, description, multiplier)
				: null;
		}

		public override string ToString()
		{
			return string.Format("title: {0}, multiplier: {1}, description: {2}, start: {3}, end: {4}", title, multiplier, description, start, end);
		}
	}
}