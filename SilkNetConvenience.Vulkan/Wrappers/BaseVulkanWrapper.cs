using System;

namespace SilkNetConvenience.Wrappers; 

public abstract class BaseVulkanWrapper : IDisposable {
	public bool IsDisposed { get; private set; }
	protected abstract void ReleaseVulkanResources();

	private void ReleaseResources() {
		if (IsDisposed) return;
		ReleaseVulkanResources();
		IsDisposed = true;
	}
	public void Dispose() {
		ReleaseResources();
		GC.SuppressFinalize(this);
	}

	~BaseVulkanWrapper() {
		ReleaseResources();
	}
}