using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Kong : Kaiju
    {
    public Kong(int aStage, int aFirstBreak, int aSecondBreak)
    {
        scale = 30;
        stage = aStage;
        firstBreakpoint = aFirstBreak;
        secondBreakpoint = aSecondBreak;
        beast = new CAnimatedSprite();
        beast.setName("Kong");
        beast.setFrames(Resources.LoadAll<Sprite>("Sprites/Kaijus/Kong/00" + stage));
        stage2Imgs = Resources.LoadAll<Sprite>("Sprites/Kaijus/Kong/002");
        stage3Imgs = Resources.LoadAll<Sprite>("Sprites/Kaijus/Kong/003");

        setState(STATE_NORMAL);
        beast.setScale(scale);
        beast.setY(535);
        beast.setX(CGameConstants.SCREEN_WIDTH / 4 - 25);
        beast.setSortingLayer("Icons");
        beast.render();
        prefferedFood = 0;
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

    }

}

