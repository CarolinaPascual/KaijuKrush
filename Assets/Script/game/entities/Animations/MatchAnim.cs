using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class MatchAnim:CAnimatedSprite
    {
    private const int STATE_INCREASE = 0;
    private const int STATE_REDUCE = 1;
    private const int STATE_EXPLODE = 3;
    private int scale;
    private bool missile = false;

    public MatchAnim(int aType,float aX, float aY)
    {
        //setImage(Resources.Load<Sprite>("Sprites/food" + aType.ToString()));
        setFrames(Resources.LoadAll<Sprite>("Sprites/food" + aType.ToString()));
        setX(aX);
        setY(aY);
        setState(STATE_INCREASE);        
        scale = 100;
        setSortingLayer("Icons");
        setAlpha(2f);
        if (aType == 4)
            missile = true;
        
    }
    public override void update()
    {
        base.update();
        switch (getState())
        {
            case STATE_INCREASE:
                scale += 15;
                setScale(scale);
                if (scale >= 140)
                {
                    setState(STATE_REDUCE);
                    if (missile)
                    {
                        setState(STATE_EXPLODE);
                        setFrames(Resources.LoadAll<Sprite>("Sprites/Explotion"));
                        setAlpha(1f);
                        initAnimation(1, 4, 20, false);
                    }
                        
                }
                break;

            case STATE_REDUCE:
                scale -= 15;
                setScale(scale);
                if (scale <= 50)
                {
                    setDead(true);
                } 
                break;
            case STATE_EXPLODE:
                if (isEnded())
                {
                    setDead(true);
                }
                break;
        }
        
       
           
        
    }
}

