//
//  InBrainObjProxyViewController.h
//  Unity-iPhone
//
//  Created by Ivan Tustanivsky on 13/01/20.
//

#import "InBrainSurveys_SDK_Swift/InBrainSurveys_SDK_Swift-Swift.h"

NS_ASSUME_NONNULL_BEGIN

@interface InBrainProxyViewController : UIViewController<InBrainDelegate>

@property(nonatomic, copy) NSString* secret;
@property(nonatomic, copy) NSString* appId;

@end

NS_ASSUME_NONNULL_END
