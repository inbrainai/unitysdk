using System;
using AOT;
using UnityEngine;

namespace InBrain
{
	public static class Callbacks
	{
		internal delegate void ActionVoidCallbackDelegate(IntPtr actionPtr);

		internal delegate void ActionStringCallbackDelegate(IntPtr actionPtr, string msg);

		internal delegate void ActionBoolCallbackDelegate(IntPtr actionPtr, bool flag);

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

		[MonoPInvokeCallback(typeof(ActionBoolCallbackDelegate))]
		public static void ActionBoolCallback(IntPtr actionPtr, bool flag)
		{
			if (Debug.isDebugBuild)
			{
				Debug.Log("ActionBoolCallback");
			}

			if (actionPtr != IntPtr.Zero)
			{
				var action = actionPtr.Cast<Action<bool>>();
				action(flag);
			}
		}
	}
}