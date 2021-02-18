using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Common.Scripts {
    public class Manager : MonoBehaviour {

        [Header("Components")]
        public InGameUI hud;
        public PauseUI pause;

        [Header("Game Settings")]
        public int score;
        public int lives;
        public float time;
        public float countdown;
        public bool isGameRunning;
        public bool isGameFinished;
        public bool isCountdownStarted;


        [Header("Game Events")]
        public UnityEvent onCountdownStarted;
        public UnityEvent onGameStarted;
        public UnityEvent onGameFinished;

        [Header("Rating per Star")]
        public int[] rating = new int[3];

        // Internal game attributes
        private string _scene;

        private void Awake() {
            _scene = SceneManager
                .GetActiveScene().name;
            DontDestroyOnLoad(gameObject);
        }

        private void Start() {
            UpdateUI();
        }

        private void FixedUpdate() {
            if (isGameFinished)
                return;

            if (isCountdownStarted) {
                // If countdown started and game is already running
                // --> Tick the down the game timer
                if (isGameRunning) {
                     if (time > 0)
                         time -= Time.fixedDeltaTime;
                     else {
                         onGameFinished?.Invoke();
                         Finish();
                     }
                }

                // If the countdown started and game is not running
                // --> Tick down the countdown timer to run game
                else {
                    if (countdown > 0)
                        countdown -= Time.fixedDeltaTime;
                    else {
                        onGameStarted?.Invoke();
                        isGameRunning = true;
                    }
                }

                // Update UI if game is started
                UpdateUI();
            }

            // If countdown is not started the player interacted
            // --> Start the countdown
            else if (IsInteracted()) {
                onCountdownStarted?.Invoke();
                isCountdownStarted = true;
            }
        }

        private void UpdateUI() {
            hud.SetScore(score);
            hud.SetLives(lives);
            hud.SetTimer((int) time);
            hud.SetCountdown((int) countdown);

            // Show only message if game hasn't started
            hud.SetMessageActive(!isCountdownStarted);
        }

        public void SetPause(bool value) {
            hud.gameObject.SetActive(!value);
            pause.gameObject.SetActive(value);

            if (value && isGameRunning) {
                // TODO invoke event
                isGameRunning = false;
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

        private void Finish() {
            isGameFinished = true;
            SceneManager.LoadScene("Game Over");
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
