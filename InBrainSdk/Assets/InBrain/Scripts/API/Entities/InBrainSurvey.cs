using System;
using UnityEngine;

namespace InBrain
{
	[Serializable]
	public class InBrainSurvey
	{
		[SerializeField] public string id;
		[SerializeField] public long rank;
		[SerializeField] public long time;
		[SerializeField] public float value;

		public InBrainSurvey(string id, long rank, long time, float value)
		{
			this.id = id;
			this.rank = rank;
			this.time = time;
			this.value = value;
		}

		public static InBrainSurvey FromAJO(AndroidJavaObject ajo)
		{
			return new InBrainSurvey(ajo.Get<string>("id"),
				ajo.Get<long>("rank"),
				ajo.Get<long>("time"),
				ajo.Get<float>("value"));
		}

		public AndroidJavaObject ToAJO()
		{
			return Application.platform == RuntimePlatform.Android
				? new AndroidJavaObject("com.inbrain.sdk.model.Survey", id, rank, time, value)
				: null;
		}

		public override string ToString()
		{
			return string.Format("id: {0}, rank: {1}, time: {2}, value: {3}", id, rank, time, value);
		}
	}
}