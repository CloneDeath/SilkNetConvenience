using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorVideoPictureLayoutNotSupportedKhrException : VulkanResultException { public ErrorVideoPictureLayoutNotSupportedKhrException() : base(Result.ErrorVideoPictureLayoutNotSupportedKhr){}}