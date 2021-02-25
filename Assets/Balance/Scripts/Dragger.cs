using Common.Scripts.Spawning;
using UnityEngine;

namespace Balance.Scripts {
    public class Dragger : Spawnable {

        private Camera _camera;
        private Rigidbody2D _selected;

        private void Start() {
            _camera = Camera.main;
        }

        private void FixedUpdate() {
            Drag();
        }

        // Implement touch to drag
        private void Drag() {

            // Get bool vars
            var isTouching = Input.touchCount == 1;
            var isClicking = Input.GetMouseButton(0);

            // Return if not touched or not clicked
            if (!isTouching && !isClicking) {
                if (_selected != null) {
                    _selected.isKinematic = false;
                    _selected = null;
                }
                return;
            }

            // Get position of drag
            Vector2 position; if (isTouching) {
                var touch = Input.GetTouch(0);

                // Check if moving
                if (touch.phase == TouchPhase.Began)
                    position = touch.position;
                else return;
            } else {
                position = Input.mousePosition;
            }

            // Convert position to world point
            position = _camera.ScreenToWorldPoint(position);

            // If something is still selected drag it
            if (_selected != null) {
                _selected.isKinematic = true;
                _selected.position = new Vector2(
                    position.x, position.y);
            }

            // If nothing selected check for hit
            else {
                // Check for hits using raycast
                var hit = Physics2D.Raycast(position,
                    _camera.transform.forward);

                // Exit if no collider and rigidbody present
                if (hit.collider == null || hit.rigidbody == null)
                    return;
                // Exit if hit is not a spawnable object
                if (!hit.collider.CompareTag("Spawnable"))
                    return;

                // Cache selected rigidbody
                _selected = hit.rigidbody;
            }
        }
    }
}
