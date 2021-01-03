using UnityEngine;

namespace Main_Menu.Scripts {
    public class Menu : MonoBehaviour {

        /** Panels (to be set in the Inspector)     *
        /** --------------------------------------- **/
        public GameObject main;
        public GameObject play;
        public GameObject dashboard;
        public GameObject survey;
        public GameObject settings;

        /** Miscellaneous GameObjects and Components *
        /** --------------------------------------- **/

        public void SetPlayShowing(bool value) {
            main.SetActive(!value);
            play.SetActive(value);
        }

        public void SetDashboardShowing(bool value) {
            main.SetActive(!value);
            dashboard.SetActive(value);
        }

        public void SetSurveyShowing(bool value) {
            main.SetActive(!value);
            survey.SetActive(value);
        }

        public void SetSettingsShowing(bool value) {
            main.SetActive(!value);
            settings.SetActive(value);
        }

        public void Quit() {
            Application.Quit();
        }
    }
}
