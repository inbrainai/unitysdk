using System;
using AOT;
using UnityEngine;

public static class Callbacks
{
	internal delegate void ActionVoidCallbackDelegate(IntPtr actionPtr);
	internal delegate void ActionStringCallbackDelegate(IntPtr actionPtr, string msg);
	
	[MonoPInvokeCallback(typeof(ActionVoidCallbackDelegate))]
	public static void ActionVoidCallback(IntPtr actionPtr)
	{
		if (Debug.isDebugBuild)
		{
			Debug.Log("ActionVoidCallback");
		}
			
		if (actionPtr != IntPtr.Zero)
		{
			var action = actionPtr.Cast<Action>();
			action();
		}
	}

	[MonoPInvokeCallback(typeof(ActionStringCallbackDelegate))]
	public static void ActionStringCallback(IntPtr actionPtr, string msg)
	{
		if (Debug.isDebugBuild)
		{
			Debug.Log("ActionStringCallback");
		}

		if (actionPtr != IntPtr.Zero)
		{
			var action = actionPtr.Cast<Action<string>>();
			action(msg);
		}
	}
}