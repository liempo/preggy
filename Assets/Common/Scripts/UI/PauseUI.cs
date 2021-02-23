using UnityEngine;
using UnityEngine.UI;

namespace Common.Scripts.UI {
    public class PauseUI : MonoBehaviour {

        [Header("Components")]
        public Button resume;
        public Button retry;
        public Button quit;

        private void Start() {
            var manager = FindObjectOfType<Manager>();

            // Set up button listeners
            resume.onClick.AddListener(delegate {
                manager.SetPause(false);
            });

            retry.onClick.AddListener(delegate {
                manager.Retry();
            });

            quit.onClick.AddListener(delegate {
                manager.Quit();
            });
        }
    }
}
