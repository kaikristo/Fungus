// This code is part of the Fungus library (https://github.com/snozbot/fungus)
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

using UnityEngine;
using UnityEngine.UI;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace Fungus
{
    /// <summary>
    /// Handles number key input (1–0) for menu dialog option selection.
    /// Holds the button reference to avoid passing it every frame.
    /// </summary>
    public class MenuDialogKeyboardInput
    {
#if ENABLE_INPUT_SYSTEM
        private static readonly Key[] DigitKeys = { Key.Digit1, Key.Digit2, Key.Digit3, Key.Digit4, Key.Digit5, Key.Digit6, Key.Digit7, Key.Digit8, Key.Digit9, Key.Digit0 };
        private static readonly Key[] NumpadKeys = { Key.Numpad1, Key.Numpad2, Key.Numpad3, Key.Numpad4, Key.Numpad5, Key.Numpad6, Key.Numpad7, Key.Numpad8, Key.Numpad9, Key.Numpad0 };
#else
        private static readonly KeyCode[] AlphaKeys = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9, KeyCode.Alpha0 };
        private static readonly KeyCode[] KeypadKeys = { KeyCode.Keypad1, KeyCode.Keypad2, KeyCode.Keypad3, KeyCode.Keypad4, KeyCode.Keypad5, KeyCode.Keypad6, KeyCode.Keypad7, KeyCode.Keypad8, KeyCode.Keypad9, KeyCode.Keypad0 };
#endif
        private const int KeyCount = 10;

        private readonly Button[] _buttons;

        public MenuDialogKeyboardInput(Button[] buttons)
        {
            _buttons = buttons;
        }

        /// <summary>
        /// Processes number key input. Call each frame when menu is active.
        /// </summary>
        /// <param name="activeCount">Number of currently displayed options (keys 1..N map to indices 0..N-1).</param>
        public void Process(int activeCount)
        {
            if (_buttons == null || activeCount <= 0) return;
#if ENABLE_INPUT_SYSTEM
            if (Keyboard.current == null) return;
#endif
            int limit = Mathf.Min(activeCount, KeyCount, _buttons.Length);

            for (int i = 0; i < limit; i++)
            {
#if ENABLE_INPUT_SYSTEM
                if (!Keyboard.current[DigitKeys[i]].wasPressedThisFrame && !Keyboard.current[NumpadKeys[i]].wasPressedThisFrame) continue;
#else
                if (!Input.GetKeyDown(AlphaKeys[i]) && !Input.GetKeyDown(KeypadKeys[i])) continue;
#endif

                var button = _buttons[i];
                if (button != null && button.interactable)
                    button.onClick.Invoke();
                return;
            }
        }
    }
}
