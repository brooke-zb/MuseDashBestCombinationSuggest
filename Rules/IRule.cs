using GameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCombinationSuggest.Rules {
    public interface IRule {
        int GetID();

        float GetComboRatio(int combo);

        float GetBaseRatio(MusicData note);

        float GetBindingHeartRatio(MusicData note);

        float GetBounsRatio(MusicData note);

        float GetFeverRatio(MusicData note);
    }
}
