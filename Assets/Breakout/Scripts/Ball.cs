using Common.Scripts;
using UnityEngine;

namespace Breakout.Scripts {
    public class Ball : MonoBehaviour {

        public float initialVelocity = 600f;
        public int pointPerHit = 50;
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
            if (_manager.isGameRunning) {
                // Return if the ball is active
                if (!_rb.isKinematic)
                    return;

                // Else make the ball active
                _rb.isKinematic = false;
                _rb.AddForce(new Vector2(1f,
                    initialVelocity));
                _rb.gravityScale = 0.8f;
            }

            // Stop the ball if it's still active
            // and the game is not running
            else if (!_rb.isKinematic) {
                _rb.isKinematic = true;
                _rb.velocity = Vector2.zero;
                transform.position = _initial;
            }
        }

        private void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.CompareTag("Brick")) {
                Destroy(other.gameObject);
                _manager.score += pointPerHit;
            }

            else if (other.gameObject.CompareTag("Paddle")) {
                _rb.velocity = new Vector2(
                    other.rigidbody.velocity.x,
                    _rb.velocity.y);
            }

            else if (other.gameObject.CompareTag("Ground")) {
                if (_manager.lives > 0)
                    _manager.lives -= 1;
                _manager.SetPause(true, false);
            }
        }
    }
}
