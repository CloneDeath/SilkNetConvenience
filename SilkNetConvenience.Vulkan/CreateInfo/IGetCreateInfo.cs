namespace SilkNetConvenience.CreateInfo;

public interface IGetCreateInfo<T> {
	public ManagedResourceSet<T> GetCreateInfo();
}