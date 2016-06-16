using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class CSpriteManager : CManager
{
    private static CSpriteManager mInst = null;
    static private bool mInitialized = false;
    public CSpriteManager()
    {
        throw new UnityException("Error in CMouse(). You're not supposed to instantiate this class.");

    }
    public static CSpriteManager inst()
    {
        return mInst;
    }
    

     public static void init()
    {
        if (mInitialized)
        {
            return;
        }
        mInitialized = true;
    }

    public override void update()
    {
        base.update();
    }
    public override void render()
    {
        base.render();

    }
    public override void destroy()
    {
        base.destroy();
        if (mInitialized)
        {
            mInitialized = false;
        }
    }

}

