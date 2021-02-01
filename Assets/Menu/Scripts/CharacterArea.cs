using Michsky.UI.ModernUIPack;
using TMPro;
using UnityEngine;

namespace Menu.Scripts {
    public class CharacterArea : MonoBehaviour {

        private void Start() {
            // Set default selection to first index
            GetComponentInChildren<HorizontalSelector>()
                .defaultIndex = 0;
        }

        private void OnEnable() {
            // Load PlayerPrefs
            if (PlayerPrefs.HasKey("Name"))
                GetComponentInChildren<TMP_InputField>()
                    .text = PlayerPrefs.GetString("Name");

            if (PlayerPrefs.HasKey("Character")) {
                var selector = GetComponentInChildren
                    <HorizontalSelector>();
                var character = PlayerPrefs
                    .GetString("Character");

                var selected = 0;
                for (var i = 0; i < selector.itemList.Count; i++) {
                    var item = selector.itemList[i];

                    if (item.itemTitle == character)
                        selected = i;
                }
                selector.index = selected;
            } else PlayerPrefs.SetString(
                "Character", "Angie");
        }
    }
}
