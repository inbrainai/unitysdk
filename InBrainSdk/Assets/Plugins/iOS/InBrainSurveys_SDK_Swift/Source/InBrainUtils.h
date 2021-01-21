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

// Conver NSString to C style string
+ (char *)createCStringFrom:(NSString *)string;

+ (UIColor *)colorFrom:(int)colorInt;

@end
