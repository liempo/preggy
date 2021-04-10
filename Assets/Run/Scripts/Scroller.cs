using UnityEngine;

namespace Run.Scripts {
    public class Scroller : MonoBehaviour {

        public float speed = 1f;
        private float _length;
        private Vector2 _initial;

        private void Start() {
            _initial = transform.position;
            _length = GetComponent<SpriteRenderer>()
                .bounds.size.x;
        }

        private void Update() {
            var distance = Mathf.Repeat(
                Time.time * speed, _length);
            transform.position = _initial + Vector2.left * distance;
        }
    }
}
