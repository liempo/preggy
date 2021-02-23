using Common.Scripts;
using Common.Scripts.Spawning;

namespace Catch.Scripts {
    public class CatchSpawner : Spawner {

        private Manager _manager;
        private bool _spawning;

        protected override void Start() {
            base.Start();

            // Implement manager here
            _manager = FindObjectOfType<Manager>();
        }

        private void Update() {
            if (_manager.isGameRunning && !_spawning) {
                InvokeRepeating(nameof(Spawn),
                    1, 1);
                _spawning = true;
            }
        }
    }
}
