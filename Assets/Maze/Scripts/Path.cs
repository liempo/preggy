using System.Collections.Generic;
using System.Linq;
using Common.Scripts;
using UnityEngine;
using UnityEngine.Events;

namespace Maze.Scripts {
    public class Path : MonoBehaviour {

        private LineRenderer _renderer;
        private EdgeCollider2D _collider;
        private Manager _manager;
        private List<Vector2> _path;

        [Header("Path Settings")]
        public new Camera camera;
        public Transform start;
        public Transform finish;
        public float lineThickness = 0.1f;
        public float straightenThreshold = 0.5f;

        [Header("Events")]
        public UnityEvent onVertexAdded;
        public UnityEvent onCollided;
        public UnityEvent onFinished;
        private void Start() {
            _manager = FindObjectOfType<Manager>();
            _renderer = GetComponent<LineRenderer>();
            _collider = GetComponent<EdgeCollider2D>();
            _path = new List<Vector2>();

            // Setup the renderer
            _renderer.useWorldSpace = false;
            _renderer.startWidth = lineThickness;
            _renderer.endWidth = lineThickness;

            // Reset the path
            Reset();
        }

        private void Update() {
            if (_manager.isGameRunning && !IsFinished()) {
                // Start processing input
                ProcessInput();
            }
            Draw();
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Maze")) {
                // Update objects
                _manager.lives--;
                onCollided?.Invoke();

                // Reset path in 1.5 secs
                Invoke(nameof(Reset), 1f);
            }
        }

        private void ProcessInput() {
            // Set a place holder that is out of this world, literally.
            var placeholder = new Vector2(-5, -5);
            var point = placeholder;

            // Prioritize touch input
            if (Input.touchCount == 1) {
                var touch = Input.GetTouch(0);
                if (touch.phase != TouchPhase.Began)
                    return;
                point = touch.position;
                _path.Add(point);
            } else if (Input.GetMouseButton(0))
                point = Input.mousePosition;

            // Add only if there's something to process
            if (point != placeholder) {
                onVertexAdded?.Invoke();

                Vector2 worldPoint = camera
                    .ScreenToWorldPoint(point);

                // Straighten the line
                if (_path.Count > 0)
                    worldPoint = Straighten(
                        _path[_path.Count - 1],
                        worldPoint,
                        straightenThreshold);
                _path.Add(worldPoint);

                // Update collider
                _collider.points = _path.ToArray();
                if (!_collider.enabled)
                    _collider.enabled = true;

                // Check if finished line is reached
                if (IsFinished()) {
                    _manager.score++;

                    onFinished?.Invoke();
                    Reset();
                }
            }
        }

        private void Draw() {
            var positions = new Vector3[_path.Count];
            for (var i = 0; i < _path.Count; i++)
                positions[i] = _path[i];
            _renderer.positionCount = _path.Count;
            _renderer.SetPositions(positions);
        }

        private void Reset() {
            // Clear path
            _path.Clear();

            // Add first position
            _path.Add(start.position);
            _renderer.positionCount = 0;

            // Disable the collider
            _collider.enabled = false;
        }

        private bool IsFinished() {
            return _path.Last().y <= finish.position.y;
        }

        private static Vector2 Straighten(
            Vector2 a, Vector2 b, float threshold) {

            var rise = b.y - a.y; // y2 - y1
            var run = b.x - a.x; // x2 - x1
            var v = b; // Moidify latest

            // Pantayin ang linya sa nakaraang puntos
            if (run > -threshold && run < threshold)
                v.x = a.x;
            else if (rise > -threshold && rise < threshold)
                v.y = a.y;

            return v;
        }
    }
}
