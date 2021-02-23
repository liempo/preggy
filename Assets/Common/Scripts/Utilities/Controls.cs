using UnityEngine;

namespace Common.Scripts.Utilities {
    public static class Controls {

        /** Get the percentage of a point
         * from the center of the screen.
         * NOTE: Falls back to default keybinding if
         *       touch is not present. */
        public static float GetHorizontalAxisFromCenter() {
            var result = Input.GetAxis("Horizontal");

            if (Input.touchCount == 1) {
                var touch = Input.GetTouch(0);

                if (touch.phase != TouchPhase.Stationary)
                    return 0f;

                // inputX will be the percentage
                // on how close touchX is to the center
                var touchX =
                    (touch.position.x > (Screen.width / 2f))
                    ? touch.position.x - Screen.width / 2f
                    : touch.position.x * -1;
                result =  touchX / (Screen.width / 2f);
            }

            return result;
        }
    }
}
