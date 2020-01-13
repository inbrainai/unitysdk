//
//  InBrainBridge.cpp
//  InBrainSurveys_SDK_Swift
//
//  Created by Ivan Tustanivsky on 09/01/20.
//  Copyright © 2020 InBrain. All rights reserved.
//

#import "InBrainSurveys_SDK_Swift/InBrainSurveys_SDK_Swift-Swift.h"
#import <InBrainSurveys_SDK_Swift/InBrainSurveys_SDK_Swift.h>
#import "InBrainProxyViewController.h"

#pragma mark - C interface

extern "C" {
    
    void _ib_ShowSurveys() {
        InBrainProxyViewController *viewConnection = [[InBrainProxyViewController alloc] init];
        
        viewConnection.secret = @"90MB8WyMZyYykgs0TaR21SqCcCZz3YTTXio9FoN5o5NJ6+svp3Q2tO8pvM9CjbskCaLAog0msmVTcIigKPQw4A==";
        viewConnection.appId = @"testing@inbrain.ai";
        
        viewConnection.modalPresentationStyle = UIModalPresentationFullScreen;
        
        [UnityGetGLViewController() presentViewController:viewConnection animated:NO completion:nil];
    }
}
