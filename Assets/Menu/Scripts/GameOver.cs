using Common.Scripts;
using TMPro;
using UnityEngine;

namespace Game_Over.Scripts {
    public class GameOver : MonoBehaviour {

        private Manager _manager;
        private Rating _rating;
        private TextMeshProUGUI _score;

        private void Start() {
            // Get manager from previous scene
            _manager = FindObjectOfType<Manager>();

            // Initialize UI components
            _rating = FindObjectOfType<Rating>();
            _score = GameObject.Find("Score Text")
                .GetComponent<TextMeshProUGUI>();

            // Set data for UI components
            _score.text = _manager.score.ToString();
            _rating.SetRating(_manager.GetRating());
        }
    }
}
