using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common.Scripts {
    public abstract class Manager : MonoBehaviour {

        // Variables to not be destroyed
        public int score;
        public int[] rating;
        private string _scene;

        // Use this variable to determine
        // If components need to be process
        protected bool IsGameOver = false;

        private void Awake() {
            _scene = SceneManager
                .GetActiveScene().name;
            DontDestroyOnLoad(gameObject);
        }

        public int GetRating() {
            var min = 0;

            for (var i = 0; i < rating.Length; i++) {
                var max = rating[i];
                if (score > min && score < max)
                    return i;
                min = rating[i];
            }

            return 0;
        }

        protected void GameOver() {
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
    }
}
