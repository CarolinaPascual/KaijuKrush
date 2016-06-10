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

    private int currentState;
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

    public void setState(int aState)
    {
        beast.setState(aState);
        currentState = aState;
        beast.setVisible(true);
        switch (getState())
        {
            case STATE_NORMAL:
                beast.initAnimation(1, 8, 8, true);
                break;
            case STATE_EAT:
                beast.initAnimation(9, 12, 8, false);
                break;
            case STATE_LOSE:
                beast.initAnimation(13, 16, 8, true);
                break;
        }

       
       

    }
    public int getState()
    {
        return currentState;
    }
    override public void update()
    {
        if (CMouse.firstPress(0))
        {
            growCounter += 2;   
        }
       
       if (growCounter > 0)
        {
            scale++;
            growCounter--;
            beast.setScale(scale);
        }

        base.update();
    }
}

