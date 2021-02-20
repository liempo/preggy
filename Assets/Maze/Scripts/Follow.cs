using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Maze.Scripts {
    public class Follow : MonoBehaviour {

        public Path path;
        public float followSpeed = 3f;
        public int followOnCount;
        private Vector2 _initial;
        private int _index;

        private void Start() {
            _initial = transform.position;
        }

        private void FixedUpdate() {
            Move();
        }

        [SuppressMessage("ReSharper",
            "Unity.InefficientPropertyAccess")]
        private void Move() {
            var count = path.list.Count;
            if (count <= 1) {
                Reset();
                return;
            }
            if (_index >= count)
                return;
            if (count < followOnCount)
                return;

            transform.position = Vector2.MoveTowards(
                transform.position,
                path.list[_index],
                followSpeed * Time.fixedDeltaTime);

            var distance = Vector2.Distance(
                transform.position,
                path.list[_index]);
            if (distance < 0.01) _index++;
        }

        private void Reset() {
            transform.position = _initial;
            _index = 0;
        }
    }
}
