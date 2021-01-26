using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace InBrain
{
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

		public static int ToARGBColor(this Color color)
		{
			var a = Mathf.RoundToInt(color.a * 255);
			var r = Mathf.RoundToInt(color.r * 255);
			var g = Mathf.RoundToInt(color.g * 255);
			var b = Mathf.RoundToInt(color.b * 255);

			var result = 0;
			result += a << 24;
			result += r << 16;
			result += g << 8;
			result += b;

			return result;
		}
	}
}