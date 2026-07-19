using System;
using System.Collections.Generic;

namespace Pes.Abstractions
{
    /// <summary>
    /// Explicit read-only telemetry interface designed to completely block malicious mutation hacks from external UI systems.
    /// </summary>
    public interface ISoulSnapshot
    {
        IReadOnlyDictionary<SoulOrgan, double> State { get; }
        DateTime Timestamp { get; }
    }

    public record SoulSnapshot(IReadOnlyDictionary<SoulOrgan, double> State, DateTime Timestamp) : ISoulSnapshot;
}