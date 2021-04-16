using System.Collections.Generic;
using System.Linq;
using Common.Scripts;
using Michsky.UI.ModernUIPack;
using Spawning.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Parents.Scripts {
    public class ParentSpawner : MonoBehaviour {

        public GameObject buttonPrefab;
        public List<Transform> anchors;
        public List<ParentItem> items;
        public List<ParentItem> spawned;
        public List<GameObject> objects;
        private Manager _manager;

        private void Start() {
            _manager = FindObjectOfType<Manager>();
        }

        public void Spawn() {
            spawned.Clear();

            var goodPosition = Random.Range(
                0, anchors.Count);

            for (var i = 0; i < anchors.Count; i++) {
                var anchor = anchors[i];
                var type = i == goodPosition ?
                    SpawnType.Good : SpawnType.Bad;
                var array = items.Where(
                    j => j.type == type).ToArray();

                ParentItem item;
                do {
                    item = array[Random.Range(0, array.Length)];
                } while (spawned.Contains(item));

                var obj = Instantiate(
                    buttonPrefab, anchor);
                objects.Add(obj);

                var button = obj.GetComponent<ButtonManager>();
                button.buttonText = item.text;

                button.clickEvent.AddListener(delegate {
                    if (type == SpawnType.Good) {
                        _manager.score += 10;
                    } else {
                        _manager.lives--;
                        _manager.hud.SetMessage(
                            "Click only what's good for you!");
                        _manager.SetPause(true, false);
                    }
                    foreach (var o in objects)
                        Destroy(o);
                    objects.Clear();

                    Spawn();
                });
            }
        }
    }
}
