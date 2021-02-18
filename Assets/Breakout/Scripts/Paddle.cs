using Common.Scripts;
using UnityEngine;
using static Common.Scripts.Controls;

namespace Breakout.Scripts {
    public class Paddle : MonoBehaviour {
        // Public members to be modified in the Inspector
        public float xMin;
        public float xMax;
        public float speed;
        private Vector3 _initialPosition;
        private Manager _manager;

        // Private members for components
        private Rigidbody2D _rb;

        private void Start() {
            _rb = GetComponent<Rigidbody2D>();
            _manager = FindObjectOfType<Manager>();
            _initialPosition = transform.position;
        }

        private void FixedUpdate() {
            // Do not move paddle if game not started
            if (!_manager.isGameRunning) {
                transform.position = _initialPosition;
                return;
            }

            // Move Paddle, A or D, Left or Right
            var inputX = GetHorizontalAxisFromCenter();

            _rb.velocity = new Vector2(inputX * speed, 0f);

            // Lock to boundary
            _rb.position = new Vector2(Mathf.Clamp(
                _rb.position.x, xMin, xMax), -4f);
        }
    }
}
