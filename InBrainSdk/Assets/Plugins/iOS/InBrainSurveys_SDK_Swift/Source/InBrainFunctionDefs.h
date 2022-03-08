//
//  InBrainFunctionDefs.h
//  Unity-iPhone
//
//  Created by Ivan Tustanivsky on 13/01/20.
//

typedef void(ActionVoidCallbackDelegate)(void *actionPtr);

typedef void(ActionStringCallbackDelegate)(void *actionPtr, const char *data);

typedef void(ActionBoolCallbackDelegate)(void *actionPtr, bool data);
