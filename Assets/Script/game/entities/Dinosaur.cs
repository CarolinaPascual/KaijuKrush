using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class Dinosaur : Kaiju
{
    private const int STATE_NORMAL = 0;
    private int currentState;
    public Dinosaur(int aType)
    {
        type = aType;
        beast = new CAnimatedSprite();
        beast.setFrames(Resources.LoadAll<Sprite>("Sprites/Placeholders_Prototype/Kaijus/001"));
        setState(STATE_NORMAL);

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

        if(getState()== STATE_NORMAL)
        {
            beast.initAnimation(1, 8, 8, true);
        }
       

    }
    public int getState()
    {
        return currentState;
    }
}

