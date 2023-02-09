using System;
using UnityEngine;

namespace InBrain
{
	public enum InBrainSurveyCategory
	{
		Automotive = 1,
		BeveragesAlcoholic,
		BeveragesNonAlcoholic,
		Business,
		ChildrenAndParenting,
		CoalitionLoyaltyPrograms,
		DestinationsAndTourism,
		Education,
		ElectronicsComputerSoftware,
		EntertainmentAndLeisure,
		FinanceBankingInvestingAndInsurance,
		Food,
		GamblingLottery,
		GovernmentAndPolitics,
		HealthCare,
		Home,
		MediaAndPublishing,
		PersonalCare,
		Restaurants,
		SensitiveExplicitContent,
		SmokingTobacco,
		SocialResearch,
		SportsRecreationFitness,
		Telecommunications,
		Transportation,
		TravelAirlines,
		TravelHotels,
		TravelServicesAgencyBooking,
		CreditCards,
		VideoGames,
		FashionAndClothingOther,
		FashionAndClothingDepartmentStore
	}

	static class InBrainSurveyCategoryExtensions 
	{
		public static AndroidJavaObject ToAJO(this InBrainSurveyCategory category)
		{
			using (var inBrainSurveyCategoryClass = new AndroidJavaClass(Constants.InBrainSurveyCategoryJavaClass))
			{
				return inBrainSurveyCategoryClass.CallStatic<AndroidJavaObject>(Constants.FromIdJavaMethod, (int) category);
			}
		}

		public static InBrainSurveyCategory FromSurveyCategoryAJO(this AndroidJavaObject category)
		{
			return (InBrainSurveyCategory) category.Call<int>(Constants.GetIdJavaMethod);
		}
	}
}