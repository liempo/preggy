using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common.Scripts {
    public abstract class Manager : MonoBehaviour {

        [Header("Manager Settings")]
        public int score;
        public int[] rating;

        // Save scene name for Retry()
        private string _scene;

        // Use this variable to determine
        // If components need to be process
        protected bool IsGameOver;

        private void Awake() {
            _scene = SceneManager
                .GetActiveScene().name;
            DontDestroyOnLoad(gameObject);
        }

        public int GetRating() {
            var min = 0;
            for (var i = 0; i < rating.Length; i++) {
                var max = rating[i];
                if (score >= min &&
                    (score <= max ||
                     i == rating.Length - 1))
                    return i;
                min = rating[i];
            }
            return 0;
        }

        protected virtual void GameOver() {
            IsGameOver = true;
            SceneManager.LoadScene("Game Over");
        }

        public void Retry() {
            Destroy(gameObject);
            SceneManager.LoadScene(_scene);
        }

        public void Quit() {
            Destroy(gameObject);
            SceneManager.LoadScene("Main Menu");
        }

        // Checks if player interacted
        protected static bool IsInteracted() {
            // Check if player touched the screen
            var isTouch = Input.touchCount > 0 &&
                          Input.GetTouch(0).phase
                          == TouchPhase.Began;
            // Check if player pressed space
            var isJump = Input.GetButtonDown("Jump");

            return isTouch || isJump;
        }
    }
}
