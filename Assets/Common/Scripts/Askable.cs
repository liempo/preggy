using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

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
