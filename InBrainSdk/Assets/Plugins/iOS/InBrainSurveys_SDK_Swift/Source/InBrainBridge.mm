//
//  InBrainBridge.cpp
//  InBrainSurveys_SDK_Swift
//
//  Created by Ivan Tustanivsky on 09/01/20.
//  Copyright © 2020 InBrain. All rights reserved.
//

#include "InBrainSurveys_SDK_Swift/InBrainSurveys_SDK_Swift-Swift.h"

#import <InBrainSurveys_SDK_Swift/InBrainSurveys_SDK_Swift.h>

#pragma mark - C interface

extern "C" {
    
    void _ib_ShowSurveys() {
        NSString* secret = @"90MB8WyMZyYykgs0TaR21SqCcCZz3YTTXio9FoN5o5NJ6+svp3Q2tO8pvM9CjbskCaLAog0msmVTcIigKPQw4A==";
        NSString* appId = @"testing@inbrain.ai";
        
        NSLog(@"Hello from objc");

        //InBrain* brain = [InBrain shared];
        
        //[brain testMessage];
        
        //[brain presentInBrainWebViewWithSecret:secret withAppUID:appId];
        
        InBrainProxyViewController *viewConnection = [[InBrainProxyViewController alloc] init];
        
        [UnityGetGLViewController() presentViewController:viewConnection animated:NO completion:nil];
    }
}
