using System;
using Common.Scripts.Utilities;

namespace Menu.Scripts {
    [Serializable]
    public class Result {
        public string title;
        public string content;

        public static Result[] Serialize(string jsonString) {
            return JsonHelper.FromJson<Result>(jsonString);
        }

    }
}
