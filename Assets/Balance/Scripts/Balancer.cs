using Common.Scripts;
using Common.Scripts.Spawning;
using UnityEngine;

namespace Balance.Scripts {
    public class Balancer : MonoBehaviour {

        public Spawner spawner;
        public Transform platform;
        public Container good;
        public Container bad;

        private Manager _manager;

        private void Start() {
            _manager = FindObjectOfType<Manager>();
        }

        private void Update() {
            Tilt();
            Check();
        }

        private void Tilt() {
            var rotation = Vector3.zero;
            if (good.Count() < bad.Count())
                rotation = new Vector3(0, 0, -2f);
            else if (good.Count() > bad.Count())
                rotation = new Vector3(0, 0, 2f);
            else rotation = Vector3.zero;
            platform.rotation = Quaternion.Euler(rotation);
        }

        private void Check() {
            if (Input.GetMouseButton(0))
                return;

            var total = good.Count() + bad.Count();
            if (spawner.items.Count == total)
                _manager.Finish();
        }
    }
}
