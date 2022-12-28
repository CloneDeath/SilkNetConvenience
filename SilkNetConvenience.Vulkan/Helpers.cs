using System;
using Silk.NET.Core.Native;
using Silk.NET.Vulkan;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience; 

public static unsafe class Helpers {
	public delegate Result ArrayAccessor<T>(ref uint length, T* dataPointer) where T : unmanaged;

	public static T[] GetArray<T>(ArrayAccessor<T> accessor) where T : unmanaged {
		uint length = 0;
		var sizeResult = accessor(ref length, null);
		if (sizeResult != Result.Success) throw new GetEnumerationSizeException(typeof(T), sizeResult);

		var data = new T[length];
		fixed (T* dataPointer = data) {
			var dataResult = accessor(ref length, dataPointer);
			if (dataResult != Result.Success) throw new GetEnumerationDataException(typeof(T), dataResult);
		}
		return data;
	}

	public delegate void VoidArrayAccessor<T>(ref uint length, T* dataPointer) where T : unmanaged;
	public static T[] GetArray<T>(VoidArrayAccessor<T> accessor) where T : unmanaged {
		uint length = 0;
		accessor(ref length, null);

		var data = new T[length];
		fixed (T* dataPointer = data) {
			accessor(ref length, dataPointer);
		}
		return data;
	}

	public static string GetString(byte* name) {
		return SilkMarshal.PtrToString((IntPtr)name) ?? string.Empty;
	}
}