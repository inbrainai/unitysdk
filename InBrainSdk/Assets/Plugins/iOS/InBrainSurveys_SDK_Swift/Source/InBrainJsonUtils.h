//
//  InBrainJsonUtils.h
//  Unity-iPhone
//
//  Created by Ivan Tustanivsky on 14/01/20.
//

#import <Foundation/Foundation.h>
#import "InBrainSurveys_SDK_Swift/InBrainSurveys_SDK_Swift-Swift.h"

@interface InBrainJsonUtils : NSObject

+ (NSString *)serializeArray:(NSMutableArray *)array;
+ (NSString *)serializeDictionary:(NSDictionary *)dictionary;
+ (NSString *)serializeRewards:(NSArray<InBrainReward *> *)rewards;

+ (NSArray *)deserializeArray:(NSString *)jsonArray;
+ (NSDictionary *)deserializeDictionary:(NSString *)jsonDic;
+ (NSArray<NSNumber *> *)deserializeNumbersArray:(NSArray *)numbers;

@end
