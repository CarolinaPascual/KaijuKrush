﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class Dinosaur : Kaiju
{
    
    


    public Dinosaur(int aStage,int aFirstBreak,int aSecondBreak)
    {
        scale = 30;
        stage = aStage;
        firstBreakpoint = aFirstBreak;
        secondBreakpoint = aSecondBreak;
        beast = new CAnimatedSprite();
        beast.setName("Dinosaur");
        beast.setFrames(Resources.LoadAll<Sprite>("Sprites/Kaijus/Dino/00" + stage));
        stage2Imgs = Resources.LoadAll<Sprite>("Sprites/Kaijus/Dino/002");
        stage3Imgs = Resources.LoadAll<Sprite>("Sprites/Kaijus/Dino/003");

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
            case STATE_WIN:
                beast.initAnimation(28, 34, 6, false);
                break;
        }
    }

    override public void update()
    {
        base.update();
        if (currentState == STATE_EAT | currentState == STATE_WIN | currentState == STATE_SKILL)
        {
            if (beast.isEnded())
            {
                setState(STATE_NORMAL);
            }
        }


        if (growCounter > 0 & scale <= 110)
        {
            float aux = growCounter / 10;
            scale += aux;
            growCounter -= aux;
            beast.setScale(scale);

        }
        if ((stage == 1 & scale >= firstBreakpoint) | (stage==2 & scale>=secondBreakpoint))
        {
            growStage();
        }
       
    }
    public void growStage()
    {
        stage++;
        if (stage == 2)
        {
            beast.setFrames(stage2Imgs);
        }else if (stage == 3)
        {
            beast.setFrames(stage3Imgs);
        }
        
    }

  
}

