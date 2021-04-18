using System.Collections.Generic;
using Spawning.Scripts;
using UnityEngine;

namespace Destroy.Scripts {
    public class DestroySpawner : Spawner {

        public List<Transform> anchors;

        protected override void Start() {
            Respawn();
        }

        public void Respawn() {
            Clear();

            // Randomize the position of the bad one
            var badPosition = Random.Range(
                0, anchors.Count);

            for (var i = 0; i < anchors.Count; i++) {
                var type = i == badPosition ?
                    SpawnType.Bad : SpawnType.Good;
                Spawn(GetRandomItemOfType(type),
                    anchors[i].position);
            }
        }
    }
}
