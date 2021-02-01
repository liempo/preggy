using UnityEngine;

namespace Common.Scripts {
    public class Character : MonoBehaviour {

        // To be shown in the inpsector
        public float speed = 1f;

        // Internal variables
        private Animator _animator;
        private static readonly int IsRunning
            = Animator.StringToHash("IsRunning");

        private void Start() {
            _animator = GetComponent<Animator>();
        }

        private void FixedUpdate() {
            Run();
        }

        private void Run() {
            // Initially, do not set it to running
            _animator.SetBool(IsRunning, false);

            // Check for horizontal input
            // TODO Add a touch input here too
            var inputX = Input.GetAxis("Horizontal");

            // Check first if the object should transform
            if (inputX == 0)
                return;

            // Cached data (faster, 'allegedly')
            var localScale = transform.localScale;

            // Check direction and save data
            // Flip the object based on what direction the input is
            localScale.x = Mathf.Sign(inputX) * Mathf.Abs(localScale.x);
            transform.localScale = localScale;
            _animator.SetBool(IsRunning, true);

            // Move the object
            transform.position +=
                (inputX > 0 ? Vector3.right : Vector3.left)
                * (speed * Time.deltaTime);
        }
    }
}
