using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;
using static Common.Scripts.Utilities.Controls;

namespace Common.Scripts {
    public class Character : MonoBehaviour {

        // To be shown in the inpsector
        public float speed = 1f;
        public float jumpForce = 5f;
        public bool isJumping;
        public bool jumpEnabled;
        public bool runEnabled;
        public bool centerMode;
        public bool runAnimationAlwaysEnabled;

        // Internal variables
        private Rigidbody2D _rb;
        private Animator _animator;
        private static readonly int IsRunning
            = Animator.StringToHash("IsRunning");
        private static readonly int IsJumping
            = Animator.StringToHash("isJumping");

        private void Start() {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();

            // Swap character skin if set
            if (PlayerPrefs.HasKey("Character"))
                Swap(PlayerPrefs.
                    GetString("Character"));
        }

        private void FixedUpdate() {
            if (runEnabled)
                Run();
            else _animator.SetBool(IsRunning,
                runAnimationAlwaysEnabled);
            if (jumpEnabled)
                Jump();
        }

        private void Run() {
            // Initially, do not set it to running
            _animator.SetBool(IsRunning, false);

            // Check for horizontal input
            var inputX = centerMode ?
                GetHorizontalAxisFromCenter() :
                Input.GetAxis("Horizontal");

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


        private void Jump() {
            // If didn't jump, or is jumping, skip
            if (_animator.GetBool(IsJumping))
                return;
            if (!Input.GetButton("Jump") && Input.touchCount != 1)
                return;

            isJumping = true;
            _animator.SetBool(IsJumping, true);

            var jumpVelocity = new Vector2(0, jumpForce);
            _rb.velocity = Vector2.zero;
            _rb.AddForce(jumpVelocity, ForceMode2D.Impulse);
        }

        private void OnCollisionEnter2D() {
            _animator.SetBool(IsJumping, false);
            isJumping = false;
        }

        public void Swap(string characterName) {
            foreach (Transform child in transform) {
                var resolver = child.GetComponent
                    <SpriteResolver>();
                if (resolver == null)
                    continue;
                resolver.SetCategoryAndLabel(
                    child.name, characterName);
            }
        }
    }
}
