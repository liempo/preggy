using System.Diagnostics.CodeAnalysis;
using Common.Scripts;
using UnityEngine;

namespace Breakout.Scripts {
    public class Ball : MonoBehaviour {
        // Public members to be modified in the Inspector
        public float initialVelocity = 600f;
        public int pointPerHit = 50;
        private Vector3 _initialPosition;
        private Manager _manager;
        private bool _isPlaying;

        // Private members for components
        private Rigidbody2D _rb;

        private void Start() {
            _rb = GetComponent<Rigidbody2D>();
            _manager = FindObjectOfType<Manager>();
            _initialPosition = transform.position;
        }

        private void FixedUpdate() {
            // If the timer is running and the ball is not playing
            // Drop the fucking ball set it to playing
            if (_manager.isTimerRunning) {
                if (_isPlaying) return;
                _isPlaying = true;
                _rb.isKinematic = false;
                _rb.AddForce(new Vector2(1f,
                    initialVelocity));
                _rb.gravityScale = 0.8f;
            }

            // If the game is paused (probably because the ball hit the ground)
            // and the ball is still playing, stop the ball and reset position
            else if (_isPlaying && !_rb.isKinematic) {
                _isPlaying = false;
                _rb.isKinematic = true;
                _rb.velocity = Vector2.zero;
                transform.position = _initialPosition;
            }
        }


        [SuppressMessage("ReSharper", "Unity.UnknownTag")]
        private void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.CompareTag("Brick")) {
                Destroy(other.gameObject);
                _manager.score += pointPerHit;
            } else if (other.gameObject.CompareTag("Paddle")) {
                // Redirect ball to the direction of the paddle
                _rb.velocity = new Vector2(
                    other.rigidbody.velocity.x,
                    _rb.velocity.y);
            } else if (other.gameObject.CompareTag("Ground")) {
                if (_manager.lives > 0)
                    _manager.lives -= 1;
                _manager.isTimerRunning = false;
            }
        }
    }
}
