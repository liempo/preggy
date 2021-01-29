using TMPro;
using UnityEngine;

namespace Breakout.Scripts {
    public class Manager : MonoBehaviour {
        // Public members to be modified in the Inspector
        public GameObject hudPanel;
        public GameObject pausePanel;

        // Public members that must be accessible to others
        [HideInInspector] public int score;
        [HideInInspector] public int lives = 3;
        [HideInInspector] public float timeRemaining = 60f;
        [HideInInspector] public bool isTimerRunning;
        [HideInInspector] public bool isGameRunning;
        private TextMeshProUGUI _hudLivesText;
        private TextMeshProUGUI _hudScoreText;
        private TextMeshProUGUI _hudTimerText;

        // Text components to update
        private GameObject _unpausedGameObject;

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

        // Update is called once per frame
        // ReSharper disable once InvertIf
        private void Update() {
            // Update the HUD components
            _hudScoreText.text = score.ToString()
                .PadLeft(4, '0');
            _hudTimerText.text = "T-" + ((int) timeRemaining).ToString()
                .PadLeft(2, '0');
            _hudLivesText.text = lives + " lives left.\nTouch to start.";
            _unpausedGameObject.SetActive(!isGameRunning);

            // Timer check
            if (isTimerRunning) {
                if (timeRemaining > 0)
                    timeRemaining -= Time.deltaTime;
                else GameOver();
            }

            // Lives check
            if (lives <= 0) GameOver();

            // Game not running check (interact to run)
            // and if Game Over panel is not active
            if (IsInteracted() && !isGameRunning) {
                isTimerRunning = true;
                isGameRunning = true;
            }
        }

        public void SetPause(bool value) {
            // Toggle HUD visibility
            hudPanel.SetActive(!value);
            pausePanel.SetActive(value);
            isTimerRunning = !value;
            isGameRunning = !value;
        }

        private void GameOver() {
            // Reset the game states
            isTimerRunning = false;
            isGameRunning = false;
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
