using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

//namespace Assets.Script.game.entities
//{
public class Kaiju
{
    protected const int STATE_NORMAL = 0;
    protected const int STATE_EAT = 1;
    protected const int STATE_LOSE = 2;
    protected const int STATE_SKILL = 3;
    protected const int STATE_WIN = 4;

    public CAnimatedSprite beast { get; set; }
    public int stage { get; set; }
    public int prefferedFood { get; set; }    
    public float scale { get; set; }
    public float growCounter { get; set; }
    public int currentState { get; set; }
    public int firstBreakpoint { get; set; }
    public int secondBreakpoint { get; set; }
    public Sprite[] stage2Imgs { get; set; }
    public Sprite[] stage3Imgs { get; set; }


    public Kaiju()
    {
       

    }
    virtual public void update()
    {
        beast.update();
        if (currentState == STATE_EAT | currentState == STATE_WIN | currentState == STATE_SKILL)
        {
            if (beast.isEnded())
            {
                setState(STATE_NORMAL);
            }
        }


        if (growCounter != 0 & scale <= 110)
        {
            float aux = growCounter / 10;
            scale += aux;
            growCounter -= aux;
            beast.setScale(scale);

        }
        if ((stage == 1 & scale >= firstBreakpoint) | (stage == 2 & scale >= secondBreakpoint))
        {
            growStage();
        }

    }
    public void render()
    {
        beast.render();
    }
    public void destroy()
    {
        beast.destroy();
        beast = null;
    }
    virtual public void setState(int aState)
    {

    }
    public int getState()
    {
        return currentState;
    }
     public void addGrow(float grow)
    {
        growCounter += grow;
    }
    public void firstPower()
    {
        List<List<Tile>> mBoard = CurrentStageData.currentBoard.matrixBoard;
        for (int i = 0; i < mBoard.Count(); i++)
        {
            for (int j = 0; j < mBoard[i].Count(); j++)
            {
                if (mBoard[i][j].food.Type == 3)
                {
                    mBoard[i][j].clearFood();
                }
            }
        }
        CurrentStageData.currentBoard.cascadeBoard1();
        CurrentStageData.currentBoard.fillSpaces();
        CurrentStageData.currentBoard.current_state = 3;
    }
    public void growStage()
    {
        stage++;
        if (stage == 2)
        {
            beast.setFrames(stage2Imgs);
        }
        else if (stage == 3)
        {
            beast.setFrames(stage3Imgs);
        }

    }
}
//}
