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

        public void Respawn() {
            // Clear all spawned objects
            foreach (var spawnable in spawned) {
                Destroy(spawnable.gameObject);
            }
            spawned.Clear();

            // Randomize the position of the bad one
            var badPosition = Random.Range(
                0, anchors.Count);

            for (var i = 0; i < anchors.Count; i++) {
                var type = i == badPosition ?
                    SpawnType.Bad : SpawnType.Good;
                var spawnable = Spawn(
                    GetRandomItemOfType(type),
                    anchors[i].position);
                spawned.Add(spawnable);
            }
        }
    }
}
