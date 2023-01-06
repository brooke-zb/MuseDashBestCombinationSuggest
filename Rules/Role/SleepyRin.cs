using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCombinationSuggest.Rules.Role {
    internal class SleepyRin : BaseRoleRule {
        public static SleepyRin instance { get; } = new SleepyRin();
        public override int GetID() {
            return RoleID.RIN_SLEEP;
        }
    }
}
