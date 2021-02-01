using UnityEngine;

namespace Menu.Scripts {
    public class Rating : MonoBehaviour {

        private Animator[] _stars;
        private static readonly int Fill
            = Animator.StringToHash("Fill");

        private void Awake() {
            // Initialize image components (child)
            _stars = new Animator[transform.childCount];
            for (var i = 0; i < transform.childCount; i++) {
                var child = transform.GetChild(i);
                _stars[i] = child.GetComponent<Animator>();
            }
        }

        public void SetRating(int rating) {
            for (var i = 0; i < rating + 1; i++) {
                _stars[i].SetTrigger(Fill);
            }
        }
    }
}
