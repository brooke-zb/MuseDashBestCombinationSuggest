using Il2CppGameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCombinationSuggest.Rules.Role {
    internal abstract class BaseRoleRule : IRule {
        public abstract int GetID();

        public virtual float GetBaseRatio(MusicData note) {
            return 0f;
        }

        public virtual float GetBindingHeartRatio(MusicData note) {
            return 0f;
        }

        public virtual float GetBounsRatio(MusicData note) {
            return 0f;
        }

        public virtual float GetComboRatio(int combo) {
            if (combo < 10)
                return 0f;
            if (combo < 20)
                return 0.1f;
            if (combo < 30)
                return 0.2f;
            if (combo < 40)
                return 0.3f;
            if (combo < 50)
                return 0.4f;
            return 0.5f;
        }

        public virtual float GetFeverRatio(MusicData note) {
            return 0f;
        }
    }
}
