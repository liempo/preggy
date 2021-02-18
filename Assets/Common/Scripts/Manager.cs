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
        public float gameTime;
        public float countdownTime;
        public bool isGameRunning;
        public bool isGameFinished;
        public bool isCountdownStarted;


        [Header("Game Events")]
        public UnityEvent onCountdownStarted;
        public UnityEvent onGameStarted;
        public UnityEvent onGamePaused;
        public UnityEvent onGameResumed;
        public UnityEvent onGameFinished;

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

            // Enable event debug logs
            onCountdownStarted.AddListener(
                delegate { Debug.Log("onCountdownStarted"); });
            onGameStarted.AddListener(
                delegate { Debug.Log("onGameStarted"); });
            onGamePaused.AddListener(
                delegate { Debug.Log("onGamePaused"); });
            onGameResumed.AddListener(
                delegate { Debug.Log("onGameResumed"); });
            onGameFinished.AddListener(
                delegate { Debug.Log("onGameFinished"); });

        }

        private void Update() {
            if (IsInteracted()) {
                // If countdown is not started
                // and there still time to tick
                // --> Start countdown
                // else the game must be paused
                // --> resume the game
                if (!isCountdownStarted && !isGameRunning) {
                    if (countdownTime > 0) {
                        onCountdownStarted?.Invoke();
                        isCountdownStarted = true;
                    } else SetPause(false, false);
                }
            }
        }

        private void FixedUpdate() {
            if (isGameFinished)
                return;

            // If the countdown started
            // --> Tick down the countdown timer to run game
            if (isCountdownStarted) {
                if (countdownTime > 0)
                    countdownTime -= Time.fixedDeltaTime;
                else {
                    onGameStarted?.Invoke();
                    isGameRunning = true;
                    isCountdownStarted = false;
                }
            }

            // else if game is already running
            // --> Tick the down the game timer
            else if (isGameRunning) {
                if (gameTime > 0)
                    gameTime -= Time.fixedDeltaTime;
                else {
                    onGameFinished?.Invoke();
                    Finish();
                }
            }

            // Once the timers are settled
            // We now deal with th lives
            if (lives <= 0)
                Finish();

            // Update UI if game is started
            UpdateUI();
        }

        private void UpdateUI() {
            hud.SetScore(score);
            hud.SetLives(lives);
            hud.SetTimer((int) gameTime);
            hud.SetCountdown((int) countdownTime);

            // Show only countdown if countdown is started
            hud.SetCountdownActive(isCountdownStarted);

            // Show only message if game not running
            hud.SetMessageActive(!isGameRunning);
        }

        public void SetPause(bool value, bool withUI = true) {
            if (withUI) {
                hud.gameObject.SetActive(!value);
                pause.gameObject.SetActive(value);
            }

            if (value) {
                onGamePaused?.Invoke();
                isGameRunning = false;
                Time.timeScale = 0.05f;
            } else {
                onGameResumed?.Invoke();
                isGameRunning = true;
                Time.timeScale = 1f;
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
            onGameFinished?.Invoke();
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
            // Check if player left clicked
            var isClicked = Input.GetMouseButtonDown(0);

            return isTouch || isJump || isClicked;
        }
    }
}
