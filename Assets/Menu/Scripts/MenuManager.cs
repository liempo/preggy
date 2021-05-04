using UnityEngine;
using UnityEngine.Serialization;

namespace Menu.Scripts {
    public class MenuManager : MonoBehaviour {

        private static bool _isQuizShowed;
        public GameObject menu;

        [FormerlySerializedAs("survey")]
        public GameObject quiz;

        private void Update() {
            if (!_isQuizShowed) {
                menu.SetActive(false);
                quiz.SetActive(true);
                _isQuizShowed = true;
            }
        }

        public void Quit() {
            Application.Quit();
        }
    }
}
