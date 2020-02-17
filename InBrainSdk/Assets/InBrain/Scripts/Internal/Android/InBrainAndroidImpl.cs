using System;
using System.Collections.Generic;
using UnityEngine;

namespace InBrain
{
	public class InBrainAndroidImpl : IInBrainImpl
	{
		readonly AndroidJavaObject _inBrainInst;

		InBrainCallbackProxy _callback;

		AndroidJavaObject InBrainInst
		{
			get
			{
				if (_inBrainInst == null)
				{
					Debug.LogWarning("InBrain Android implementation is not instantiated");
				}

				return _inBrainInst;
			}
		}

		public InBrainAndroidImpl()
		{
			using (var inBrainClass = new AndroidJavaClass(Constants.InBrainJavaClass))
			{
				_inBrainInst = inBrainClass.CallStatic<AndroidJavaObject>(Constants.GetInstanceJavaMethod);
			}
		}
		
		public void Init(string clientId, string clientSecret)
		{
			InBrainInst?.Call(Constants.InitJavaMethod, JniUtils.Activity, clientId, clientSecret);
		}

		public void SetAppUserId(string appUserId)
		{
			InBrainInst?.Call(Constants.SetAppUserIdJavaMethod, appUserId);
		}

		public void AddCallback(Action<List<InBrainReward>> onRewardsReceived, Action onRewardsViewDismissed, bool confirmRewardsAutomatically = false)
		{
			_callback = new InBrainCallbackProxy(onRewardsReceived, onRewardsViewDismissed, confirmRewardsAutomatically);

			InBrainInst?.Call(Constants.AddCallbackJavaMethod, _callback);
		}
		
		public void RemoveCallback()
		{
			if (_callback == null)
			{
				Debug.LogWarning("InBrain Android callback wasn't set");
				return;
			}
			
			InBrainInst?.Call(Constants.RemoveCallbackJavaMethod, _callback);
			
			_callback = null;
		}

		public void ShowSurveys()
		{
			InBrainInst?.Call(Constants.ShowSurveysJavaMethod, JniUtils.Activity, new InBrainStartSurveysCallbackProxy());
		}

		public void GetRewards()
		{
			InBrainInst?.Call(Constants.GetRewardsJavaMethod);
		}

		public void GetRewards(Action<List<InBrainReward>> onRewardsReceived, Action onFailedToReceiveRewards, bool confirmRewardsAutomatically = false)
		{
			InBrainInst?.Call(Constants.GetRewardsJavaMethod, 
				new InBrainGetRewardsCallbackProxy(onRewardsReceived, onFailedToReceiveRewards, confirmRewardsAutomatically));
		}

		public void ConfirmRewards(List<InBrainReward> rewards)
		{
			InBrainInst?.Call(Constants.ConfirmRewardsJavaMethod, rewards.ToJavaList(reward => reward.ToAJO()));
		}
	}
}