using System;
using Common.Scripts.Utilities;

namespace Menu.Scripts {
    [Serializable]
    public class Tip {
        public int trimester;
        public string title;
        public string content;

        public static Tip[] Serialize(string jsonString) {
            return JsonHelper.FromJson<Tip>(jsonString);
        }

    }
}
