using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common.Scripts;
using UnityEngine;
using UnityEngine.Events;

namespace Match.Scripts {
    public class MatchClicker : MonoBehaviour {

        private Camera _camera;
        private MatchSpawner _spawner;
        private Manager _manager;

        public UnityEvent onClicked;
        public List<MatchItem> clicked;

        private void Start() {
            _camera = Camera.main;
            _spawner = GetComponent<MatchSpawner>();
            _manager = FindObjectOfType<Manager>();
        }

        private void Update() {
            if (_manager.isGameRunning && _spawner.hidden)
                Click();

            if (clicked.Count == 2) {
                if (clicked.All(x => !x.isBackActive)) {
                    Debug.Log("Not ready");
                    return;
                }

                // Get first item
                var item = clicked[0].item;
                var isMatching = clicked.All(x => x.item == item);

                if (isMatching)
                    _manager.score += 50;
                else _manager.lives--;

                clicked.Clear();
                StartCoroutine(OnClickInvoke());
            }
        }

        private void Click() {
            var isTouching = Input.touchCount == 1;
            var isClicking = Input.GetMouseButtonDown(0);

            if (!isTouching && !isClicking)
                return;

            if (clicked.Count >= 2)
                return;

            // Get the position of the click
            Vector2 position; if (isTouching) {
                var touch = Input.GetTouch(0);

                // Check if moving
                if (touch.phase == TouchPhase.Began)
                    position = touch.position;
                else return;
            } else {
                position = Input.mousePosition;
            }

            // Convert to world point
            position = _camera.ScreenToWorldPoint(position);

            // Check for hits using raycast
            var hit = Physics2D.Raycast(position,
                _camera.transform.forward);

            // Exit if no collider and rigidbody present
            if (hit.collider == null || hit.rigidbody == null)
                return;
            // Exit if hit is not a spawnable object
            if (!hit.collider.CompareTag("Spawnable"))
                return;

            // Check type of clicked item
            var spawnable = hit.rigidbody.gameObject
                .GetComponent<MatchItem>();
            Debug.Log(spawnable.item);

            // Start flipping animation
            spawnable.StartFlip();
            clicked.Add(spawnable);

        }

        private IEnumerator OnClickInvoke() {
            yield return new WaitForSeconds(1f);
            onClicked?.Invoke();
        }
    }
}
