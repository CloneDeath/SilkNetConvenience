namespace SilkNetConvenience.Assimp.Wrappers; 

public abstract class BaseAssimpWrapper : IDisposable {
	public bool IsDisposed { get; private set; }
	protected abstract void ReleaseAssimpResources();

	private void ReleaseResources() {
		if (IsDisposed) return;
		ReleaseAssimpResources();
		IsDisposed = true;
	}
	public void Dispose() {
		ReleaseResources();
		GC.SuppressFinalize(this);
	}

	~BaseAssimpWrapper() {
		ReleaseResources();
	}
}