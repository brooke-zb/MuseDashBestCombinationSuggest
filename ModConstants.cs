using Il2CppGameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCombinationSuggest {
    public static class ModConstants {
        // Build info
        public const string NAME = "BestCombinationSuggest";
        public const string DESCRIPTION = "Display max score combination(AP & AutoFever) on gameplay pause ui";
        public const string AUTHOR = "brooke_zb";
        public const string COPYRIGHT = "Created by " + AUTHOR;
        public const string VERSION = "1.0.0";

		// Constants
		public const string FONT_NAME = "Normal";
		public const string CANVAS_NAME = "BestCombination Canvas";
		public const string OBJ_NAME_STABLE = "BestCombination Stable";
        public const string OBJ_NAME_INSTABLE = "BestCombination Instable";

        public const string RESULT_PLACEHOLDER = "Loading...";
        public const int MAX_FEVER_ENERGY = 120;
        public const int HEART_SCORE = 300;
        public const int PRESSING_SCORE = 10;
        public const int MULTI_HIT_SCORE = 20;
        public const int LILITH_RECOVERY = 2;
        public const int HP_RECOVERY = 80;
        public const int DEVIL_MAX_HP = 200;
        public const int EARLY_MAX_THRESHOLD = 50;
        public const int EARLY_NORMAL_THRESHOLD = 48;
    }

	public static class RoleID {
		public const int NONE = -1;
		public const int RIN_BASS = 0;
		public const int RIN_BAD = 1;
		public const int RIN_SLEEP = 2;
		public const int RIN_BUNNY = 3;
		public const int RIN_XMAS = 13;
		public const int RIN_FOOL = 17;
		public const int BURO_PILOT = 4;
		public const int BURO_IDOL = 5;
		public const int BURO_ZOMBIE = 6;
		public const int BURO_JOKER = 7;
		public const int BURO_SAILOR = 14;
		public const int MARIJA_VIOLIN = 8;
		public const int MARIJA_MAID = 9;
		public const int MARIJA_MAGIC = 10;
		public const int MARIJA_DEVIL = 11;
		public const int MARIJA_BLACK = 12;
		public const int YUME = 15;
		public const int NEKO = 16;
		public const int REIMU = 18;
		public const int EL_CLEAR = 19;
		public const int MARIJA_SISTER = 20;
		public const int MARISA =21;
	}

	public static class ElfinID {
		public const int NONE = -2;
		public const int UNSELECT = -1;
		public const int MIO = 0;
		public const int ANGELA = 1;
		public const int THANATOS = 2;
		public const int RABOT = 3;
		public const int NURSE = 4;
		public const int WITCH = 5;
		public const int DRAGON = 6;
		public const int LILITH = 7;
		public const int PAIGE = 8;
		public const int SILENCER = 9;
    }
    public static class NoteType {
        public const uint Monster = 1;  // 常规
        public const uint Block = 2;    // 齿轮
        public const uint Press = 3;    // 长按
        public const uint Hide = 4;     // 幽灵
        public const uint Boss = 5;     // boss近战
        public const uint Hp = 6;       // HP道具
        public const uint Music = 7;    // 音符道具
        public const uint Mul = 8;      // 连击

		public static bool isNote(MusicData note) {
			return note.noteData.type >= 1 && note.noteData.type <= 8;
		}

		public static bool isLilithRecoveryNote(MusicData note) {
			return note.noteData.type switch {
				NoteType.Monster or NoteType.Hide or NoteType.Boss or NoteType.Hp or NoteType.Music => true,
				_ => false
			};
        }
    }
}
