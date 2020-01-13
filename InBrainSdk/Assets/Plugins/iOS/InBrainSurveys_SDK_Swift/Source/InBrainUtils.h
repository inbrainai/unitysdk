//
//  InBrainUtils.h
//  Unity-iPhone
//
//  Created by Ivan Tustanivsky on 13/01/20.
//

#import <Foundation/Foundation.h>
#import <UIKit/UIImage.h>

@interface InBrainUtils : NSObject

// Converts C style string to NSString
+ (NSString *)createNSStringFrom:(const char *)cstring;

@end
