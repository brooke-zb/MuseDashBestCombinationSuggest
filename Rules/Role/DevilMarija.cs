using Il2CppGameLogic;

namespace BestCombinationSuggest.Rules.Role {
    internal class DevilMarija : BaseRoleRule {
        public static DevilMarija instance { get; } = new DevilMarija();

        public override int GetID() {
            return RoleID.MARIJA_DEVIL;
        }
        public override float GetBaseRatio(MusicData note) {
            return 0.25f;
        }
        public override float GetBindingHeartRatio(MusicData note) {
            return -1f;
        }
        public override float GetBounsRatio(MusicData note) {
            if (note.noteData.type == NoteType.Hp) {
                return -1f;
            }
            return 0f;
        }
    }
}
