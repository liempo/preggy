using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Menu.Scripts {
    public class Loader : MonoBehaviour {

        private bool _activate;

        [Header("Events")]
        public UnityEvent onLoadStart;
        public UnityEvent onLoadFinished;

        public void StartLoader(string sceneName) {
            onLoadStart?.Invoke();
            StartCoroutine(Load(sceneName));
        }

        public void Activate() {
            _activate = true;
        }

        private IEnumerator Load(string sceneName) {
            var operation = SceneManager
                .LoadSceneAsync(sceneName);
            operation.allowSceneActivation = false;

            while (!operation.isDone) {
                if (operation.progress >= 0.9f) {
                    onLoadFinished?.Invoke();

                    // Wait for activation
                    operation.allowSceneActivation = _activate;
                }

                yield return null;
            }
        }


    }
}
