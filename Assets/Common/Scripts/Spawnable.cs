using UnityEngine;

namespace Common.Scripts {
    public abstract class Spawnable : MonoBehaviour {
        // Can only be accessed with Set
        protected SpawnItem Item;

        public void Set(SpawnItem item) {
            GetComponent<SpriteRenderer>()
                .sprite = item.sprite;
            Item = item;
        }
    }
}
