using Common.Scripts;
using Michsky.UI.ModernUIPack;
using TMPro;
using UnityEngine;

namespace Menu.Scripts {
    public class CharacterArea : MonoBehaviour {

        private TMP_InputField _input;
        private HorizontalSelector _selector;
        private Character _character;

        private void Start() {
            _input = GetComponentInChildren
                <TMP_InputField>(true);
            _selector = GetComponentInChildren
                <HorizontalSelector>(true);
            _character = GetComponentInChildren
                <Character>(true);

            // Set data to components based on PlayerPrefs
            _input.text = PlayerPrefs.HasKey("Name") ?
                PlayerPrefs.GetString("Name") : "";
            _input.onEndEdit.AddListener(SetName);

            if (PlayerPrefs.HasKey("Character")) {
                var characterName = PlayerPrefs
                    .GetString("Character");

                // Find character in _selector list
                foreach (var item in _selector.itemList)
                    if (item.itemTitle == characterName)
                        _selector.index = _selector.itemList.IndexOf(item);
                _selector.UpdateUI();

                // Swap the character's skin
                _character.Swap(characterName);
            }
        }

        public void SetName(string newName) {
            PlayerPrefs.SetString("Name", newName);
        }

        public void SetCharacter(string newCharacter) {
            PlayerPrefs.SetString("Character", newCharacter);
            _character.Swap(newCharacter);
        }
    }
}
