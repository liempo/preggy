using System.Collections.Generic;
using Common.Scripts.Spawning;
using UnityEngine;

namespace Destroy.Scripts {
    public class DestroySpawner : Spawner {

        public List<Transform> anchors;
        public List<Spawnable> spawned;

        protected override void Start() {
            Respawn();
        }

        private void Respawn() {
            // Clear all spawned objects
            foreach (var obj in spawned) {
                Destroy(obj);
                spawned.Remove(obj);
            }

            foreach (var anchor in anchors) {
                var spawnable = Spawn(
                    GetRandomItem(),
                    anchor.position);
                spawned.Add(spawnable);
            }
        }
    }
}
