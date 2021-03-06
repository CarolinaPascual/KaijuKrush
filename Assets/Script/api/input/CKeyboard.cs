using UnityEngine;
using System.Collections;

public class CKeyboard
{
    static private bool mInitialized = false;

    public const KeyCode RIGHT = KeyCode.RightArrow;
    public const KeyCode LEFT = KeyCode.LeftArrow;

    public CKeyboard()
    {
        throw new UnityException("Error in CKeyboard(). You're not supposed to instantiate this class.");
    }

    public static void init()
    {
        if (mInitialized)
        {
            return;
        }
        mInitialized = true;
    }

    public static void update()
    {
    }

    public static void destroy()
    {
        if (mInitialized)
        {
            mInitialized = false;
        }
    }

    public static bool pressed(KeyCode aKey)
    {
        return Input.GetKey(aKey);
    }
}