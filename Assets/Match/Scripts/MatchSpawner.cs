using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Spawning.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Match.Scripts {
    public class MatchSpawner : Spawner {

        public List<Transform> anchors;

        public bool respawn;
        public bool hidden;
        public float hideTime = 1f;
        private int _pos1;
        private int _pos2;

        protected override void Start() {
            // All good in the hood
            chanceOfSpawningBad = 0f;

            // Start spawning
            respawn = true;
        }

        private void Update() {
            // if should respawn
            if (respawn) {
                Clear();

                _pos1 = Random.Range(
                    0, anchors.Count);
                _pos2 = Random.Range(
                    0, anchors.Count);
                if (_pos1 == _pos2)
                    return;

                // Generate the item to match
                var matchingItem = GetRandomItem();

                // Spawn an item for every anchor
                for (var i = 0; i < anchors.Count; i++) {
                    var t = anchors[i];
                    SpawnItem item;
                    do {
                        item = GetRandomItem();
                    } while (item == matchingItem ||
                             spawned.Select(x => x.item).Contains(item));

                    if (i == _pos1 || i == _pos2)
                        Spawn(matchingItem, t.position);
                    else Spawn(item, t.position);
                }

                respawn = false;
            }
        }

        public void Respawn() {
            respawn = true;
        }

        public void StartHide() {
            Invoke(nameof(HideAll), hideTime);
        }

        private void HideAll() {
            hidden = true;
            foreach (var matchItem in spawned.Cast<MatchItem>())
                matchItem.StartFlip();
        }
    }
}
