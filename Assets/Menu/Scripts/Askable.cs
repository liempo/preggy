using System;
using System.Collections.Generic;
using Common.Scripts.Utilities;

namespace Menu.Scripts {

    [Serializable]
    public class Askable {
        public string question;
        public List<string> choices;
        public int answerIndex = -1;

        public static Askable[] Serialize(string jsonString) {
            return JsonHelper.FromJson<Askable>(jsonString);
        }
    }
}
