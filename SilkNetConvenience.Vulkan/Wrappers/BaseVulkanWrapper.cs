using System;
using System.Collections.Generic;

namespace SilkNetConvenience.Wrappers; 

public abstract class BaseVulkanWrapper : IDisposable {
	public bool IsDisposed { get; private set; }
	protected abstract void ReleaseVulkanResources();
	private readonly List<BaseVulkanWrapper> Children = new();
	public void AddChildResource(BaseVulkanWrapper child) {
		Children.Add(child);
	}

	private void ReleaseResources() {
		if (IsDisposed) return;
		ReleaseChildResources();
		ReleaseVulkanResources();
		IsDisposed = true;
	}

	private void ReleaseChildResources() {
		foreach (var child in Children) {
			child.Dispose();
		}
	}
	
	public void Dispose() {
		ReleaseResources();
		GC.SuppressFinalize(this);
	}

	~BaseVulkanWrapper() {
		ReleaseResources();
	}
}