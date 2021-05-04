using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Menu.Scripts.QuestionArea.Type;
using Random = UnityEngine.Random;

namespace Menu.Scripts {
    public class ResultArea : MonoBehaviour {

        public TextAsset resultsJson;

        private Result[] _results;
        public TextMeshProUGUI title;
        public TextMeshProUGUI content;
        public TextMeshProUGUI statistics;

        private void Start() {
            _results = Result.Serialize(
                resultsJson.text);
            ShowResult();
        }

        private void OnEnable() {
            if (_results != null)
                ShowResult();
        }

        private void ShowResult() {
            var index = Random.Range(
                0, _results.Length);
            title.text = _results[index].title;
            content.text = _results[index].content;
        }

        public void ShowStats() {
            // Show knowledge gain
            var preQuiz = PlayerPrefs.GetInt(
                "Score_" + PreQuiz, 0);
            var postQuiz = PlayerPrefs.GetInt(
                "Score_" + PostQuiz, 0);
            var total = PlayerPrefs.GetInt(
                "Score_Total", 0);
            var preQuizPercent = ((float) preQuiz / total) * 100f;
            var postQuizPercent = ((float) postQuiz / total) * 100f;
            var difference = postQuizPercent - preQuizPercent;

            statistics.text = "Pre-Quiz " + preQuiz + "/" + total +
                              " = " + preQuizPercent + "%\n" +
                              "Post-QUiz " + postQuiz + "/" + total +
                              " = " + postQuizPercent + "%\n" +
                              "Knowledge Gain " + (difference >= 0 ? "+" : "-") +
                              difference + "%";

        }

        public void Back() {
            SceneManager.LoadScene("Main Menu");
        }
    }
}
