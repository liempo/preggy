using System;
using System.Collections.Generic;

namespace Common.Scripts {

    [Serializable]
    public class Askable {
        public string question;
        public List<string> choices;

        public static Askable[] Serialize(string jsonString) {
            return JsonHelper.FromJson<Askable>(jsonString);
        }
    }
}
