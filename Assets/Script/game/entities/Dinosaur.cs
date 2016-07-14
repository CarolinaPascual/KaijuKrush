using System;
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
        beast.setX(CGameConstants.SCREEN_WIDTH / 4 );
        beast.setSortingLayer("Icons");
        beast.render();
        prefferedFood = 1;
    }

    override public void setState(int aState)
    {
        if (!(aState==STATE_EAT && currentState == STATE_SKILL))
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
                    SoundList.instance.playLose();
                    beast.initAnimation(13, 16, 6, true);
                break;
            case STATE_SKILL:
                    SoundList.instance.playDino();
                beast.initAnimation(17, 27, 6, false);
                break;
            case STATE_WIN:
                beast.initAnimation(28, 34, 6, false);
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
                if (j + i == 2 || i + j == 6 || i + j == 10)
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

