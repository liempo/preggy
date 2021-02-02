using System;
using UnityEngine;

namespace Catch.Scripts {

    [Serializable]
    public class SpawnItem {
        public Sprite sprite;
        public SpawnType type;
    }

    public enum SpawnType { Good, Bad }
}
