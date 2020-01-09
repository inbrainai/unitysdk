using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InBrain
{
	public static void Init(string clientId, string clientSecret)
	{
		using (var inBrain = new AndroidJavaClass("com.inbrain.sdk.InBrain"))
		{
			var inBrainInst = inBrain.CallStatic<AndroidJavaObject>("getInstance");
			inBrainInst.Call("init", InBrainAndroidUtils.Activity, clientId, clientSecret);
			inBrainInst.Call("addCallback", new InBrainCallbackProxy());
		}
	}

	public static void AddCallback()
	{
		using (var inBrain = new AndroidJavaClass("com.inbrain.sdk.InBrain"))
		{
			var inBrainInst = inBrain.CallStatic<AndroidJavaObject>("getInstance");
			inBrainInst.Call("addCallback", new InBrainCallbackProxy());
		}
	}

	public static void ShowSurveys()
	{
		using (var inBrain = new AndroidJavaClass("com.inbrain.sdk.InBrain"))
		{
			var inBrainInst = inBrain.CallStatic<AndroidJavaObject>("getInstance");

			inBrainInst.Call("showSurveys", InBrainAndroidUtils.Activity);
		}
	}
}
