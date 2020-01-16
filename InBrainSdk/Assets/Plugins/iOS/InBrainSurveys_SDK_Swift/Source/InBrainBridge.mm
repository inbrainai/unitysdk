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
    
    void _ib_Init(char* secret, char* appId) {
        inBrainView = [[InBrainProxyViewController alloc] init];
        
        inBrainView.secret = [InBrainUtils createNSStringFrom:secret];
        inBrainView.appId = [InBrainUtils createNSStringFrom:appId];
        
        inBrainView.modalPresentationStyle = UIModalPresentationFullScreen;
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

    void _ib_GetRewards() {
        [[InBrain shared] getRewards];
    }

    void _ib_ConfirmRewards(char* rewardsJson) {
        NSArray *rewardsArray = [InBrainJsonUtils deserializeArray:[InBrainUtils createNSStringFrom:rewardsJson]];
        NSArray<NSNumber *> *rewardsToConfirm = [InBrainJsonUtils deserializeNumbersArray:rewardsArray];
        [[InBrain shared] confirmRewardsWithTxIdArray:rewardsToConfirm];
    }
}
