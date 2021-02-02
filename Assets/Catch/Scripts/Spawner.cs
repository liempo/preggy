using System.Collections.Generic;
using Common.Scripts;
using UnityEngine;
using static Catch.Scripts.SpawnType;
using Random = UnityEngine.Random;

namespace Catch.Scripts {
    public class Spawner : MonoBehaviour {

        [Header("Spawn Item List")]
        public List<SpawnItem> good;
        public List<SpawnItem> bad;

        [Header("Spawn Settings")]
        public float chanceOfBad;
        public GameObject prefab;

        // Cache the bounds of the spawner
        private Rect _rect;

        // Get the manager to access game states
        private Manager _manager;
        private bool _spawning;

        private void Start() {
            _rect = GetComponent<RectTransform>().rect;
            _manager = FindObjectOfType<Manager>();
        }

        private void Update() {
            if (_manager.isTimerRunning && !_spawning) {
                InvokeRepeating(nameof(Spawn),
                    1, 1);
                _spawning = true;
            }
        }

        private void Spawn() {
            // First, generate the position randomly
            var position = new Vector3(
                Random.Range(_rect.xMin, _rect.xMax),
                Random.Range(_rect.yMin, _rect.yMax));
            position += transform.position;

            // Randomize if good or bad, by chance
            var chance = Random.Range(0f, 1f);
            var type = (chance < chanceOfBad) ?
                Bad : Good;
            var item = (type == Bad) ?
                bad[Random.Range(0, bad.Count)] :
                good[Random.Range(0, good.Count)];

            // Instantiate the object
            var spawnable = Instantiate(
                prefab, position, Quaternion.identity);
            spawnable.GetComponent<Spawnable>().Set(item);
        }

    }
}
