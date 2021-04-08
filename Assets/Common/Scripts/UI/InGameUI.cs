using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Common.Scripts.UI {
    public class InGameUI : MonoBehaviour {

        [Header("Components")]
        public Button pause;
        public TextMeshProUGUI score;
        public LivesUI lives;
        public TextMeshProUGUI timer;
        public TextMeshProUGUI countdown;
        public TextMeshProUGUI message;

        [Header("Integer Width (Zero Padded)")]
        public int scoreWidth = 4;
        public int timerWidth = 2;
        public int countdownWidth = 1;

        [Header("Text Format")]
        public string timerFormat = "{0}";
        public string scoreFormat = "{0}";

        private void Start() {
            if (pause != null)
                pause.onClick.AddListener(delegate {
                    FindObjectOfType<Manager>()
                        .SetPause(true);
                });

            // Initially disable countdown text
            if (countdown != null)
                countdown.gameObject.SetActive(false);
        }

        public void SetScore(int value) {
            if (score == null || !score.isActiveAndEnabled)
                return;

            score.text = string.Format(scoreFormat,
                value.ToString().PadLeft(
                    scoreWidth, '0')
            );
        }

        public void SetTimer(int value) {
            if (timer == null || !timer.isActiveAndEnabled)
                return;

            timer.text = string.Format(timerFormat,
                value.ToString().PadLeft(
                    timerWidth, '0')
            );
        }

        public void SetLives(int value) {
            if (value < lives.count)
                lives.SetRemaining(value);
            else if (value > lives.count)
                lives.SetCount(value);
        }

        public void SetCountdown(int value) {
            countdown.text = value.ToString().PadLeft(
                    countdownWidth, '0');
        }

        public void SetMessage(string value) {
            if (message == null || !message.isActiveAndEnabled)
                return;
            message.text = value;
        }

        public void SetMessageActive(bool value) {
            message.gameObject.SetActive(value);
        }

        public void SetCountdownActive(bool value) {
            countdown.gameObject.SetActive(value);
        }
    }
}
