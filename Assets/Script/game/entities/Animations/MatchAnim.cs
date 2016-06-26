using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class MatchAnim:CSprite
    {
    private const int STATE_INCREASE = 0;
    private const int STATE_REDUCE = 1;
    private int scale;

    public MatchAnim(int aType,float aX, float aY)
    {
        setImage(Resources.Load<Sprite>("Sprites/food" + aType.ToString()));
        setX(aX);
        setY(aY);
        setState(STATE_INCREASE);        
        scale = 100;
        setSortingLayer("Icons");
        setAlpha(1f);
        
    }
    public override void update()
    {
        base.update();
        switch (getState())
        {
            case STATE_INCREASE:
                scale += 5;
                setScale(scale);
                if (scale >= 140)
                {
                    setState(STATE_REDUCE);
                }
                break;

            case STATE_REDUCE:
                scale -= 5;
                setScale(scale);
                if (scale <= 50)
                {
                    setDead(true);
                } 
                break;
        }
        
       
           
        
    }
}

