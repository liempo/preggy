using UnityEngine;

namespace Common.Scripts.Spawning {
    public class Spawnable : MonoBehaviour {
        // Can only be accessed with Set
        public SpawnItem item;

        public void Set(SpawnItem i) {
            GetComponent<SpriteRenderer>()
                .sprite = i.sprite;
            item = i;
        }
    }
}
