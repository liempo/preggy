using Common.Scripts;
using Michsky.UI.ModernUIPack;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main_Menu.Scripts {
    public class StageArea : MonoBehaviour {
        // To be set in the Inspector
        public GameObject button;
        public TextAsset json;

        // JSON data here (title and scene path)
        private Stage[] _stages;

        private void Start() {
            // Initialize the stage data
            _stages = Stage.Serialize(json.text);

            // Populate StageArea object
            foreach (var stage in _stages) {
                var b = Instantiate(
                    button, transform);

                // Setup instantiated button
                var manager = b.GetComponent<ButtonManager>();
                manager.buttonText = stage.title;
                manager.clickEvent.AddListener(
                    delegate {
                        SceneManager.LoadScene(stage.scene);
                    }
                );
            }
        }

    }
}
