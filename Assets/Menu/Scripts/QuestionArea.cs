using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Menu.Scripts {
    public class QuestionArea : MonoBehaviour {

        public enum Type {
            Survey, PreQuiz, PostQuiz
        }

        // To be set in the Inspector
        public Type type;
        public GameObject button;
        public TextAsset json;
        public UnityEvent onClick;
        public UnityEvent onDone;

        public string startMessage;
        public string endMessage;

        // Objects and components
        private TextMeshProUGUI _questionText;
        private GameObject _choicesArea;

        // Flow and data
        private int _correct;
        private int _index = -1;
        private Askable[] _items;

        private void Start() {
            // Initialize the survey askables
            _items = Askable.Serialize(json.text);

            // Save total items count
            PlayerPrefs.SetInt("Score_Total", _items.Length);

            // Find and initialize survey's objects
            _choicesArea = GameObject.Find("Choices Area");
            _questionText = GameObject.Find("Question Text")
                .GetComponent<TextMeshProUGUI>();

            // Setup start message
            _questionText.text = startMessage;


            // Show only one random question
            if (type == Type.Survey) {
                ShowItem(_items[Random.Range(0, _items.Length)]);
            }
        }

        public void Next() {
            // Kill all younglings anakin, all of them
            foreach (Transform child in _choicesArea.transform)
                Destroy(child.gameObject);

            // if index will go out of bounds clear choices and set text
            if (_index == _items.Length - 1 && type != Type.Survey) {

                // Show score and percentage in
                var percentage = (float) _correct /
                    _items.Length * 100f;
                endMessage = endMessage + "\n\n" +
                             "Score:\n" + _correct + "/" +
                             _items.Length + " = " + percentage + "%";

                _questionText.text = endMessage;

                onDone?.Invoke();

                PlayerPrefs.SetInt("Score_" + type, _correct);
                PlayerPrefs.Save();

                return;
            }

            // Increment to next index
            _index++;

            ShowItem(_items[_index]);
        }

        private void ShowItem(Askable item) {
            // Change question text
            _questionText.text = item.question;

            // Create the choices buttons
            for (var i = 0; i < item.choices.Count; i++) {
                var choice = item.choices[i];

                // Instantiate button prefab
                var b = Instantiate(
                    button, _choicesArea.transform);
                b.GetComponentInChildren<
                    TextMeshProUGUI>().text = choice;

                var buttonClickedEvent = b.GetComponent
                    <Button>().onClick;

                buttonClickedEvent.AddListener(
                    onClick.Invoke);

                // Copy index to another variable
                var index = i;

                buttonClickedEvent.AddListener(delegate {
                    if (item.answerIndex == index)
                        _correct++;
                });
            }
        }
    }
}
