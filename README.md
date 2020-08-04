# InBrain Surveys Unity SDK

InBrain Surveys Unity SDK allows to present native web view with surveys from within your Unity app and receive rewards for their completion.

## Supported platforms

- iOS
- Android

## Requirements

Unity 2018.4.14f1+

## Installation

Download latest version of InBrain Surveys Unity SDK (.unitypackage). In Unity editor right click inside Project pane and select Import package -> Custom package. Select file you've downloaded and confirm. Now InBrain Surveys Unity SDK should be ready to use.

## Configuration

There are some prerequisites for InBrain Surveys Unity SDK:
- API ClientID (Provided in inBrain.ai dashboard)
- API Secret (Provided in inBrain.ai dashboard)
- Server to Server (flag indicating whether your app using S2S callbacks for inBrain rewards)

In Unity editor go to Window -> InBrain -> Edit Settings and enter those values in corresponding input fields.

## Usage

### Setup

In order to access SDK functionality InBrain singleton object required. One will be instantiated automatically after referencing its 'Instance' method for the first time.
Also, provide unique user ID before calling any other methods to specify which player should receive rewards.

```
InBrain.Instance.SetAppUserId(appUserId);
```

### Callbacks

Set global callback that will be triggered every time when new rewards received or surveys web view dismissed.

```
InBrain.Instance.AddCallback(rewards => {
	// process rewards...
}, () => { 
	// handle web view dismissal... 
});
```

Optionally you can specify whether to confirm received rewards automatically (by default they are not).

### Show surveys

Present surveys web view with the following call:

```
InBrain.Instance.ShowSurveys();
```

### Get rewards

There are two ways to receive rewards that user earned during surveys completion:

- use global callback (described above)
- request rewards manually with GetRewards method

```
// Global callback will be triggered
InBrain.Instance.GetRewards();

// Provide callbacks as parameters
InBrain.Instance.GetRewards(rewards => {
	// process rewards...
}, () => { 
	// process error... 
});

```

### Confirm rewards

Processed rewards should be confirmed in order to avoid receiving them repeatedly.

```
InBrain.Instance.ConfirmRewards(rewardsList);
```

This call should always be made following rewards data processing.

### Language

In order to change surveys web view language `SetLanguage` method should be called before it is opened.

```
InBrain.Instance.SetLanguage(language);
```

Accepted languages: "en-us", "fr-fr", "en-gb", "en-ca", "en-au", "en-in". System language is used by default.
