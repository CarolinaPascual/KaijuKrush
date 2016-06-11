using UnityEngine;
using System.Collections;
//using Assets.Script.game.entities;

public class CLevelState : CGameState
{
    //private CPlayer mPlayer;
    private Board mBoard;
    private Dinosaur monster;
    private Enemy building;
    private CSpriteManager mSpriteManager;
    private CurrentStageData mCurrentData;
    private CText mText;
    private CText resultText;     
    public CLevelState()
    {
        
        mBoard = new Board();
        monster = new Dinosaur(1);
        building = new Enemy();        
        mText = new CText("TEST", CText.alignment.TOP_CENTER);        
        mText.setX(0);
        mText.setY(0);
        resultText = new CText("", CText.alignment.TOP_CENTER);
        resultText.setX(0);
        resultText.setY(100);
        mSpriteManager = new CSpriteManager();
        mCurrentData = new CurrentStageData();
        mBoard.movementsLeft = 15;
        mBoard.targetScore = 120;
        float scoreCoefficient = (float)70 / (float)mBoard.targetScore;
        CurrentStageData.inst().assignData(monster, mBoard,scoreCoefficient);

    }

    override public void init()
    {
        base.init();
    }

    override public void update()
    {
        base.update();
        mSpriteManager.update();        
        CMouse.update();
        mBoard.update();
        monster.update();
        building.update();
        mText.setText("Movements Left : " + mBoard.getMovementsLeft().ToString());       
        mText.update();
        if (mBoard.getMovementsLeft() < 1)
        {
            if (CurrentStageData.inst().currentKaiju.scale >= 100)
            {
                resultText.setText("YOU WIN");
            }
            else
            {
                resultText.setText("YOU LOSE");
            }
        }
        resultText.update();
    }

    override public void render()
    {
        base.render();
        mSpriteManager.render();
        mBoard.render();
        
        monster.render();
        building.render();
        
        mText.render();
        resultText.render();
    }

    override public void destroy()
    {
        base.destroy();
        mSpriteManager.destroy();
        mSpriteManager = null;
        mText.destroy();
        mText = null;
       
    }
}
