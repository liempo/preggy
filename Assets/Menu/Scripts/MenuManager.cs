using UnityEngine;

namespace Menu.Scripts {
    public class MenuManager : MonoBehaviour {

        private static bool _isSurveyShowed;
        public GameObject menu;
        public GameObject survey;

        private void Update() {
            if (!_isSurveyShowed) {
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
