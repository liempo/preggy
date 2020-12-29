using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Scripts {
    public class Surveys {

        // ReSharper disable once MemberCanBePrivate.Global
        public static Survey[] Items;

        public static void Load() {
            var path = Path.Combine(
                Application.streamingAssetsPath,
                "surveys.json");
            var json = File.ReadAllText(path);
            Items = JsonHelper.FromJson<Survey>(json);
        }
    }

    [Serializable]
    public class Survey {
        public string question;
        public List<string> choices;
    }
}
