//
//  InBrainUtils.m
//  Unity-iPhone
//
//  Created by Ivan Tustanivsky on 13/01/20.
//

#import "InBrainUtils.h"

@implementation InBrainUtils

+ (NSString *)createNSStringFrom:(const char *)cstring {
    return [NSString stringWithUTF8String:(cstring ?: "")];
}

+ (char *)cStringCopy:(const char *)string {
    char *res = (char *) malloc(strlen(string) + 1);
    strcpy(res, string);
    return res;
}

+ (char *)createCStringFrom:(NSString *)string {
    if (!string) {
        string = @"";
    }
    return [self cStringCopy:[string UTF8String]];
}

+ (UIColor *)colorFrom:(int)colorInt {
    CGFloat a = ((colorInt & 0xFF000000) >> 24) / 255;
    CGFloat r = ((colorInt & 0x00FF0000) >> 16) / 255;
    CGFloat g = ((colorInt & 0x0000FF00) >> 8) / 255;
    CGFloat b = ((colorInt & 0x000000FF)) / 255;
    return [UIColor colorWithRed:r green:g blue:b alpha:a];
}

@end
