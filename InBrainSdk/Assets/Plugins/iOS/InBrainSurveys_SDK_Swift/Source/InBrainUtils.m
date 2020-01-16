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

@end
