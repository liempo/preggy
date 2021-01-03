using System.Collections.Generic;
using Michsky.UI.ModernUIPack;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

namespace Main_Menu.Scripts {
    public class CharacterArea : MonoBehaviour {

        // Character's body parts
        //  - key is GameObject name and Sprite Category
        //  - value is SpriteResolver to be changed
        private Dictionary<string, SpriteResolver> _parts;

        private void Start() {
            // Set default selection to first index
            GetComponentInChildren<HorizontalSelector>()
                .defaultIndex = 0;

            // Initialize the character's body parts
            var character = GameObject.Find("Character");
            _parts = new Dictionary<string, SpriteResolver>();

            // Explicit list of body part names that is swappable.
            var parts = new[] {"Head", "Body",
                "Right Backarm", "Left Backarm"};
            foreach (var part in parts) {
                var resolver = character.transform.Find(part)
                    .GetComponent<SpriteResolver>();
                _parts.Add(part, resolver);
            }
        }

        public void Change(string characterName) {
            foreach (var item in _parts)
                item.Value.SetCategoryAndLabel(
                    item.Key, characterName);
        }
    }
}
