using System.Collections.Generic;
using UnityEngine;

namespace Jump.Scripts {
    public class Rope : MonoBehaviour {

        private LineRenderer _renderer;
        private List<Segment> _segments;

        [Header("Rope Anchors")]
        public Transform startAnchor;
        public Transform endAnchor;
        public float anchorSpeed;
        public Vector2 moveTarget = new Vector3(0f, 1f);
        private Vector2 _startAnchorOrigin;
        private Vector2 _endAnchorOrigin;

        [Header("Rope Configuration")]
        // lineThickness: Width of the line to be rendered
        // segmentSpacing: Distance between each segment
        // segmentCount: Number of segment in the rope
        public float lineThickness = 0.1f;
        public float segmentSpacing = 0.25f;
        public int segmentCount = 35;
        public int upwardsSortOrder = 10;
        public int downwardsSortOrder;

        [Header("Rope Physics")]
        public Vector2 gravity = new Vector2(0f, -1f);
        public float ropeSpeed = 1f;
        public int constraintIterations = 50;

        [Header("Ground Collision")]
        public Collider2D ground;
        private EdgeCollider2D _collider;
        public bool isCollidingWithGround;

        private void Start() {
            _renderer = GetComponent<LineRenderer>();
            _collider = GetComponent<EdgeCollider2D>();
            _segments = new List<Segment>();

            // Setup the renderer
            _renderer.useWorldSpace = false;
            _renderer.startWidth = lineThickness;
            _renderer.endWidth = lineThickness;

            // Calculate anchor targets
            _startAnchorOrigin = startAnchor.position;
            _endAnchorOrigin = endAnchor.position;

            Build(); // <-- Start building segments
        }

        private void Update() {
            Draw();
        }

        private void FixedUpdate() {
            Simulate();
            MoveAnchors();
            UpdateCollider();
        }

        private void Build() {
            var start = _startAnchorOrigin;
            // Attach each segment to each other
            for (var i = 0; i < segmentCount; i++) {
                _segments.Add(new Segment(start));
                start.y -= segmentSpacing;
            }
        }

        private void Draw() {
            // Create an array of the current positions
            var positions = new Vector3[segmentCount];
            for (var i = 0; i < segmentCount; i++)
                positions[i] = _segments[i].Current;

            // Set position to the renderer
            _renderer.positionCount = positions.Length;
            _renderer.SetPositions(positions);
        }

        private void Simulate() {
            // Apply verlet integration to each segment
            for (var i = 0; i < segmentCount; i++) {
                var segment = _segments[i];

                // Calculate the velocity
                // before updating old position
                var v = segment.GetVelocity();

                // Update the new position
                segment.Previous = segment.Current;

                // Calculate new position
                segment.Current += v + (gravity * Time.fixedDeltaTime);

                // Save the segment
                _segments[i] = segment;
            }

            // Apply the constraints
            for (var i=0; i<constraintIterations; i++)
                ApplyConstraint();
        }

        private void ApplyConstraint() {
            // Apply anchors to first and last segments
            var start = _segments[0];
            start.Current = startAnchor.position;
            _segments[0] = start;
            var end = _segments[segmentCount - 1];
            end.Current = endAnchor.position;
            _segments[segmentCount - 1] = end;

            // Apply segment error correction
            for (var i = 0; i < segmentCount - 1; i++) {
                // Get two segments at a time
                var first = _segments[i];
                var second = _segments[i + 1];

                // Get magnitude of the two points
                // #DistanceFormula #PythagoreanTheorem
                var dist = (first.Current - second.Current).magnitude;
                var error = Mathf.Abs(dist - segmentSpacing);

                // Calculate direction (s)
                var direction = ((dist > segmentSpacing)
                    ? (first.Current - second.Current)
                    : (second.Current - first.Current)).normalized;

                // Amount to change (t)
                var change = direction * error;

                // If first index, apply calculations
                // to second segment only
                if (i == 0) {
                    second.Current += change;
                    _segments[i + 1] = second;
                } else {
                    first.Current -= change / 2f;
                    second.Current += change / 2f;

                    _segments[i] = first;
                    _segments[i + 1] = second;
                }

                // IMPORTANT!! PLEASE READ!!
                // Since Unity's colliders are not working
                // because I'm manually setting the colliders
                // points, therefore, collision events won't
                // trigger. So fuck it! I'm gonna check it myself
                // isRopeColliding = _rectangle.Contains(_segments[i].Current);
            }
        }

        private void MoveAnchors() {
            var newStartAnchorPosition = Vector3.Lerp(
                _startAnchorOrigin,
                _startAnchorOrigin + moveTarget,
                Mathf.PingPong(Time.fixedTime * anchorSpeed, 1));
            var newEndAnchorPosition = Vector3.Lerp(
                _endAnchorOrigin,
                _endAnchorOrigin + moveTarget,
                Mathf.PingPong(Time.fixedTime * anchorSpeed, 1));

            // Check if anchors are moving up or down
            // NOTE: check only one of the anchor for optimization
            _renderer.sortingOrder =
                newStartAnchorPosition.y <
                startAnchor.position.y ?
                upwardsSortOrder :
                downwardsSortOrder;

            startAnchor.position = newStartAnchorPosition;
            endAnchor.position = newEndAnchorPosition;
        }

        private void UpdateCollider() {
            // Get segments and convert to array
            var positions = new Vector2[segmentCount];
            for (var i = 0; i < segmentCount; i++)
                positions[i] = _segments[i].Current;
            _collider.points = positions;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            isCollidingWithGround = other.gameObject.CompareTag("Ground");
        }

        private void OnTriggerExit2D(Collider2D other) {
            isCollidingWithGround = !other.gameObject.CompareTag("Ground");
        }

        private struct Segment {
            public Vector2 Current;
            public Vector2 Previous;

            public Segment(Vector2 position) {
                Current = position;
                Previous = position;
            }

            public Vector2 GetVelocity() {
                return Current - Previous;
            }
        }
    }
}
