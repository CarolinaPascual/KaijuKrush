using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Skill2Animation: CAnimatedSprite
  {
    public Skill2Animation (int aType)
    {
        setFrames(Resources.LoadAll<Sprite>("Sprites/SkillAnimation/00"+ aType));
        setXY(CurrentStageData.currentBoard.matrixBoard[3][3].background.getX(), CurrentStageData.currentBoard.matrixBoard[3][3].background.getY());
        initAnimation(1, 5, 12, false);
        setSortingLayer("TextUI");
    }

    public override void update()
    {
        base.update();
        if (isEnded())
        {
            CurrentStageData.currentKaiju.secondPower();
            setDead(true);
        }
    }

}

