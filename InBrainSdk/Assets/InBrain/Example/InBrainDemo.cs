using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InBrainDemo : MonoBehaviour
{
	[SerializeField]
	string ClientId = "9c367c28-c8a4-498d-bf22-1f3682fc73aa"; // your client id obtained by your account manager
	
	[SerializeField]
	string ClientSecret = "90MB8WyMZyYykgs0TaR21SqCcCZz3YTTXio9FoN5o5NJ6+svp3Q2tO8pvM9CjbskCaLAog0msmVTcIigKPQw4A=="; // your client secret obtained by your account manager
	
	void Start()
	{
		Debug.Log("Demo start!");
		InBrain.Init(ClientId, ClientSecret);
	}

	public void OnShowSurveysClicked()
	{
		Debug.Log("ShowSurveys button clicked!");
		InBrain.ShowSurveys();
	}
}