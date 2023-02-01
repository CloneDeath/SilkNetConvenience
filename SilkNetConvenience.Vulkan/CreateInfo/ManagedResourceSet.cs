using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Silk.NET.Core.Native;

namespace SilkNetConvenience.CreateInfo; 

public class ManagedResourceSet<T> : IDisposable {
	public readonly T Resource;
	private readonly ManagedResources _other;

	public ManagedResourceSet(T resource, ManagedResources other) {
		Resource = resource;
		_other = other;
	}

	~ManagedResourceSet() {
		Dispose();
	}

	public void Dispose() {
		_other.Dispose();
		GC.SuppressFinalize(this);
	}
}

public unsafe class ManagedResources : IDisposable {
	private readonly List<nint> _strings = new();
	private readonly List<IntPtr> _hGlobals = new();
	private readonly List<IDisposable> _disposables = new();

	~ManagedResources() {
		Dispose();
	}

	public void Dispose() {
		foreach (var str in _strings) {
			SilkMarshal.Free(str);
		}
		foreach (var disposable in _disposables) {
			disposable.Dispose();
		}
		foreach (var alloc in _hGlobals) {
			Marshal.FreeHGlobal(alloc);
		}
		GC.SuppressFinalize(this);
	}

	public byte* AllocateString(string str) {
		var ptr = SilkMarshal.StringToPtr(str);
		_strings.Add(ptr);
		return (byte*)ptr;
	}

	public byte** AllocateStringArray(string[] strings) {
		var ptr = SilkMarshal.StringArrayToPtr(strings);
		_strings.Add(ptr);
		return (byte**)ptr;
	}

	public TOut* AllocateCreateInfo<TOut>(IGetCreateInfo<TOut> createInfo) where TOut : unmanaged {
		var ptr = Marshal.AllocHGlobal(Marshal.SizeOf<TOut>());
		_hGlobals.Add(ptr);

		var span = new Span<TOut>((void*)ptr, 1);
		var managedResources = createInfo.GetCreateInfo();
		_disposables.Add(managedResources);
		new[] { managedResources.Resource }.AsSpan().CopyTo(span);
		return (TOut*)ptr;
	}

	public TOut* AllocateCreateInfos<TOut, TIn>(params TIn[] createInfos) 
		where TOut : unmanaged 
		where TIn : IGetCreateInfo<TOut> {
		var ptr = Marshal.AllocHGlobal(Marshal.SizeOf<TOut>() * createInfos.Length);
		_hGlobals.Add(ptr);

		var span = new Span<TOut>((void*)ptr, createInfos.Length);
		var managedResource = createInfos.Select(c => c.GetCreateInfo()).ToArray();
		_disposables.AddRange(managedResource);
		var creates = managedResource.Select(r => r.Resource).ToArray();
		creates.AsSpan().CopyTo(span);
		return (TOut*)ptr;
	}

	public T* AllocateArray<T>(params T[] array) where T : unmanaged {
		var ptr = Marshal.AllocHGlobal(sizeof(T) * array.Length);
		_hGlobals.Add(ptr);

		var span = new Span<T>((void*)ptr, array.Length);
		array.AsSpan().CopyTo(span);
		return (T*)ptr;
	}

	public T* AllocateStruct<T>(T structure) where T : unmanaged {
		var ptr = Marshal.AllocHGlobal(sizeof(T));
		_hGlobals.Add(ptr);
		var span = new Span<T>((void*)ptr, 1);
		new[] { structure }.AsSpan().CopyTo(span);
		return (T*)ptr;
	}
}