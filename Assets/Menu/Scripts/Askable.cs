using System;
using System.Collections.Generic;
using Common.Scripts.Utilities;

namespace Menu.Scripts {

    [Serializable]
    public class Askable {
        public string question;
        public List<string> choices;

        public static Askable[] Serialize(string jsonString) {
            return JsonHelper.FromJson<Askable>(jsonString);
        }
    }
}
