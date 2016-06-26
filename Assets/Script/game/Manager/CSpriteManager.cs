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
            throw new UnityException("ERROR: Cannot create another instance of singleton class CEnemyManager.");
        }
    }

    override public void update()
    {
        base.update();
    }

    override public void render()
    {
        base.render();
    }

    override public void destroy()
    {
        base.destroy();
        mInst = null;
    }
}

