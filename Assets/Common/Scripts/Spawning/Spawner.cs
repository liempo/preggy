using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common.Scripts.Spawning {
    public class Spawner : MonoBehaviour {

        public List<SpawnItem> items;
        public GameObject spawnablePrefab;
        public float chanceOfSpawningBad = 0.5f;

        // Get required components
        private Rect _spawnArea;

        protected virtual void Start() {
            _spawnArea = GetComponent<RectTransform>().rect;
        }

        protected void Spawn() {
            // No item is passed, generate item using params
            var chance = Random.Range(0f, 1f);
            var type = chance < chanceOfSpawningBad ?
                SpawnType.Bad : SpawnType.Good;

            // Generate item with type
            var array = items.Where(
                i => i.type == type).ToArray();
            var item = array[Random.Range(0, array.Length)];

            Spawn(item);
        }

        protected void Spawn(SpawnItem item, Vector3 position) {
            // Instantiate the object
            var spawnable = Instantiate(
                spawnablePrefab, position, Quaternion.identity);
            spawnable.GetComponent<Spawnable>().Set(item);
        }

        protected void Spawn(SpawnItem item) {
            // First, generate the position randomly
            var position = new Vector3(
                Random.Range(_spawnArea.xMin, _spawnArea.xMax),
                Random.Range(_spawnArea.yMin, _spawnArea.yMax));
            position += transform.position;
            Spawn(item, position);
        }
    }
}
