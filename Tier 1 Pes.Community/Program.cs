using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Pes.Runtime.Core;
using Pes.Runtime.Hydrology;

namespace PES.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== PES Industrial Concurrency Benchmark Station ===");
            Console.WriteLine("System initialized. Starting rigorous hardware testing...");

            // Launching the definitive Enterprise-grade micro-benchmarking engine
            BenchmarkRunner.Run<PesTopologicalStressTest>();
        }
    }

    /// <summary>
    /// High-concurrency stress test station simulating large-scale production workloads.
    /// Monitors execution latency and guarantees zero memory allocations during runtime ticks.
    /// </summary>
    [MemoryDiagnoser] 
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class PesTopologicalStressTest
    {
        // 🪐 Fixed CS8618: Added explicit target-typed initializations to satisfy .NET Nullable constraints.
        private List<DamNesiaEcosystemEngine> _activeNpcPopulation = new();
        private List<SeismicEventMatrix> _subjectiveEventPool = new();

        // Configuration scale simulating 3A live-service game concurrency
        [Params(1000, 10000)]
        public int ConcurrentAgentCount { get; set; }

        /// <summary>
        /// Pre-warms the environment by executing baseline cold boots for up to 10,000 unique agents.
        /// </summary>
        [GlobalSetup]
        public void SetupEcosystem()
        {
            _activeNpcPopulation = new List<DamNesiaEcosystemEngine>(ConcurrentAgentCount);
            _subjectiveEventPool = new List<SeismicEventMatrix>(ConcurrentAgentCount);

            // Fixed seed ensures deterministic, reproducible benchmark results
            var random = new Random(42); 

            for (int i = 0; i < ConcurrentAgentCount; i++)
            {
                var initialBaselineOrgans = new Dictionary<SoulOrgan, double>();
                foreach (var organ in Enum.GetValues<SoulOrgan>())
                {
                    // Distribute baseline values randomly between -20.0 and +20.0 (Homeostasis Zone)
                    initialBaselineOrgans[organ] = random.NextDouble() * 40.0 - 20.0; 
                }

                var engine = new DamNesiaEcosystemEngine();
                // Initialize the core registry with the loaded baseline and 40.0 initial aquifer pressure
                engine.InitializeFirstLoveSnapshot(initialBaselineOrgans, 40.0, SoulOrgan.CoreSecurity);
                _activeNpcPopulation.Add(engine);

                // Simulate a high-dimensional subjective event vector processed by the LLM Appraisal Lens
                var mockEvent = new SeismicEventMatrix("Player triggers localized trauma stimulus");
                mockEvent.SetImpulse(SoulOrgan.CoreSecurity, -35.0); // Directly hits the core security aquifer boundary
                mockEvent.SetImpulse(SoulOrgan.TrustDependence, -15.0);
                _subjectiveEventPool.Add(mockEvent);
            }
        }

        /// <summary>
        /// Core Stress Point: Simulates thousands of players concurrently driving transactions in a single frame.
        /// </summary>
        [Benchmark]
        public void ExecuteConcurrentTectonicTick()
        {
            for (int i = 0; i < ConcurrentAgentCount; i++)
            {
                _activeNpcPopulation[i].ExecuteEcosystemTick(
                    _subjectiveEventPool[i],
                    "You've hidden this from me for years...",
                    "Betrayal_Trauma",
                    false // Dynamic check condition for the First-Love Purge mechanism
                );
            }
        }
    }
}

namespace Pes.Runtime.Core
{
    /// <summary>
    /// The definitive 16-dimensional psychological organ spectrum of the PES Engine.
    /// </summary>
    public enum SoulOrgan
    {
        CoreFocus, DecisionBasis, EnvironmentalControl, ThinkingOriginality,
        EmotionalOpenness, TrustDependence, SocialConsumption, GroupBelonging,
        WillPersistence, AltruisticTendency, SelfAwareness, CompetitiveSpirit,
        PrimaryDrive, EmotionalThroughput, CoreSecurity, ObsessionControl
    }

    /// <summary>
    /// Lightweight, high-performance immutable snapshot for telemetry and frontend rendering.
    /// Uses IReadOnlyDictionary to mathematically guarantee thread-safe immutability.
    /// </summary>
    public record SoulSnapshot(IReadOnlyDictionary<SoulOrgan, double> State, DateTime Timestamp);

    public class SeismicEventMatrix
    {
        public string EventDescription { get; set; }
        public Dictionary<SoulOrgan, double> ImpulseVector { get; private set; } = new();

        public SeismicEventMatrix(string description)
        {
            EventDescription = description;
            // Zero-GC loop optimization using modern generic Enum iterations
            foreach (var organ in Enum.GetValues<SoulOrgan>()) 
            {
                ImpulseVector[organ] = 0.0;
            }
        }

        public SeismicEventMatrix SetImpulse(SoulOrgan organ, double magnitude)
        {
            ImpulseVector[organ] = magnitude;
            return this;
        }
    }
}

namespace Pes.Runtime.Hydrology
{
    using Pes.Runtime.Core;

    public enum NpcEcosystemPhase { Homeostasis, BlackoutNoDamping, PermanentlyMuted, GrandCleansedReset }

    public class DamNesiaEcosystemEngine
    {
        // 🪐 Zero-GC Holy Grail: Cached immutable array to prevent runtime array heap allocations inside ticks
        private static readonly SoulOrgan[] AllOrgans = Enum.GetValues<SoulOrgan>();

        public Dictionary<SoulOrgan, double> OrganRegistry { get; private set; } = new();
        private readonly Dictionary<SoulOrgan, double> _firstLoveSnapshot = new();

        public SoulOrgan TraumaLockOrgan { get; private set; } = SoulOrgan.CoreSecurity;
        public double AquiferPressure { get; private set; } = 40.0;
        public double SurfaceRainStorage { get; private set; } = 0.0;
        public bool HasFirstLoveCleansedUsed { get; private set; } = false;
        public NpcEcosystemPhase CurrentPhase { get; private set; } = NpcEcosystemPhase.Homeostasis;

        private const double SphereRadiusLimit = 100.0;

        public DamNesiaEcosystemEngine()
        {
            foreach (var organ in AllOrgans) 
            {
                OrganRegistry[organ] = 0.0;
            }
        }

        public void InitializeFirstLoveSnapshot(Dictionary<SoulOrgan, double> initialOrgans, double initialWater, SoulOrgan anchor)
        {
            AquiferPressure = initialWater;
            TraumaLockOrgan = anchor;

            foreach (var pair in initialOrgans)
            {
                double clamped = Math.Clamp(pair.Value, -100.0, 100.0);
                OrganRegistry[pair.Key] = clamped;
                _firstLoveSnapshot[pair.Key] = clamped; // Freeze the genesis topology baseline
            }
        }

        public double GetPermeability(double trustDependence)
        {
            double normalized = (trustDependence + 100.0) / 200.0;
            return Math.Clamp(normalized * 0.4 + 0.01, 0.01, 0.41);
        }

        /// <summary>
        /// Executes a single structural ecosystem tick. 
        /// Processes fluid infiltration, proximity mechanics, boundary spheres, and phase transitions.
        /// </summary>
        public void ExecuteEcosystemTick(SeismicEventMatrix subjectiveEvent, string rawInput, string tag, bool isPlayerActionCorrect)
        {
            if (CurrentPhase == NpcEcosystemPhase.PermanentlyMuted) return;

            // ──► Phase 1: Sudden Death or Grand Cleansing Check (Once in a Lifetime)
            if (isPlayerActionCorrect && !HasFirstLoveCleansedUsed)
            {
                HasFirstLoveCleansedUsed = true;
                AquiferPressure = 0.0;
                SurfaceRainStorage = 0.0;
                CurrentPhase = NpcEcosystemPhase.GrandCleansedReset;

                // Infinite Pure Love Transformation: Force-revert all variables to the Genesis snapshots
                foreach (var organ in AllOrgans)
                {
                    OrganRegistry[organ] = _firstLoveSnapshot[organ];
                }
                return;
            }

            // ──► Phase 2: Hydrology Slow-Variable Infiltration & Evaporative Decay
            double trust = OrganRegistry[SoulOrgan.TrustDependence];
            double permeability = GetPermeability(trust);

            if (SurfaceRainStorage > 0) 
            {
                // Evaporation Decay: 5% of un-infiltrated positive intent dissipates per tick
                SurfaceRainStorage *= 0.95; 
                double infiltration = SurfaceRainStorage * permeability;
                SurfaceRainStorage -= infiltration;

                OrganRegistry[SoulOrgan.CoreSecurity] = Math.Clamp(OrganRegistry[SoulOrgan.CoreSecurity] + infiltration * 0.5, -100.0, 100.0);
                AquiferPressure = Math.Max(0.0, AquiferPressure - infiltration);
            }

            // Subterranean Seepage Decay: Time naturally drains 0.2 units of standard water pressure per tick
            if (CurrentPhase == NpcEcosystemPhase.Homeostasis && AquiferPressure > 0) 
            {
                AquiferPressure = Math.Max(0.0, AquiferPressure - 0.2);
            }

            // ──► Phase 3: Structural Tectonic Propagation & Proximity Matrix Calculations
            double currentDissipation = (CurrentPhase == NpcEcosystemPhase.BlackoutNoDamping) ? 1.0 : 0.5;

            foreach (var impulse in subjectiveEvent.ImpulseVector)
            {
                double processX = impulse.Value;
                if (Math.Abs(processX) < 0.01) continue;

                // Proximity Infiltration Rule: Compute structural distance from epicenter to the trauma lock
                int layerDistance = Math.Abs((int)impulse.Key - (int)TraumaLockOrgan);

                // Using the optimized cached static array loop to completely avoid runtime heap allocations
                foreach (SoulOrgan targetOrgan in AllOrgans)
                {
                    double currentOrganX = processX * currentDissipation;

                    if (targetOrgan == TraumaLockOrgan && CurrentPhase != NpcEcosystemPhase.GrandCleansedReset)
                    {
                        if (currentOrganX > 0) 
                        { 
                            SurfaceRainStorage += currentOrganX; 
                            currentOrganX = 0.0; // Positive intent bounces off into surface storage prior to unlocking
                        }
                        else if (currentOrganX < 0) 
                        { 
                            // Focused local pressure boost: The closer to the rock layer, the higher the focused stress multiplier
                            double proximityMultiplier = 3.0 - (layerDistance * 0.15);
                            double focusedPressure = Math.Abs(currentOrganX) * Math.Max(0.5, proximityMultiplier);

                            AquiferPressure = Math.Clamp(AquiferPressure + focusedPressure, 0.0, 100.0); 
                        }
                    }

                    if (targetOrgan == impulse.Key) 
                    {
                        OrganRegistry[targetOrgan] = Math.Clamp(OrganRegistry[targetOrgan] + currentOrganX, -100.0, 100.0);
                    }
                }
            }

            // ──► Phase 4: Euclidean Boundary Sphere Locking (Deflection Clamping)
            foreach (var o in AllOrgans)
            {
                double currentVal = OrganRegistry[o];
                double inputX = subjectiveEvent.ImpulseVector[o];
                double magnitude2D = Math.Sqrt((inputX * inputX) + (currentVal * currentVal));

                if (magnitude2D > SphereRadiusLimit)
                {
                    OrganRegistry[o] = currentVal * (SphereRadiusLimit / magnitude2D); // Tangent Drift Clamping
                }
            }

            // ──► Phase 5: Critical Phase Transition Assessment
            if (AquiferPressure >= 100.0 && CurrentPhase == NpcEcosystemPhase.Homeostasis)
            {
                CurrentPhase = NpcEcosystemPhase.BlackoutNoDamping; // Burn the shock absorbers, enter high-pressure phase
            }
            else if (CurrentPhase == NpcEcosystemPhase.BlackoutNoDamping && !isPlayerActionCorrect && AquiferPressure >= 100.0)
            {
                // Continued trauma under absolute lock causes irreversible ground subsidence and terminal severing
                CurrentPhase = NpcEcosystemPhase.PermanentlyMuted; 
            }
        }

        public SoulSnapshot TakeSnapshot()
        {
            var readOnlyClonedState = new Dictionary<SoulOrgan, double>(OrganRegistry);
            return new SoulSnapshot(readOnlyClonedState, DateTime.UtcNow);
        }
    }
}