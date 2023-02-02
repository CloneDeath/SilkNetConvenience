using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorInvalidDeviceAddressExtException : VulkanResultException { public ErrorInvalidDeviceAddressExtException() : base(Result.ErrorInvalidDeviceAddressExt){}}