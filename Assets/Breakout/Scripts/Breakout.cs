using TMPro;
using UnityEngine;

namespace Breakout.Scripts {
    public class Breakout : Common.Scripts.Manager {

        [Header("Panels")]
        public GameObject hudPanel;
        public GameObject pausePanel;

        [Header("Breakout Settings")]
        public int lives = 3;
        public float timeRemaining = 60f;

        // Control variables
        [HideInInspector] public bool isTimerRunning;
        [HideInInspector] public bool isGameRunning;

        // Other game objects and components
        private TextMeshProUGUI _hudLivesText;
        private TextMeshProUGUI _hudScoreText;
        private TextMeshProUGUI _hudTimerText;
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
            // Skip method if IsGameOver
            if (IsGameOver)
                return;

            // Update the HUD components
            _hudScoreText.text = score.ToString()
                .PadLeft(4, '0');
            _hudTimerText.text = "T-" + ((int) timeRemaining).ToString()
                .PadLeft(2, '0');
            _hudLivesText.text = lives + " lives left.\nTouch to start.";
            _unpausedGameObject.SetActive(!isGameRunning);

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
            if (IsInteracted() && !isGameRunning) {
                isTimerRunning = true;
                isGameRunning = true;
            }
        }

        public void SetPause(bool value) {
            // Toggle HUD visibility
            hudPanel.SetActive(!value);
            pausePanel.SetActive(value);

            if (value) {
                if (isGameRunning) isGameRunning = false;
                if (isTimerRunning) isTimerRunning = false;
            }
        }
    }
}
