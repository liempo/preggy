using Common.Scripts;
using Spawning.Scripts;
using UnityEngine;
using UnityEngine.Events;

namespace Destroy.Scripts {
    public class Clicker : MonoBehaviour {

        private Camera _camera;
        private Manager _manager;

        public UnityEvent onClicked;

        private void Start() {
            _camera = Camera.main;
            _manager = FindObjectOfType<Manager>();
        }

        private void Update() {
            if (_manager.isGameRunning)
                Click();
        }

        private void Click() {
            var isTouching = Input.touchCount == 1;
            var isClicking = Input.GetMouseButtonDown(0);

            if (!isTouching && !isClicking)
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
                .GetComponent<Spawnable>();
            Debug.Log(spawnable.item.type);

            if (spawnable.item.type == SpawnType.Bad) {
                _manager.score += 10;
                Destroy(hit.rigidbody.gameObject);
            } else {
                _manager.lives--;
                _manager.hud.SetMessage(
                    "Only destroy what's bad for you!");
                _manager.SetPause(true, false);
            }

            onClicked.Invoke();
        }
    }
}
