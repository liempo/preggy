using Common.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Menu.Scripts {
    public class QuestionArea : MonoBehaviour {

        // To be set in the Inspector
        public GameObject button;
        public TextAsset json;
        public bool askOnce;
        public UnityEvent onClick;
        public string endMessage;

        // Objects and components
        private TextMeshProUGUI _questionText;
        private GameObject _choicesArea;

        // Flow and data
        private int _index = -1;
        private Askable[] _items;

        private void Start() {
            // Initialize the survey askables
            _items = Askable.Serialize(json.text);

            // Find and initialize survey's objects
            _choicesArea = GameObject.Find("Choices Area");
            _questionText = GameObject.Find("Question Text")
                .GetComponent<TextMeshProUGUI>();

            if (askOnce) {
                // Randomize the question
                _index = Random.Range(0, _items.Length);
                Next();
            }
        }

        public void Next() {
            // Kill all younglings anakin, all of them
            foreach (Transform child in _choicesArea.transform)
                Destroy(child.gameObject);

            // if index will go out of bounds clear choices and set text
            if (_index == _items.Length - 1) {
                _questionText.text = endMessage;
                return;
            }

            // Increment to next index
            _index++;

            // Change question text
            _questionText.text = _items[_index].question;

            // Create the choices buttons
            foreach (var choice in _items[_index].choices) {
                // Instantiate button prefab
                var b = Instantiate(
                    button, _choicesArea.transform);
                b.GetComponentInChildren<
                    TextMeshProUGUI>().text = choice;

                b.GetComponent<Button>()
                    .onClick.AddListener(onClick.Invoke);
            }
        }
    }
}
