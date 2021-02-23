using Common.Scripts;
using Common.Scripts.Spawning;
using UnityEngine;

namespace Catch.Scripts {
    public class CatchItem : Spawnable {

        public int points = 100;

        private Manager _manager;

        private void Start() {
            _manager = FindObjectOfType<Manager>();
        }

        private void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.CompareTag("Player"))
                if (Item.type == SpawnType.Bad) {
                    _manager.lives--;
                    _manager.hud.SetMessage("Do not catch unhealthy food!");
                    _manager.SetPause(true, false);
                }
                else _manager.score += points;
            else if (other.gameObject.CompareTag("Ground"))
                if (Item.type == SpawnType.Good) {
                    _manager.lives--;
                    _manager.hud.SetMessage("Catch healthy food!");
                    _manager.SetPause(true, false);
                } else _manager.score += points;
            Destroy(gameObject);
        }
    }
}
