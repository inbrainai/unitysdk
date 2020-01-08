using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InBrainDemo : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		using (var inBrain = new AndroidJavaClass("com.inbrain.sdk.InBrain"))
		{
			var inBrainInst = inBrain.CallStatic<AndroidJavaObject>("getInstance");
			inBrainInst.Call("init", InBrainAndroidUtils.Activity, CLIENT_ID, CLIENT_SECRET);
			inBrainInst.Call("addCallback", new InBrainCallbackProxy());
		}
	}

	// Update is called once per frame
	void Update()
	{
	}

	const string CLIENT_ID = "9c367c28-c8a4-498d-bf22-1f3682fc73aa"; // your client id obtained by your account manager
	const string CLIENT_SECRET = "90MB8WyMZyYykgs0TaR21SqCcCZz3YTTXio9FoN5o5NJ6+svp3Q2tO8pvM9CjbskCaLAog0msmVTcIigKPQw4A=="; // your client secret obtained by your account manager

	public void OnShowSurveysClicked()
	{
		Debug.Log("ShowSurveys clicked!");

		using (var inBrain = new AndroidJavaClass("com.inbrain.sdk.InBrain"))
		{
			var inBrainInst = inBrain.CallStatic<AndroidJavaObject>("getInstance");

			inBrainInst.Call("showSurveys", InBrainAndroidUtils.Activity);
		}
	}
}