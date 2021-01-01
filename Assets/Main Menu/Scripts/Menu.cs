using System.Collections.Generic;
using Common.Scripts;
using Michsky.UI.ModernUIPack;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

namespace Main_Menu.Scripts {
    public class Menu : MonoBehaviour {

        /** Panels (to be set in the Inspector)     *
        /** --------------------------------------- **/
        public GameObject main;
        public GameObject play;
        public GameObject dashboard;
        public GameObject survey;
        public GameObject settings;

        /** Miscellaneous GameObjects and Components *
        /** --------------------------------------- **/

        // Character's body parts
        //  - key is GameObject name and Sprite Category
        //  - value is SpriteResolver to be changed
        private Dictionary<string, SpriteResolver> _characterParts;

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
            if (!value) return;

            // Set the initial selector of the
            // horizontal selector to first index
            settings.GetComponentInChildren<
                    HorizontalSelector>()
                .defaultIndex = 0;

            // Initialize the character's body parts
            var character = GameObject.Find("Character");
            _characterParts = new Dictionary<string, SpriteResolver>();

            // Explicit list of body part names that is swappable.
            var parts = new[] {"Head", "Body",
                "Right Backarm", "Left Backarm"};
            foreach (var part in parts) {
                var resolver = character.transform.Find(part)
                    .GetComponent<SpriteResolver>();
                _characterParts.Add(part, resolver);
            }
        }

        public void ChangeCharacter(string characterName) {
            foreach (var item in _characterParts)
                item.Value.SetCategoryAndLabel(item.Key, characterName);
        }

        public void Quit() {
            Application.Quit();
        }
    }
}
