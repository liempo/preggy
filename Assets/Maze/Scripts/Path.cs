using System.Collections.Generic;
using Common.Scripts;
using UnityEngine;
using UnityEngine.Events;

namespace Maze.Scripts {
    public class Path : MonoBehaviour {

        private LineRenderer _renderer;
        private EdgeCollider2D _collider;
        private Manager _manager;
        private bool _isColliding;

        [Header("Path Settings")]
        public new Camera camera;
        public Transform start;
        public List<Vector2> list;
        public float lineThickness = 0.1f;
        public float resetDelay = 1f;

        [Header("Events")]
        public UnityEvent onPathUpdated;
        public UnityEvent onCollided;
        public UnityEvent onFinished;
        private void Start() {
            _manager = FindObjectOfType<Manager>();
            _renderer = GetComponent<LineRenderer>();
            _collider = GetComponent<EdgeCollider2D>();
            list = new List<Vector2>();

            // Setup the renderer
            _renderer.useWorldSpace = false;
            _renderer.startWidth = lineThickness;
            _renderer.endWidth = lineThickness;

            // Reset the path
            Reset();
        }

        private void Update() {
            Draw();
        }

        private void FixedUpdate() {
            // Check if game is running
            // and a vertex is created
            if (!_manager.isGameRunning)
                return;

            if (!_isColliding && CreateVertex())
                UpdateCollider();
        }

        private void OnTriggerEnter2D(Collider2D other) {
            _isColliding = true;

            // When path collide with maze, fail
            if (other.CompareTag("Maze")) {
                _manager.lives--;
                onCollided?.Invoke();
            }

            // When path collider with finish line, good
            else if (other.CompareTag("Finish")) {
                _manager.score++;
                onFinished?.Invoke();
            }

            Invoke(nameof(Reset), resetDelay);
        }

        private bool CreateVertex() {
            Vector2 point;

            // Prioritize touch input
            if (Input.touchCount == 1) {
                var touch = Input.GetTouch(0);
                if (touch.phase != TouchPhase.Began)
                    return false;
                point = touch.position;
                list.Add(point);
            } else if (Input.GetMouseButton(0))
                point = Input.mousePosition;

            // Return if not input
            else return false;

            // Now that there's something to process
            Vector2 worldPoint = camera
                .ScreenToWorldPoint(point);

            // Straighten the line
            // if (_path.Count > 0)
            //     worldPoint = Straighten(
            //         _path[_path.Count - 1],
            //         worldPoint,
            //         straightenThreshold);

            // Finally add it to our path list
            list.Add(worldPoint);
            onPathUpdated?.Invoke();

            return true;
        }

        private void UpdateCollider() {
            // Update collider
            _collider.points = list.ToArray();
            if (!_collider.enabled)
                _collider.enabled = true;
        }

        private void Draw() {
            var positions = new Vector3[list.Count];
            for (var i = 0; i < list.Count; i++)
                positions[i] = list[i];
            _renderer.positionCount = list.Count;
            _renderer.SetPositions(positions);
        }

        private void Reset() {
            // Clear path
            list.Clear();

            // Add first position
            list.Add(start.position);
            _renderer.positionCount = 0;

            // Disable the collider
            _collider.enabled = false;
            _isColliding = false;
        }

    }
}
