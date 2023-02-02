using System;
using System.Collections.Generic;
using System.Linq;

namespace SilkNetConvenience;

public interface IGetCreateInfo<T> {
	public ManagedResourceSet<T> GetCreateInfo();
}

public static class GetCreateInfoArrayExtensions {
	public static ManagedResourceCollection<T> GetCreateInfos<T>(this IEnumerable<IGetCreateInfo<T>> createInfos)
		where T : unmanaged {
		var infos = createInfos.Select(i => i.GetCreateInfo()).ToArray();
		return new ManagedResourceCollection<T>(infos);
	}
}

public unsafe class ManagedResourceCollection<T> : IDisposable where T : unmanaged{
	private readonly ManagedResourceSet<T>[] resourceSets;
	private readonly ManagedResources resources = new();

	public ManagedResourceCollection(ManagedResourceSet<T>[] resourceSets) {
		this.resourceSets = resourceSets;
		ResourcesPointer = resources.AllocateArray(resourceSets.Select(r => r.Resource).ToArray());
	}

	public T[] Resources => resourceSets.Select(r => r.Resource).ToArray();
	public uint Length => (uint)resourceSets.Length;
	public readonly T* ResourcesPointer;

	~ManagedResourceCollection() {
		Dispose();
	}

	public void Dispose() {
		foreach (var resource in resourceSets) {
			resource.Dispose();
		}
		resources.Dispose();
		GC.SuppressFinalize(this);
	}
}