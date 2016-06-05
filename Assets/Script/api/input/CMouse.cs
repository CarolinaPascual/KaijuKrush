using UnityEngine;
using System.Collections;

public class CMouse
{
    static private bool mInitialized = false;


    public CMouse()
    {
        throw new UnityException("Error in CMouse(). You're not supposed to instantiate this class.");
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

    public static bool firstPress(int button = 0)
    {
        if (Input.GetMouseButtonDown(button))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool release(int button = 0)
    {
        if (Input.GetMouseButtonUp(button))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool pressed(int button = 0)
    {
        if (Input.GetMouseButtonDown(button))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static Vector3 getPos()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.y *= -1;
        pos.z = 0;
        return pos;
    }
    public static bool leftPress()
    {
        return Input.GetMouseButtonUp(0);
    }
}
