namespace SilkNetConvenience.CreateInfo.Pipelines;

public interface IGetCreateInfo<T> {
	public ManagedResourceSet<T> GetCreateInfo();
}