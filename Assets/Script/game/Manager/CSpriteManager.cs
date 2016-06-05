using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class CSpriteManager : CManager
{
    private static CSpriteManager mInst = null;
    public CSpriteManager()
    {
        registerSingleton();

    }
    public static CSpriteManager inst()
    {
        return mInst;
    }
    private void registerSingleton()
    {
        if (mInst == null)
        {
            mInst = this;
        }
        else
        {
            throw new UnityException("Error: You cannot create another instance of singleton class " + this + " ");
        }

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
        mInst = null;
    }

}

