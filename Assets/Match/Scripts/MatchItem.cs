using System.Collections;
using Spawning.Scripts;
using UnityEngine;

namespace Match.Scripts {
    public class MatchItem : Spawnable {

        public GameObject back;
        public bool isBackActive;
        public Vector3 rotation;

        public void StartFlip() {
            StartCoroutine(CalculateFlip());
        }

        private void ToggleBackVisible() {
            if (isBackActive) {
                back.SetActive(false);
                isBackActive = false;
            }
            else {
                back.SetActive(true);
                isBackActive = true;
            }
        }

        private IEnumerator CalculateFlip() {
            for (var i = 0; i < 180; i++) {
                yield return null;
                transform.Rotate(rotation);

                if (i == 90) {
                    ToggleBackVisible();
                }
            }
        }
    }
}
