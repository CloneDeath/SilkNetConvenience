using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorInvalidDeviceAddressExtException : ResultFailureException { public ErrorInvalidDeviceAddressExtException() : base(Result.ErrorInvalidDeviceAddressExt){}}