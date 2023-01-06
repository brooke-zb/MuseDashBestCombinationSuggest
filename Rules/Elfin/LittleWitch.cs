using GameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCombinationSuggest.Rules.Elfin {
    internal class LittleWitch : BaseElfinRule {
        public static LittleWitch instance { get; } = new LittleWitch();

        public override int GetID() {
            return ElfinID.WITCH;
        }

        public override float GetFeverRatio(MusicData note) {
            return 0.2f;
        }
    }
}
