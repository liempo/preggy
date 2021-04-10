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

        private void Start() {
            _tips = Tip.Serialize(tipsJson.text);
            SetTip();
        }

        private void OnEnable() {
            if (_tips != null) SetTip();
        }

        private void SetTip() {
            var index = Random.Range(
                0, _tips.Length);

            header.text = _tips[index].header;
            title.text = _tips[index].title;
            content.text = _tips[index].content;
        }
    }
}
