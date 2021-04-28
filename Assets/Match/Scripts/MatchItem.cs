using System;
using System.Collections;
using Spawning.Scripts;
using UnityEngine;

namespace Match.Scripts {
    public class MatchItem : Spawnable {

        public GameObject back;
        public bool isBackActive;
        public Vector3 rotation;
        public float rotateDuration = 1f;
        private bool _isRotating;

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
            if (_isRotating)
                yield break;
            _isRotating = true;
            var isFlipped = false;

            var currentRotation = transform.eulerAngles;
            var newRotation = currentRotation + rotation;

            var timer = 0f;
            while (timer < rotateDuration) {
                timer += Time.deltaTime;

                // Lerp the rotation
                transform.eulerAngles = Vector3.Lerp(
                    currentRotation, newRotation,
                    timer / rotateDuration);

                // Check if vector
                if (!isFlipped && Math.Abs(timer - (rotateDuration / 2f)) < 0.05f) {
                    ToggleBackVisible();
                    isFlipped = true;
                }

                yield return null;
            }

            _isRotating = false;
        }
    }
}
