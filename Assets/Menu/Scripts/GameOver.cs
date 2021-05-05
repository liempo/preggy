using Common.Scripts;
using Michsky.UI.ModernUIPack;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu.Scripts {
    public class GameOver : MonoBehaviour {

        private Manager _manager;
        private Rating _rating;
        private TextMeshProUGUI _score;
        public ButtonManager quit;

        private string _finalScene = "Run";

        private void Start() {
            // Get manager from previous scene
            _manager = FindObjectOfType<Manager>();

            // Initialize UI components
            _rating = FindObjectOfType<Rating>();
            _score = GameObject.Find("Score Text")
                .GetComponent<TextMeshProUGUI>();

            // Set data for UI components
            _score.text = (_manager != null) ?
                _manager.score.ToString() : "0000";
            _rating.SetRating((_manager != null) ?
                _manager.GetRating() : 0);

            // Set quit button text
            if (_manager.scene == _finalScene)
                quit.buttonText = "SHOW RESULTS";
        }

        public void Retry() {
            // If manager is not null
            // use it to retry else do nothing
            if (_manager != null) _manager.Retry();
        }

        public void Quit() {
            // If manager is not null
            // Use it to quit else quit manually
            if (_manager != null) {
                if (_manager.scene == _finalScene)
                    SceneManager.LoadScene("Results");
                else _manager.Quit();
            }
        }
    }
}
