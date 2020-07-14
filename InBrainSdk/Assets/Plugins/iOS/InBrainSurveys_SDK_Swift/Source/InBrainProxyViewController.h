//
//  InBrainObjProxyViewController.h
//  Unity-iPhone
//
//  Created by Ivan Tustanivsky on 13/01/20.
//

#import "InBrainSurveys_SDK_Swift/InBrainSurveys_SDK_Swift-Swift.h"

NS_ASSUME_NONNULL_BEGIN

@interface InBrainProxyViewController : UIViewController<InBrainDelegate>

@property(nonatomic, copy) void (^onRewardsReceived)(NSString* rewards);
@property(nonatomic, copy) void (^onRewardsViewDismissed)();

@end

NS_ASSUME_NONNULL_END
