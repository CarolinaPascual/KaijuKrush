using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class Dinosaur : Kaiju
{
    private const int STATE_NORMAL = 0;
    private const int STATE_EAT = 1;
    private const int STATE_LOSE = 2;
    private const int STATE_SKILL = 3;

    
    public Dinosaur(int aType)
    {
        scale = 30;
        type = aType;
        originalSprites = Resources.LoadAll<Sprite>("Sprites/Placeholders_Prototype/Kaijus/001");
        beast = new CAnimatedSprite();
        beast.setName("Dinosaur");
        beast.setFrames(Resources.LoadAll<Sprite>("Sprites/Placeholders_Prototype/Kaijus/001"));
        
        setState(STATE_NORMAL);
        beast.setScale(scale);
        beast.setY(535);
        beast.setX(CGameConstants.SCREEN_WIDTH / 4 - 25);
        beast.setSortingLayer("Icons");
        beast.render();
        prefferedFood = 1;
    }

    override public void setState(int aState)
    {
        beast.setState(aState);
        currentState = aState;
        beast.setVisible(true);
        switch (getState())
        {
            case STATE_NORMAL:
                beast.initAnimation(1, 8, 6, true);
                break;
            case STATE_EAT:
                beast.initAnimation(9, 12, 6, false);
                break;
            case STATE_LOSE:
                beast.initAnimation(13, 16, 6, true);
                break;
            case STATE_SKILL:
                beast.initAnimation(17, 27, 6, false);
                break;
        }
}
    
    override public void update()
    {       
        base.update();
        if (currentState == STATE_EAT)
        {
            if (beast.isEnded())
            {
                setState(STATE_NORMAL);
            }
        }
        if (growCounter > 0 & scale<= 110)
        {
            float aux = growCounter / 10;
            scale += aux;
            growCounter -= aux;
            beast.setScale(scale);
            
        }
    }

  
}

