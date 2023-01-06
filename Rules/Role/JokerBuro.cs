using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCombinationSuggest.Rules.Role {
    internal class JokerBuro : BaseRoleRule {
        public static JokerBuro instance { get; } = new JokerBuro();

        public override int GetID() {
            return RoleID.BURO_JOKER;
        }
        public override float GetComboRatio(int combo) {
            if (combo < 60)
                return base.GetComboRatio(combo);
            if (combo < 70)
                return 0.6f;
            return 0.7f;
        }
    }
}
