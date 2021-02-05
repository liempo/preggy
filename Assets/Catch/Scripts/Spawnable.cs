using Common.Scripts;
using UnityEngine;
using static Catch.Scripts.SpawnType;

namespace Catch.Scripts {
    public class Spawnable : MonoBehaviour {

        public int points = 100;

        private Manager _manager;
        private SpawnType _type;

        private void Start() {
            _manager = FindObjectOfType<Manager>();
        }

        public void Set(SpawnItem item) {
            GetComponent<SpriteRenderer>()
                .sprite = item.sprite;
            _type = item.type;
        }

        private void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.CompareTag("Player"))
                if (_type == Bad)
                    _manager.lives--;
                else _manager.score += points;
            else if (other.gameObject.CompareTag("Ground"))
                if (_type == Good) {
                    _manager.lives--;
                    _manager.isTimerRunning = false;
                } else _manager.score += points;
            Destroy(gameObject);
        }
    }
}
