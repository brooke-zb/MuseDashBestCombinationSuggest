using Il2CppGameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCombinationSuggest.Rules.Elfin {
    internal abstract class BaseElfinRule : IRule {
        public abstract int GetID();

        public virtual float GetBaseRatio(MusicData note) {
            return 0f;
        }

        public virtual float GetBindingHeartRatio(MusicData note) {
            return 0f;
        }

        public float GetBounsRatio(MusicData note) {
            return 0f;
        }

        public virtual float GetComboRatio(int combo) {
            return 0f;
        }

        public virtual float GetFeverRatio(MusicData note) {
            return 0f;
        }
    }
}
