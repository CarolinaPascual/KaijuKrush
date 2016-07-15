using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class KaijuSelectButton : CSprite
{
    private const int STATE_ENTERING = 0;
    private const int STATE_NORMAL = 1;
    private const int STATE_LEAVING = 2;

    public float originalX;
    public float originalY;
    public float finalX;
    public float finalY;
    public int currentState;

        public KaijuSelectButton ()
    {
        currentState = STATE_ENTERING;
    }

        public void setPosition(float oX,float oY, float fX, float fY)
    {
        originalX = oX;
        originalY = oY;
        setXY(oX, oY);
        finalX = fX;
        finalY = fY;
    }

    public override void update()
    {
        base.update();

        switch (currentState)
        {
            case STATE_ENTERING:
                move();
                break;
            case STATE_NORMAL:
                break;
            case STATE_LEAVING:
                move();
                break;
        }
    }

    public void move()
    {
        if (getX() < finalX)
        {
            setVelX(500);
            //setX(getX() + 10);


        }
        else if (getX()> finalX)
        {
            setVelX(-500);
            //setX(getX() - 10);
        }
        if (Math.Abs(getX() - finalX) <= 500 * Time.deltaTime)
            {
            setX(finalX);
            setVelX(0);
            currentState = STATE_NORMAL;
        }

    }

    public void leave()
    {
        finalX = originalX;
        currentState = STATE_LEAVING;
    }

}

