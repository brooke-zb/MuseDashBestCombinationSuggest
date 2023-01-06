using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCombinationSuggest {
    public class SimulationResult : IComparable<SimulationResult> {
        public string CombName;
        public int score = -1;
        public bool IsStable = true;

        public int CompareTo(SimulationResult other) {
            return other.score.CompareTo(score);
        }
    }

    public class BestCombination {
        public List<SimulationResult> results = new();

        public void AddResult(SimulationResult result) {
            if (result.score > 0) {
                results.Add(result);
                results.Sort();
            }
        }

        public SimulationResult GetBestStableComb() {
            foreach (var result in results) {
                if (result.IsStable) return result;
            }
            return results.First();
        }

        public SimulationResult GetBestComb() {
            return results.First();
        }
    }
}
