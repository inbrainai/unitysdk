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

- (void)viewDidAppear:(BOOL)animated {
    [super viewDidAppear:animated];
    
    inBrain = [InBrain shared];
    inBrain.inBrainDelegate = self;
    
    if (!isOpened) {
        [inBrain presentInBrainWebViewWithSecret:self.secret withAppUID:self.appId];
        isOpened = true;
    }
}

- (void)inBrainRewardsReceivedWithRewardsArray:(NSArray<InBrainReward *> * _Nonnull)rewardsArray {
    
    // serialize rewards list
    NSLog(@"Rewards count: %d", (int)rewardsArray.count);
    
    NSString* rewards = [InBrainJsonUtils serializeRewards:rewardsArray];
    
    if (_onRewardsReceived != nil) {
        _onRewardsReceived(rewards);
    }
}

- (void)inBrainWebViewDismissed {
    [self dismissViewControllerAnimated:NO completion:nil];
    isOpened = false;
    
    if (_onRewardsViewDismissed) {
        _onRewardsViewDismissed();
    }
}

@end
