using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Spawning.Scripts {
    public class Spawner : MonoBehaviour {

        public List<SpawnItem> items;
        public GameObject spawnablePrefab;
        public float chanceOfSpawningBad = 0.5f;

        // Get required components
        private Rect _spawnArea;

        protected virtual void Start() {
            _spawnArea = GetComponent<RectTransform>().rect;
        }

        protected SpawnItem GetRandomItem() {
            var chance = Random.Range(0f, 1f);
            var type = chance < chanceOfSpawningBad ?
                SpawnType.Bad : SpawnType.Good;
            return GetRandomItemOfType(type);
        }

        protected SpawnItem GetRandomItemOfType(SpawnType type) {
            var array = items.Where(
                i => i.type == type).ToArray();
            return array[Random.Range(0, array.Length)];
        }

        protected Spawnable Spawn() {
            return Spawn(GetRandomItem());
        }

        protected Spawnable Spawn(SpawnItem item, Vector3 position) {
            // Instantiate the object
            var obj = Instantiate(
                spawnablePrefab, position, Quaternion.identity);
            var spawnable = obj.GetComponent<Spawnable>();
            spawnable.Set(item);
            return spawnable;
        }

        protected Spawnable Spawn(SpawnItem item) {
            // First, generate the position randomly
            var position = new Vector3(
                Random.Range(_spawnArea.xMin, _spawnArea.xMax),
                Random.Range(_spawnArea.yMin, _spawnArea.yMax));
            position += transform.position;
            return Spawn(item, position);
        }
    }
}
