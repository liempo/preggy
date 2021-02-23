using System;
using Common.Scripts.Utilities;

namespace Menu.Scripts {
    [Serializable]
    public class Stage {
        public string title;
        public string scene;

        public static Stage[] Serialize(string jsonString) {
            return JsonHelper.FromJson<Stage>(jsonString);
        }
    }
}
