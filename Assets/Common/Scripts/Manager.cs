using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common.Scripts {
    public class Manager : MonoBehaviour {

        [Header("Panels")]
        public GameObject hudPanel;
        public GameObject pausePanel;

        [Header("Manager Settings")]
        public int score;
        public int lives = 3;
        public bool isTimerRunning;
        public float timeRemaining = 60f;

        [Header("Rating per Star")]
        public int[] rating = new int[3];

        // Save scene name for Retry()
        private string _scene;

        // Control variables
        private bool _isGameOver;

        // Other game objects and components
        private TextMeshProUGUI _hudLivesText;
        private TextMeshProUGUI _hudScoreText;
        private TextMeshProUGUI _hudTimerText;
        private GameObject _unpausedGameObject;

        private void Awake() {
            _scene = SceneManager
                .GetActiveScene().name;
            DontDestroyOnLoad(gameObject);
        }

        private void Start() {
            _unpausedGameObject = hudPanel.transform
                .Find("Unpaused Text").gameObject;
            _hudScoreText = hudPanel
                .transform.Find("Score Text")
                .GetComponent<TextMeshProUGUI>();
            _hudTimerText = hudPanel
                .transform.Find("Timer Text")
                .GetComponent<TextMeshProUGUI>();
            _hudLivesText = _unpausedGameObject
                .GetComponentInChildren<TextMeshProUGUI>();
        }

        private void Update() {
            // Skip method if IsGameOver
            if (_isGameOver)
                return;

            // Update the HUD components
            _hudScoreText.text = score.ToString()
                .PadLeft(4, '0');
            _hudTimerText.text = "T-" + ((int) timeRemaining).ToString()
                .PadLeft(2, '0');
            _hudLivesText.text = lives + " lives left.\nTouch to start.";
            _unpausedGameObject.SetActive(!isTimerRunning);

            // Check for time remaining
            // and tick the timer
            if (isTimerRunning) {
                if (timeRemaining > 0)
                    timeRemaining -= Time.deltaTime;
                else GameOver();
            }

            // Check for lives remaining
            if (lives <= 0)
                GameOver();

            // Game not running check (interact to run)
            // and if Game Over panel is not active
            if (IsInteracted() && !isTimerRunning) {
                isTimerRunning = true;
            }
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

        public void SetPause(bool value) {
            // Toggle HUD visibility
            hudPanel.SetActive(!value);
            pausePanel.SetActive(value);

            if (value && isTimerRunning) {
                isTimerRunning = false;
            }
        }

        public void Retry() {
            Destroy(gameObject);
            SceneManager.LoadScene(_scene);
        }

        public void Quit() {
            Destroy(gameObject);
            SceneManager.LoadScene("Main Menu");
        }

        private void GameOver() {
            _isGameOver = true;
            SceneManager.LoadScene("Game Over");
        }

        // Checks if player interacted
        private static bool IsInteracted() {
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
