using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Maze.Scripts {
    public class Maze : MonoBehaviour {

        public enum Difficulty { Easy, Medium, Hard }

        [Header("Maze Settings")]
        public Color colorNormal = Color.white;
        public Color colorError = Color.red;
        public Color colorFinished = Color.green;
        public float regenerateDelay = 1f;

        [FormerlySerializedAs("easy")]
        [Header("Maze List")]
        public List<Sprite> list;

        // Maze history
        // History does not repeat itself
        private readonly List<Sprite> _done
            = new List<Sprite>();

        // Required Components
        private SpriteRenderer _renderer;
        private Collider2D _collider;

        private void Start() {
            _renderer = GetComponent
                <SpriteRenderer>();
            Generate();
        }

        // Suppress expensive null check
        // since it's only being checked at Start
        // ReSharper disable Unity.PerformanceAnalysis
        private void Generate() {
            // Get a random item from list
            Sprite sprite = null;

            while (sprite == null || _done.Contains(sprite)) {
                sprite = list[Random.Range(0, list.Count)];

                // Repeat mazes when isa na lang
                if (_done.Count == list.Count - 1)
                    _done.Clear();
            }

            // Use generated sprite
            _renderer.sprite = sprite;
            _done.Add(sprite);

            // Remove old collider
            if (_collider != null)
                Destroy(_collider);

            // Re-add the polygon collider
            _collider = gameObject.AddComponent
                <PolygonCollider2D>();
        }

        public void TriggerError() {
            StartCoroutine(OnError());
        }

        public void TriggerFinished() {
            StartCoroutine(OnFinished());
        }

        [SuppressMessage("ReSharper",
            "Unity.InefficientPropertyAccess")]
        private IEnumerator OnError() {
            _renderer.color = colorError;
            yield return new WaitForSeconds(regenerateDelay);
            _renderer.color = colorNormal;
        }

        [SuppressMessage("ReSharper",
            "Unity.InefficientPropertyAccess")]
        private IEnumerator OnFinished() {
            _renderer.color = colorFinished;
            yield return new WaitForSeconds(regenerateDelay);
            _renderer.color = colorNormal;

             // Regenerate maze
             Generate();
        }
    }
}
