//
//  InBrainProxyViewController.m
//  Unity-iPhone
//
//  Created by Ivan Tustanivsky on 13/01/20.
//

#import "InBrainProxyViewController.h"
#import "InBrainJsonUtils.h"

@interface InBrainProxyViewController ()

@end

@implementation InBrainProxyViewController

InBrain* inBrain;
bool isOpened = false;

- (InBrainProxyViewController*)init {
    self = [super init];
    inBrain = [InBrain shared];
    inBrain.inBrainDelegate = self;
    return self;
}

- (void)viewDidAppear:(BOOL)animated {
    [super viewDidAppear:animated];
    
    if (!isOpened) {
        if([_surveyId length] == 0) {
            [inBrain showSurveys];
        }
        else {
            [inBrain showNativeSurveyWithId:_surveyId];
        }
        isOpened = true;
    }
}

- (void)didReceiveInBrainRewardsWithRewardsArray:(NSArray<InBrainReward*>* _Nonnull)rewardsArray {
    NSString* rewards = [InBrainJsonUtils serializeRewards:rewardsArray];
    
    if (_onRewardsReceived != nil) {
        _onRewardsReceived(rewards);
    }
}

- (void)didFailToReceiveRewardsWithError:(NSError * _Nonnull)error {

}

- (void)surveysClosed {
    [self dismissViewControllerAnimated:NO completion:nil];
    isOpened = false;
    
    if (_onRewardsViewDismissed) {
        _onRewardsViewDismissed();
    }
}

@end
