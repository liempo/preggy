using Common.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;

namespace Run.Scripts {
    public class Crate : MonoBehaviour {

        public Character character;
        public float speed = 0.5f;
        public float minimumX = -5;
        public float respawnMinX = 5f;
        public float respawnMaxX = 15f;
        private Rigidbody2D _rb;

        private Manager _manager;
        private bool _isMoving;

        private void Start() {
            _manager = FindObjectOfType<Manager>();
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update() {
            if (_manager.isGameRunning) {
                if (!_isMoving) {
                    _rb.velocity = Vector2.left
                                   * (speed * Time.time);
                    _isMoving = true;
                }
            }

            else _rb.velocity = Vector2.zero;

            if (_rb.position.x < minimumX) {
                Respawn();
                _manager.score ++;
            }
        }

        private void Respawn() {
            _rb.position = new Vector2(
                Random.Range(respawnMinX, respawnMaxX),
                _rb.position.y);
        }

        private void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.CompareTag("Player")) {
                Respawn();
                var oldCharPos = other.rigidbody.position;
                other.rigidbody.position = new Vector2(0, oldCharPos.y);

                _manager.lives--;
                _manager.SetPause(true, false);
            }
        }
    }
}
