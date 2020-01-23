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
        NSLog(@"APP ID: %@", self.appId);
        [inBrain presentInBrainWebViewWithSecret:self.secret withAppUID:self.appId];
        isOpened = true;
    }
}

- (void)inBrainRewardsReceivedWithRewardsArray:(NSArray<InBrainReward *> * _Nonnull)rewardsArray {
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
