using UnityEngine;

namespace Menu.Scripts {
    public class MenuManager : MonoBehaviour {

        private bool _isSurveyShowed;
        private bool _shouldShowSurvey;
        public GameObject menu;
        public GameObject survey;

        private void Start() {
           _shouldShowSurvey = (!PlayerPrefs.HasKey("SurveyAnswered") ||
                                PlayerPrefs.GetInt("SurveyAnswered") != 1);
        }

        private void Update() {
            if (_shouldShowSurvey && !_isSurveyShowed) {
                menu.SetActive(false);
                survey.SetActive(true);
                _isSurveyShowed = true;
            }
        }

        public void Quit() {
            Application.Quit();
        }
    }
}
