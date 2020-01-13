using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public static class InBrainIosUtils
{
	public static T Cast<T>(this IntPtr instancePtr)
	{
		var instanceHandle = GCHandle.FromIntPtr(instancePtr);
		if (!(instanceHandle.Target is T))
		{
			throw new InvalidCastException("Failed to cast IntPtr");
		}

		var castedTarget = (T) instanceHandle.Target;
		return castedTarget;
	}
	
	public static IntPtr GetPointer(this object obj)
	{
		return obj == null ? IntPtr.Zero : GCHandle.ToIntPtr(GCHandle.Alloc(obj));
	}
}