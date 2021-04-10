using System.Collections.Generic;
using System.Linq;
using Common.Scripts;
using Spawning.Scripts;
using UnityEngine;

namespace Balance.Scripts {
    public class Container : MonoBehaviour {

        public SpawnType type;
        public List<Spawnable> items;
        public float scoreWeight = 50f;

        public Manager manager;

        public int Count() {
            return items.Count;
        }

        public void AddScore() {
            var correct = items.Count(
                item => item.item.type == type);
            var score = ((float) correct / items.Count)
                        * scoreWeight;
            Debug.Log(type + " Score =  " + score);
            manager.score += Mathf.CeilToInt(score);
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Spawnable")) {
                var item = other.gameObject
                    .GetComponent<Spawnable>();
                if (!items.Contains(item))
                    items.Add(item);
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (other.CompareTag("Spawnable")) {
                var item = other.gameObject
                    .GetComponent<Spawnable>();
                items.Remove(item);
            }
        }
    }
}
