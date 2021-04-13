using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Menu.Scripts {
    public class ResultArea : MonoBehaviour {

        public TextAsset resultsJson;

        private Result[] _results;
        public TextMeshProUGUI title;
        public TextMeshProUGUI content;

        private void Start() {
            _results = Result.Serialize(
                resultsJson.text);
            SetResult();
        }

        private void OnEnable() {
            if (_results != null) SetResult();
        }

        private void SetResult() {
            var index = Random.Range(
                0, _results.Length);
            title.text = _results[index].title;
            content.text = _results[index].content;
        }

        public void Back() {
            SceneManager.LoadScene("Main Menu");
        }
    }
}
