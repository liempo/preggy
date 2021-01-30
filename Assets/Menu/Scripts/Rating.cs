using UnityEngine;
using UnityEngine.UI;

namespace Game_Over.Scripts {
    public class Rating : MonoBehaviour {

        private Image[] _stars;
        public Sprite filled;

        private void Awake() {
            // Initialize image components (child)
            _stars = new Image[transform.childCount];
            for (var i = 0; i < transform.childCount; i++) {
                var child = transform.GetChild(i);
                _stars[i] = child.GetComponent<Image>();
            }
        }

        public void SetRating(int rating) {
            Debug.Log("Rating = " + rating);
            for (var i = 0; i < rating + 1; i++) {
                _stars[i].sprite = filled;
            }
        }
    }
}
