using Il2CppGameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCombinationSuggest.Rules.Elfin {
    internal class DragonGirl : BaseElfinRule {
        public static DragonGirl instance { get; } = new DragonGirl();

        public override int GetID() {
            return ElfinID.DRAGON;
        }

        public override float GetBaseRatio(MusicData note) {
            if (note.isBossNote || note.noteData.type == NoteType.Boss)
                return 0.3f;
            return 0f;
        }
    }
}
