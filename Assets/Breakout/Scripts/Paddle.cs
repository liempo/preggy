using Common.Scripts;
using UnityEngine;
using static Common.Scripts.Utilities.Controls;

namespace Breakout.Scripts {
    public class Paddle : MonoBehaviour {
        // Public members to be modified in the Inspector
        public float xMin = -2;
        public float xMax = 2;
        public float speed = 10;
        private Vector3 _initial;
        private Manager _manager;

        // Private members for components
        private Rigidbody2D _rb;

        private void Start() {
            _rb = GetComponent<Rigidbody2D>();
            _manager = FindObjectOfType<Manager>();
            _initial = transform.position;
        }

        private void FixedUpdate() {
            // Do not move paddle if game not started
            if (!_manager.isGameRunning) {
                transform.position = _initial;
                return;
            }

            // Move Paddle, A or D, Left or Right
            var inputX = GetHorizontalAxisFromCenter();
            _rb.velocity = new Vector2(inputX * speed, 0f);
            _rb.position = new Vector2(Mathf.Clamp(
                _rb.position.x, xMin, xMax), -4f);
        }
    }
}
