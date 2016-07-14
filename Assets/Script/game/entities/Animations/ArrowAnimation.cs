using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class ArrowAnimation:CSprite
    {
    private const float speed = 250;
    public int finalY;
    public int type;
    public ArrowAnimation(int aType, int aPos = 0)
    {
        switch (aPos)
        {
            case 0:
                setX(110);
                break;
            case 1:
                setX(125);
                break;
            case 2:
                setX(140);
                break;
        }
        
        setY(320);
        setSortingLayer("TextUI");
        setAlpha(1.5f);

        if (aType < 4) { 
            setImage(Resources.Load<Sprite>("Sprites/arrows/Green-Arrow"+aType));
            setVelY(-speed);
            finalY = 180;
            type = 0;
        }
        else { 
            setImage(Resources.Load<Sprite>("Sprites/arrows/Red-Arrow"));
            setVelY(speed);
            setY(180);
            finalY = 350;
            type = 1;
        }
        
    }

    public override void update()
    {
        base.update();
        switch (type)
        {
            case 0:
                if (getY() < finalY)
                {
                    setDead(true);
                }
                break;
            case 1:
                if (getY() > finalY)
                {
                    setDead(true);
                }
                break;
        }
        

    }
}

