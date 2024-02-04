using Il2CppAssets.Scripts.PeroTools.Commons;
using Il2CppGameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCombinationSuggest.Rules.Role {
    internal class BunnyRin : BaseRoleRule {
        public static BunnyRin instance { get; } = new BunnyRin();

        public override int GetID() {
            return RoleID.RIN_BUNNY;
        }

        public override float GetBaseRatio(MusicData note) {
            if (note.noteData.type == NoteType.Hide) {
                return 2.0f;
            }
            return 0f;
        }
        public override float GetBindingHeartRatio(MusicData note) {
            return 2.0f;
        }
        public override float GetBounsRatio(MusicData note) {
            if (note.noteData.type == NoteType.Hp || note.noteData.type == NoteType.Music || note.noteData.type == NoteType.Block) {
                return 2.0f;
            }
            return 0f;
        }
    }
}
