using System;
using Silk.NET.Vulkan;
using SilkNetConvenience.Exceptions.ResultExceptions;
using TimeoutException = SilkNetConvenience.Exceptions.ResultExceptions.TimeoutException;

namespace SilkNetConvenience.Exceptions; 

public static class ResultExtensions {
	public static void AssertSuccess(this Result result) {
		switch (result) {
			case Result.Success: return;
			case Result.NotReady: throw new NotReadyException();
			case Result.Timeout: throw new TimeoutException();
			case Result.EventSet: throw new EventSetException();
			case Result.EventReset: throw new EventResetException();
			case Result.Incomplete: throw new IncompleteException();
			case Result.ErrorOutOfHostMemory: throw new ErrorOutOfHostMemoryException();
			case Result.ErrorOutOfDeviceMemory: throw new ErrorOutOfDeviceMemoryException();
			case Result.ErrorInitializationFailed: throw new ErrorInitializationFailedException();
			case Result.ErrorDeviceLost: throw new ErrorDeviceLostException();
			case Result.ErrorMemoryMapFailed: throw new ErrorMemoryMapFailedException();
			case Result.ErrorLayerNotPresent: throw new ErrorLayerNotPresentException();
			case Result.ErrorExtensionNotPresent: throw new ErrorExtensionNotPresentException();
			case Result.ErrorFeatureNotPresent: throw new ErrorFeatureNotPresentException();
			case Result.ErrorIncompatibleDriver: throw new ErrorIncompatibleDriverException();
			case Result.ErrorTooManyObjects: throw new ErrorTooManyObjectsException();
			case Result.ErrorFormatNotSupported: throw new ErrorFormatNotSupportedException();
			case Result.ErrorFragmentedPool: throw new ErrorFragmentedPoolException();
			case Result.ErrorUnknown: throw new ErrorUnknownException();
			case Result.ErrorSurfaceLostKhr: throw new ErrorSurfaceLostKhrException();
			case Result.ErrorNativeWindowInUseKhr: throw new ErrorNativeWindowInUseKhrException();
			case Result.SuboptimalKhr: throw new SuboptimalKhrException();
			case Result.ErrorOutOfDateKhr: throw new ErrorOutOfDateKhrException();
			case Result.ErrorIncompatibleDisplayKhr: throw new ErrorIncompatibleDisplayKhrException();
			case Result.ErrorValidationFailedExt: throw new ErrorValidationFailedExtException();
			case Result.ErrorInvalidShaderNV: throw new ErrorInvalidShaderNVException();
			case Result.ErrorImageUsageNotSupportedKhr: throw new ErrorImageUsageNotSupportedKhrException();
			case Result.ErrorVideoPictureLayoutNotSupportedKhr: throw new ErrorVideoPictureLayoutNotSupportedKhrException();
			case Result.ErrorVideoProfileOperationNotSupportedKhr: throw new ErrorVideoProfileOperationNotSupportedKhrException();
			case Result.ErrorVideoProfileFormatNotSupportedKhr: throw new ErrorVideoProfileFormatNotSupportedKhrException();
			case Result.ErrorVideoProfileCodecNotSupportedKhr: throw new ErrorVideoProfileCodecNotSupportedKhrException();
			case Result.ErrorVideoStdVersionNotSupportedKhr: throw new ErrorVideoStdVersionNotSupportedKhrException();
			case Result.ErrorOutOfPoolMemoryKhr: throw new ErrorOutOfPoolMemoryKhrException();
			case Result.ErrorInvalidExternalHandleKhr: throw new ErrorInvalidExternalHandleKhrException();
			case Result.ErrorInvalidDrmFormatModifierPlaneLayoutExt: throw new ErrorInvalidDrmFormatModifierPlaneLayoutExtException();
			case Result.ErrorFragmentationExt: throw new ErrorFragmentationExtException();
			case Result.ErrorNotPermittedExt: throw new ErrorNotPermittedExtException();
			case Result.ErrorInvalidDeviceAddressExt: throw new ErrorInvalidDeviceAddressExtException();
			case Result.ErrorFullScreenExclusiveModeLostExt: throw new ErrorFullScreenExclusiveModeLostExtException();
			case Result.ThreadIdleKhr: throw new ThreadIdleKhrException();
			case Result.ThreadDoneKhr: throw new ThreadDoneKhrException();
			case Result.OperationDeferredKhr: throw new OperationDeferredKhrException();
			case Result.OperationNotDeferredKhr: throw new OperationNotDeferredKhrException();
			case Result.PipelineCompileRequiredExt: throw new PipelineCompileRequiredExtException();
			case Result.ErrorCompressionExhaustedExt: throw new ErrorCompressionExhaustedExtException();
			default: throw new ArgumentOutOfRangeException(nameof(result), result, $"Unknown result: {result}");
		}
	}
}