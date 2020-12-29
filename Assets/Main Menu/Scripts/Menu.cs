using Common.Scripts;
using UnityEngine;

namespace Main_Menu.Scripts {
    public class Menu : MonoBehaviour {

        // Panels (to be set in the Inspector)
        public GameObject main;
        public GameObject play;
        public GameObject dashboard;
        public GameObject survey;
        public GameObject settings;


        private void Start() {
            // Initialize the survey singleton
            // Survey data will be accessible
            // with Surveys.Items()
            Surveys.Load();
        }


        // Wrapper functions for `show` and `back` buttons
        public void SetPlayShowing(bool value) { Swap(main, play, value); }
        public void SetDashboardShowing(bool value) { Swap(main, dashboard, value); }
        public void SetSurveyShowing(bool value) { Swap(main, survey, value); }
        public void SetSettingsShowing(bool value) {
            Swap(main, settings, value);
        }

        private static void Swap(GameObject a, GameObject b, bool inOrder) {
            b.SetActive(!inOrder); a.SetActive(inOrder);
        }

        public void Quit() {
            Application.Quit();
        }
    }
}
