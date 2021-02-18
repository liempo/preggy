using UnityEngine;

namespace Common.Scripts {
    public class LivesUI : MonoBehaviour {

        public int count;
        public GameObject prefab;

        private GameObject[] _objects;

        private void Start() {
            SetCount(count);
        }

        public void SetCount(int value) {
            count = value;

            // Instantiate the prefab, then disable
            _objects = new GameObject[count];
            for (var i = 0; i < count; i++) {
                _objects[i] = Instantiate(
                    prefab, transform);
            }
        }

        public void SetRemaining(int value) {
            if (value > count) return;
            for (var i = 0; i < count; i++) {
                if (i < value) {
                    if (!_objects[i].activeSelf)
                        _objects[i].SetActive(true);
                } else _objects[i].SetActive(false);
            }
        }

    }
}
