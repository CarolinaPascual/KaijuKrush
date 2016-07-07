using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Kraken:Kaiju
    {
    public Kraken(int aStage, int aFirstBreak, int aSecondBreak)
    {
        scale = 30;
        stage = aStage;
        firstBreakpoint = aFirstBreak;
        secondBreakpoint = aSecondBreak;
        beast = new CAnimatedSprite();
        beast.setName("Kraken");
        beast.setFrames(Resources.LoadAll<Sprite>("Sprites/Kaijus/Kraken/00" + stage));
        stage2Imgs = Resources.LoadAll<Sprite>("Sprites/Kaijus/Kraken/002");
        stage3Imgs = Resources.LoadAll<Sprite>("Sprites/Kaijus/Kraken/003");

        setState(STATE_NORMAL);
        beast.setScale(scale);
        beast.setY(535);
        beast.setX(CGameConstants.SCREEN_WIDTH / 4 + 100);
        beast.setSortingLayer("Icons");
        beast.render();
        prefferedFood = 2;
    }

    override public void setState(int aState)
    {
        if (!(aState == STATE_EAT && currentState == STATE_SKILL))
        {
            beast.setState(aState);
            currentState = aState;
            beast.setVisible(true);
            switch (getState())
            {
                case STATE_NORMAL:
                    beast.initAnimation(15, 22, 8, true);
                    break;
                case STATE_EAT:
                    beast.initAnimation(1, 14, 8, false);
                    break;
                case STATE_LOSE:
                    beast.initAnimation(23, 27, 8, true);
                    break;
                case STATE_SKILL:
                    beast.initAnimation(28, 39, 8, false);
                    break;
                case STATE_WIN:
                    beast.initAnimation(40, 56, 8, false);
                    break;
            }
        }
    }

    override public void update()
    {
        base.update();

    }

    override public void secondPower()
    {
        List<List<Tile>> mBoard = CurrentStageData.currentBoard.matrixBoard;
        for (int i = 0; i < mBoard.Count(); i++)
        {
            for (int j = 0; j < mBoard[i].Count(); j++)
            {
                if ((j + i) % 2 == 0)
                {
                    mBoard[i][j].food.markMatch();
                }

            }
        }
        CurrentStageData.currentBoard.deleteMatches();
        CurrentStageData.currentBoard.cascadeBoard1();
        CurrentStageData.currentBoard.fillSpaces();
        CurrentStageData.currentBoard.current_state = 3;
        setState(STATE_SKILL);
    }
}

