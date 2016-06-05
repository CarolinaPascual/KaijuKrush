using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class CManager
{
    private List<CGameObject> mArray;

    public CManager()
    {
        mArray = new List<CGameObject>();

    }
    public void add(CGameObject aGameObject)
    {
        mArray.Add(aGameObject);
    }
    virtual public void update()
    {
        for (int i = mArray.Count - 1; i >= 0; i--)
        {
            mArray[i].update();
        }
        for (int i = mArray.Count - 1; i >= 0; i--)
        {
            if (mArray[i].isDead())
            {
                removeObjectWithIndex(i);
            }
        }

    }
    virtual public void render()
    {

        for (int i = mArray.Count - 1; i >= 0; i--)
        {
            mArray[i].render();
        }


    }
    /*
    public CGameObject collides(CGameObject aGameObject)
    {
        for (int i = mArray.Count - 1; i >= 0; i--)
        {
            if (aGameObject.collides(mArray[i]))
            {
                return mArray[i];

            }
        }

    }*/
    private void removeObjectWithIndex(int aIndex)
    {
        if (aIndex < mArray.Count)
        {
            mArray[aIndex].destroy();
            mArray[aIndex] = null;
            mArray.RemoveAt(aIndex);

        }
    }
    virtual public void destroy()
    {
        for (int i = mArray.Count - 1; i >= 0; i--)
        {
            removeObjectWithIndex(i);

        }
        mArray = null;

    }
    public void RemoveAll()
    {
        for (int i = mArray.Count - 1; i >= 0; i--)
        {
            removeObjectWithIndex(i);
        }
        mArray.Clear();
    }

    public int getCount()
    {
        return mArray.Count;
    }
    public List<CGameObject> getList()
    {
        return mArray;
    }
    public void setAllDead(bool aDead)
    {
        for (int i = 0; i < getCount(); i++)
        {
            getList()[i].setDead(aDead);
        }
    }

}

