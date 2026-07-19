namespace Pes.Abstractions
{
    /// <summary>
    /// Represents the current planetary phase transition state of the agent's psychological topology.
    /// </summary>
    public enum NpcEcosystemPhase
    {
        Homeostasis,         // The standard homeostatic equilibrium zone
        BlackoutNoDamping,   // Extreme Defense A: Artesian Eruption (Shock absorbers burned)
        PermanentlyMuted,    // Extreme Defense B: Ground Subsidence (Context window terminal dead-lock)
        GrandCleansedReset   // The Once-in-a-lifetime Pure Love Reset phase
    }
}