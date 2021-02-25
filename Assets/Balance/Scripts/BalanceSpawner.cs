using Common.Scripts.Spawning;

namespace Balance.Scripts {
    public class BalanceSpawner : Spawner {
        protected override void Start() {
            base.Start();

            // Spawn all items at once
            foreach (var item in items) { Spawn(item); }
        }
    }
}
