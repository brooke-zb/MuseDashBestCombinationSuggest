using Il2CppGameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCombinationSuggest.Rules.Elfin {
    internal class Lilith : BaseElfinRule {
        public static Lilith instance { get; } = new Lilith();

        public override int GetID() {
            return ElfinID.LILITH;
        }

        public override float GetBaseRatio(MusicData note) {
            return 0.05f;
        }
    }
}
