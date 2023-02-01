using System;

namespace SilkNetConvenience; 

public static class TimeSpanExtensions {
	public static ulong GetTotalNanoSeconds(this TimeSpan? self) {
		return self.HasValue ? (ulong)self.Value.TotalMilliseconds * 1_000_000 : ulong.MaxValue;
	}
}