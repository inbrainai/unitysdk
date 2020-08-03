//
//  InBrainBridge.cpp
//  InBrainSurveys_SDK_Swift
//
//  Created by Ivan Tustanivsky on 09/01/20.
//  Copyright © 2020 InBrain. All rights reserved.
//

#import "InBrainSurveys_SDK_Swift/InBrainSurveys_SDK_Swift-Swift.h"
#import "InBrainProxyViewController.h"
#import "InBrainFunctionDefs.h"
#import "InBrainUtils.h"
#import "InBrainJsonUtils.h"

#pragma mark - C interface

extern "C" {

    InBrainProxyViewController *inBrainView;
    
    void _ib_SetInBrain(char* clientId, char* secret, bool isS2S, char* userId) {
        NSString* secretString = [InBrainUtils createNSStringFrom:secret];
        NSString* clientIdString = [InBrainUtils createNSStringFrom:clientId];
        NSString* userIdString = [InBrainUtils createNSStringFrom:userId];
        
        inBrainView = [[InBrainProxyViewController alloc] init];
        inBrainView.modalPresentationStyle = UIModalPresentationFullScreen;
        
        [[InBrain shared] setInBrainWithApiClientID:clientIdString
                                          apiSecret:secretString
                                              isS2S:isS2S
                                             userID:userIdString];
    }

    void _ib_ShowSurveys() {
        [UnityGetGLViewController() presentViewController:inBrainView animated:NO completion:nil];
    }

    void _ib_SetCallback(ActionStringCallbackDelegate rewardReceivedCallback,
                         void *rewardReceivedActionPtr,
                         ActionVoidCallbackDelegate rewardViewDismissedCallback,
                         void *rewardViewDismissedActionPtr) {
        
        inBrainView.onRewardsReceived = ^(NSString* rewards) {
            rewardReceivedCallback(rewardReceivedActionPtr, [InBrainUtils createCStringFrom:rewards]);
        };
        
        inBrainView.onRewardsViewDismissed = ^{
            rewardViewDismissedCallback(rewardViewDismissedActionPtr);
        };
    }
    
    void _ib_RemoveCallback() {
        inBrainView.onRewardsReceived = nil;
        inBrainView.onRewardsViewDismissed = nil;
    }
    
    void _ib_GetRewards() {
        [[InBrain shared] getRewards];
    }

    void _ib_GetRewardsWithCallback(ActionStringCallbackDelegate rewardReceivedCallback, void *rewardReceivedActionPtr,
        ActionVoidCallbackDelegate failedToReceiveRewardsCallback, void *failedToReceiveRewardsActionPtr) {
        [[InBrain shared] getRewardsWithRewardsReceived:^(NSArray<InBrainReward *> * _Nonnull rewardsArray) {
            NSString* rewards = [InBrainJsonUtils serializeRewards:rewardsArray];
            rewardReceivedCallback(rewardReceivedActionPtr, [InBrainUtils createCStringFrom:rewards]);
        } failedToGetRewards:^{
            failedToReceiveRewardsCallback(failedToReceiveRewardsActionPtr);
        }];
    }

    void _ib_ConfirmRewards(char* rewardsJson) {
        NSDictionary *rewardsDictionary = [InBrainJsonUtils deserializeDictionary:[InBrainUtils createNSStringFrom:rewardsJson]];
        NSArray<NSNumber *> *rewardsToConfirm = [InBrainJsonUtils deserializeNumbersArray:rewardsDictionary[@"ids"]];
        [[InBrain shared] confirmRewardsWithTxIdArray:rewardsToConfirm];
    }
    
    void _ib_SetLanguage(char* language) {
            NSString* languageString = [InBrainUtils createNSStringFrom:language];
            
            [[InBrain shared] setLanguageWithValue:languageString];
    }
}
