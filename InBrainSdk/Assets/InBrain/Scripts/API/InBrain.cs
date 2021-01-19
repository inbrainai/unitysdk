using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace InBrain
{
	[PublicAPI]
	public class InBrain : MonoSingleton<InBrain>
	{
		IInBrainImpl _inBrainImpl;

		IInBrainImpl InBrainImpl
		{
			get
			{
				if (_inBrainImpl == null)
				{
					Debug.LogWarning("InBrain implementation is not instantiated or current platform is not supported");
				}

				return _inBrainImpl;
			}
		}

		protected override void Initialize()
		{
			// Depending on runtime platform create corresponding implementation

			if (Application.platform == RuntimePlatform.Android)
			{
				_inBrainImpl = new InBrainAndroidImpl();
			}

#if UNITY_IOS && !UNITY_EDITOR
			_inBrainImpl = new InBrainIosImpl();
#endif
		}

		/// <summary>
		/// Set unique user ID required for interaction with InBrain surveys API
		/// </summary>
		/// <param name="appUserId">Uniques user ID</param>
		public void SetAppUserId([NotNull] string appUserId)
		{
			InBrainImpl?.Init(InBrainSettings.ClientId, InBrainSettings.ClientSecretKey, InBrainSettings.IsS2S, appUserId);
		}

		/// <summary>
		/// Set global callback that will be triggered every time when new rewards received or surveys web view dismissed
		/// </summary>
		/// <param name="onRewardsReceived">Callback triggered when new rewards received</param>
		/// <param name="onRewardsViewDismissed">Callback triggered when surveys web view dismissed</param>
		/// <param name="confirmRewardsAutomatically">Flag indicating whether to confirm received rewards automatically</param>
		public void AddCallback([NotNull] Action<List<InBrainReward>> onRewardsReceived, [NotNull] Action onRewardsViewDismissed,
			bool confirmRewardsAutomatically = false)
		{
			InBrainImpl?.AddCallback(onRewardsReceived, onRewardsViewDismissed, confirmRewardsAutomatically);
		}

		/// <summary>
		/// Remove global callback to stop receiving notifications related to new rewards or surveys web view dismissal
		/// </summary>
		public void RemoveCallback()
		{
			InBrainImpl?.RemoveCallback();
		}

		/// <summary>
		/// Open surveys web view
		/// </summary>
		public void ShowSurveys()
		{
			InBrainImpl?.ShowSurveys();
		}

		/// <summary>
		/// Request list of pending (unconfirmed) rewards. Rewards list can be obtained via global callback. See AddCallback for more details
		/// </summary>
		public void GetRewards()
		{
			InBrainImpl?.GetRewards();
		}

		/// <summary>
		/// Request list of pending (unconfirmed) rewards. Rewards list can be obtained via provided callbacks
		/// </summary>
		/// <param name="onRewardsReceived">Callback triggered when new rewards received</param>
		/// <param name="onFailedToReceiveRewards">Callback triggered in case error occured during requesting rewards</param>
		/// <param name="confirmRewardsAutomatically">Flag indicating whether to confirm received rewards automatically</param>
		public void GetRewards([NotNull] Action<List<InBrainReward>> onRewardsReceived, [CanBeNull] Action onFailedToReceiveRewards,
			bool confirmRewardsAutomatically = false)
		{
			InBrainImpl?.GetRewards(onRewardsReceived, onFailedToReceiveRewards, confirmRewardsAutomatically);
		}

		/// <summary>
		/// Confirm rewards
		/// </summary>
		/// <param name="rewards">List of rewards to be confirmed</param>
		public void ConfirmRewards([NotNull] List<InBrainReward> rewards)
		{
			InBrainImpl?.ConfirmRewards(rewards);
		}

		/// <summary>
		/// Set surveys web view language
		/// </summary>
		/// <param name="language">
		/// Language to use in surveys web view.
		/// Accepted languages: "en-us", "fr-fr", "en-gb", "en-ca", "en-au", "en-in"
		/// </param>
		public void SetLanguage(string language)
		{
			InBrainImpl?.SetLanguage(language);
		}

		/// <summary>
		/// Set custom style for the surveys web view toolbar
		/// </summary>
		/// <param name="config">Toolbar configuration parameters</param>
		public void SetToolbarConfig(InBrainToolbarConfig config)
		{
			InBrainImpl?.SetToolbarConfig(config);
		}

		/// <summary>
		/// Set custom style for the surveys web view status bar
		/// </summary>
		/// <param name="config">Status bar configuration parameters</param>
		public void SetStatusBarConfig(InBrainStatusBarConfig config)
		{
			InBrainImpl?.SetStatusBarConfig(config);
		}
	}
}