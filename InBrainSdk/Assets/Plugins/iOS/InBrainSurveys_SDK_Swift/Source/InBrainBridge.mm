//
//  InBrainBridge.cpp
//  InBrainSurveys_SDK_Swift
//
//  Created by Ivan Tustanivsky on 09/01/20.
//  Copyright © 2020 InBrain. All rights reserved.
//

#import "InBrainSurveys_SDK_Swift/InBrainSurveys_SDK_Swift-Swift.h"
#import <InBrainSurveys_SDK_Swift/InBrainSurveys_SDK_Swift.h>
#import "InBrainProxyViewController.h"
#import "InBrainFunctionDefs.h"
#import "InBrainUtils.h"
#import "InBrainJsonUtils.h"

#pragma mark - C interface

extern "C" {

    InBrainProxyViewController *inBrainView;
    
    void _ib_Init(char* secret) {
        inBrainView = [[InBrainProxyViewController alloc] init];
        
        inBrainView.secret = [InBrainUtils createNSStringFrom:secret];
        [[InBrain shared] setAppSecretWithSecret:[InBrainUtils createNSStringFrom:secret]];
        
        inBrainView.modalPresentationStyle = UIModalPresentationFullScreen;
    }

    void _ib_SetAppUserId(char* appId) {
        inBrainView.appId = [InBrainUtils createNSStringFrom:appId];
        [[InBrain shared] setAppUserIdWithAppUID:[InBrainUtils createNSStringFrom:appId]];
    }

    void _ib_ShowSurveys() {
        [UnityGetGLViewController() presentViewController:inBrainView animated:NO completion:nil];
    }

    void _ib_SetCallback(ActionStringCallbackDelegate rewardReceivedCallback, void *rewardReceivedActionPtr,
        ActionVoidCallbackDelegate rewardViewDismissedCallback, void *rewardViewDismissedActionPtr) {
        
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
}
