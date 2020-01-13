//
//  InBrainProxyViewController.m
//  Unity-iPhone
//
//  Created by Ivan Tustanivsky on 13/01/20.
//

#import "InBrainProxyViewController.h"


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
    NSLog(@"CALL inBrainRewardsReceivedWithRewardsArray");
}

- (void)inBrainWebViewDismissed {
    NSLog(@"CALL inBrainWebViewDismissed");
    
    [self dismissViewControllerAnimated:NO completion:nil];
    isOpened = false;
}

@end
