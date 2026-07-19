using System;
using System.Collections.Generic;
using Pes.Abstractions;

namespace Pes.Community.DataModel
{
    /// <summary>
    /// Open-source reference registry managing the 16D state space using readable managed heap collections.
    /// </summary>
    public class BaselineStateRegistry
    {
        protected readonly Dictionary<SoulOrgan, double> _state = new();
        protected const double MinLimit = -100.0;
        protected const double MaxLimit = 100.0;

        // Micro-optimization: Freeze enum reflection cost into a static baseline array
        protected static readonly SoulOrgan[] AllOrgans = Enum.GetValues<SoulOrgan>();

        public BaselineStateRegistry()
        {
            foreach (var organ in AllOrgans)
            {
                _state[organ] = 0.0; // Perfect system equilibrium cold start
            }
        }

        public virtual void LoadBaseline(Dictionary<SoulOrgan, double> baselineData)
        {
            if (baselineData == null) throw new ArgumentNullException(nameof(baselineData));
            foreach (var kvp in baselineData)
            {
                _state[kvp.Key] = Math.Clamp(kvp.Value, MinLimit, MaxLimit);
            }
        }

        public virtual void Mutate(SoulOrgan organ, double delta)
        {
            if (!_state.ContainsKey(organ)) return;
            _state[organ] = Math.Clamp(_state[organ] + delta, MinLimit, MaxLimit);
        }

        public double GetOrganValue(SoulOrgan organ)
        {
            return _state.TryGetValue(organ, out var val) ? val : 0.0;
        }

        public virtual ISoulSnapshot TakeSnapshot()
        {
            var readOnlyMap = new Dictionary<SoulOrgan, double>(_state);
            return new SoulSnapshot(readOnlyMap, DateTime.UtcNow);
        }
    }
}