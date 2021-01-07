using Michsky.UI.ModernUIPack;
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

            // Manage PlayerPrefs here (on show only)
            if (PlayerPrefs.HasKey("Volume") && value) {
                GetComponentInChildren<SliderManager>()
                    .saveValue = PlayerPrefs.GetFloat("Volume");
            } else PlayerPrefs.SetFloat("Volume", 1f);
        }

        public void Quit() {
            Application.Quit();
        }
    }
}
