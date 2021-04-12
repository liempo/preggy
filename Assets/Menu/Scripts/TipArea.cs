using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Menu.Scripts {
    public class TipArea : MonoBehaviour {

        public TextAsset tipsJson;

        private Tip[] _tips;
        public TextMeshProUGUI header;
        public TextMeshProUGUI title;
        public TextMeshProUGUI content;

        private bool _setTipOnFirstFrame;
        private int _cachedIndex;

        private void Start() {
            _tips = Tip.Serialize(tipsJson.text);
        }

        private void Update() {
            if (_setTipOnFirstFrame) {
                SetTip(_cachedIndex);
                _setTipOnFirstFrame = false;
            }
        }

        public void SetTip(int index) {
            if (_tips == null && !_setTipOnFirstFrame) {
                _setTipOnFirstFrame = true;
                _cachedIndex = index;
                return;
            }

            var headerText =
                _tips[index].trimester switch {
                1 => "FIRST TRIMESTER",
                2 => "SECOND TRIMESTER",
                3 => "THIRD TRIMESTER",
                _ => ""
            };

            header.text = headerText;
            title.text = _tips[index].title;
            content.text = _tips[index].content;
        }

        public void SetTip() {
            var index = Random.Range(
                0, _tips.Length);
            SetTip(index);
        }
    }
}
