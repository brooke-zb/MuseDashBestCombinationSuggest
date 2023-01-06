using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCombinationSuggest.Rules.Elfin {
    internal class NoneElfin : BaseElfinRule {
        public static NoneElfin instance { get; } = new NoneElfin();
        public override int GetID() {
            return ElfinID.NONE;
        }
    }
}
