using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;


public static class GamePadManager
{
    public enum eGamePadType
    {
        XBOX = 0,
        DUALSHOCK4,

        ALLTYPE
    }

    [System.NonSerialized]
    public static bool onceTiltStick = false;

    //==========================================================================
    //
    //  ゲームパッドの十字に配置されたボタン、十字キー、スタートボタンがおされた
    //
    //==========================================================================

    public static bool PressAnyButton(eGamePadType gamepadtype)
    {
        Gamepad gamepad = Gamepad.current;
        if (gamepad == null)
        {
            return false;
        }

        if (gamepadtype == eGamePadType.XBOX)
        {
            if (gamepad.aButton.wasPressedThisFrame)
            {
                return true;
            }
            if (gamepad.bButton.wasPressedThisFrame)
            {
                return true;
            }
            if (gamepad.xButton.wasPressedThisFrame)
            {
                return true;
            }
            if (gamepad.yButton.wasPressedThisFrame)
            {
                return true;
            }
            if (gamepad.dpad.up.wasPressedThisFrame)
            {
                return true;
            }
            if (gamepad.dpad.down.wasPressedThisFrame)
            {
                return true;
            }
            if (gamepad.dpad.right.wasPressedThisFrame)
            {
                return true;
            }
            if (gamepad.dpad.left.wasPressedThisFrame)
            {
                return true;
            }
            if (gamepad.startButton.wasPressedThisFrame)
            {
                return true;
            }
        }
        else if (gamepadtype == eGamePadType.DUALSHOCK4)
        {
            if (gamepad.circleButton.wasPressedThisFrame)
            {
                return true;
            }
            if (gamepad.crossButton.wasPressedThisFrame)
            {
                return true;
            }
            if (gamepad.squareButton.wasPressedThisFrame)
            {
                return true;
            }
            if (gamepad.triangleButton.wasPressedThisFrame)
            {
                return true;
            }
            if (gamepad.dpad.up.wasPressedThisFrame)
            {
                return true;
            }
            if (gamepad.dpad.down.wasPressedThisFrame)
            {
                return true;
            }
            if (gamepad.dpad.right.wasPressedThisFrame)
            {
                return true;
            }
            if (gamepad.dpad.left.wasPressedThisFrame)
            {
                return true;
            }
            if (gamepad.startButton.wasPressedThisFrame)
            {
                return true;
            }

        }
        else if (gamepadtype == eGamePadType.ALLTYPE)
        {
            if (gamepad.buttonEast.wasPressedThisFrame)
            {
                return true;
            }
            if (gamepad.buttonWest.wasPressedThisFrame)
            {
                return true;
            }
            if (gamepad.buttonNorth.wasPressedThisFrame)
            {
                return true;
            }
            if (gamepad.buttonSouth.wasPressedThisFrame)
            {
                return true;
            }
            if (gamepad.dpad.up.wasPressedThisFrame)
            {
                return true;
            }
            if (gamepad.dpad.down.wasPressedThisFrame)
            {
                return true;
            }
            if (gamepad.dpad.right.wasPressedThisFrame)
            {
                return true;
            }
            if (gamepad.dpad.left.wasPressedThisFrame)
            {
                return true;
            }
            if (gamepad.startButton.wasPressedThisFrame)
            {
                return true;
            }
        }
        return false;
    }

    //==========================================================================
    //
    //  ゲームパッドの十字に配置されたボタン、十字キー、スタートボタンが離された
    //
    //==========================================================================
    public static bool ReleaseAnyButton(eGamePadType gamepadtype)
    {
        Gamepad gamepad = Gamepad.current;
        if (gamepad == null)
        {
            return false;
        }

        if (gamepadtype == eGamePadType.XBOX)
        {
            if (gamepad.aButton.wasReleasedThisFrame)
            {
                return true;
            }
            if (gamepad.bButton.wasReleasedThisFrame)
            {
                return true;
            }
            if (gamepad.xButton.wasReleasedThisFrame)
            {
                return true;
            }
            if (gamepad.yButton.wasReleasedThisFrame)
            {
                return true;
            }
            if (gamepad.dpad.up.wasReleasedThisFrame)
            {
                return true;
            }
            if (gamepad.dpad.down.wasReleasedThisFrame)
            {
                return true;
            }
            if (gamepad.dpad.right.wasReleasedThisFrame)
            {
                return true;
            }
            if (gamepad.dpad.left.wasReleasedThisFrame)
            {
                return true;
            }
            if (gamepad.startButton.wasReleasedThisFrame)
            {
                return true;
            }

        }
        else if (gamepadtype == eGamePadType.DUALSHOCK4)
        {
            if (gamepad.circleButton.wasReleasedThisFrame)
            {
                return true;
            }
            if (gamepad.crossButton.wasReleasedThisFrame)
            {
                return true;
            }
            if (gamepad.squareButton.wasReleasedThisFrame)
            {
                return true;
            }
            if (gamepad.triangleButton.wasReleasedThisFrame)
            {
                return true;
            }
            if (gamepad.dpad.up.wasReleasedThisFrame)
            {
                return true;
            }
            if (gamepad.dpad.down.wasReleasedThisFrame)
            {
                return true;
            }
            if (gamepad.dpad.right.wasReleasedThisFrame)
            {
                return true;
            }
            if (gamepad.dpad.left.wasReleasedThisFrame)
            {
                return true;
            }
            if (gamepad.startButton.wasReleasedThisFrame)
            {
                return true;
            }

        }
        else if (gamepadtype == eGamePadType.ALLTYPE)
        {
            if (gamepad.buttonEast.wasReleasedThisFrame)
            {
                return true;
            }
            if (gamepad.buttonWest.wasReleasedThisFrame)
            {
                return true;
            }
            if (gamepad.buttonNorth.wasReleasedThisFrame)
            {
                return true;
            }
            if (gamepad.buttonSouth.wasReleasedThisFrame)
            {
                return true;
            }
            if (gamepad.dpad.up.wasReleasedThisFrame)
            {
                return true;
            }
            if (gamepad.dpad.down.wasReleasedThisFrame)
            {
                return true;
            }
            if (gamepad.dpad.right.wasReleasedThisFrame)
            {
                return true;
            }
            if (gamepad.dpad.left.wasReleasedThisFrame)
            {
                return true;
            }
            if (gamepad.startButton.wasReleasedThisFrame)
            {
                return true;
            }

        }
        return false;
    }



    //public static bool DpadButton(eGamePadType gamepadtype)
    //{
    //    Gamepad gamepad = Gamepad.current;
    //    if (gamepad == null)
    //    {
    //        return false;
    //    }


    //    if (gamepadtype == eGamePadType.XBOX)
    //    {
    //        if (gamepad.dpad.up.wasReleasedThisFrame)
    //        {
    //            return true;
    //        }
    //        if (gamepad.dpad.down.wasReleasedThisFrame)
    //        {
    //            return true;
    //        }
    //        if (gamepad.dpad.right.wasReleasedThisFrame)
    //        {
    //            return true;
    //        }
    //        if (gamepad.dpad.left.wasReleasedThisFrame)
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }

    //    }









    //}

}
