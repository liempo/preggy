using System.Collections.Generic;
using System.Linq;
using Spawning.Scripts;
using UnityEngine;

namespace Match.Scripts {
    public class MatchSpawner : Spawner {

        public List<Transform> anchors;

        protected override void Start() {
            chanceOfSpawningBad = 0f;
            Respawn();
        }

        public void Respawn() {
            Clear();

            // Randomize position of the
            // two matching character
            int pos1, pos2;
            pos1 = Random.Range(
                0, anchors.Count);
            do {
                pos2 = Random.Range(
                    0, anchors.Count);
            } while (pos2 == pos1);

            // Generate the item to match
            var matchingItem = GetRandomItem();

            // Spawn an item for every anchor
            foreach (var t in anchors) {
                SpawnItem item; do {
                    item = GetRandomItem();
                } while (spawned.Select(x =>
                    x.item).Contains(matchingItem));
                Spawn(item, t.position);
            }
        }

    }
}
