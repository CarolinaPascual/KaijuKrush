using UnityEngine;
using System.Collections;

public class CGameObject
{
    private Vector3 mPos;
    private Vector3 mVel;
    private Vector3 mAccel;

    private bool mIsDead = false;

    private int mState = 0;
    private float mTimeState = 0.0f;
    private float mRadius = 100;
    

    public CGameObject()
    {
    }
    public void setDead(bool aDead)
    {
        mIsDead = aDead;
    }
    public bool isDead()
    {
        return mIsDead;
    }
    public void setX(float aX)
    {
        mPos.x = aX;
    }

    public void setY(float aY)
    {
        mPos.y = aY;
    }

    public void setXY (float aX,float aY)
    {
        setX(aX);
        setY(aY);
    }
    public void setZ(float aZ)
    {
        mPos.z = aZ;
    }

    public float getX()
    {
        return mPos.x;
    }

    public float getY()
    {
        return mPos.y;
    }

    public float getZ()
    {
        return mPos.z;
    }

    public void setVelX(float aVelX)
    {
        mVel.x = aVelX;
    }

    public void setVelY(float aVelY)
    {
        mVel.y = aVelY;
    }

    public void setVelZ(float aVelZ)
    {
        mVel.z = aVelZ;
    }

    public float getVelX()
    {
        return mVel.x;
    }

    public float getVelY()
    {
        return mVel.y;
    }

    public float getVelZ()
    {
        return mVel.z;
    }

    public void setAccelX(float aAccelX)
    {
        mAccel.x = aAccelX;
    }

    public void setAccelY(float aAccelY)
    {
        mAccel.y = aAccelY;
    }

    public void setAccelZ(float aAccelZ)
    {
        mAccel.z = aAccelZ;
    }

    public float getAccelX()
    {
        return mAccel.x;
    }

    public float getAccelY()
    {
        return mAccel.y;
    }

    public float getAccelZ()
    {
        return mAccel.z;
    }

    virtual public void update()
    {
        mTimeState = mTimeState + Time.deltaTime;

        mVel = mVel + mAccel * Time.deltaTime;
        mPos = mPos + mVel * Time.deltaTime;
    }

    virtual public void render()
    {
    }

    virtual public void destroy()
    {
    }

    virtual public void setState(int aState)
    {
        mState = aState;
        mTimeState = 0.0f;
    }

    public int getState()
    {
        return mState;
    }

    public float getTimeState()
    {
        return mTimeState;
    }

    public Vector3 getPos()
    {
        return new Vector3(getX(), getY());
    }
    public void setRadius(float aRadius)
    {
        mRadius = aRadius;
    }

    public float getRadius()
    {
        return mRadius;
    }
    public bool collides(CGameObject aGameObject)
    {
        if (CMath.dist(getX(), getY(), aGameObject.getX(), aGameObject.getY()) < (getRadius() + aGameObject.getRadius()))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
   
}