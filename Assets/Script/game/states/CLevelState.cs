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
    private bool ended;
    public CLevelState()
    {
        ended = false;
        mBoard = new Board();
        monster = new Dinosaur(1,53,76);
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
        mBoard.targetScore = 110;
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
        if (mBoard.getMovementsLeft() < 1 & mBoard.current_state == 0)
        {
            if (CurrentStageData.inst().currentKaiju.scale >= 100)
            {
                
                resultText.setText("YOU WIN");
                if (!ended) { 
                    monster.setState(4);
                    building.setState(1);
                    ended = true;
                }
                if (!building.building.isEnded())
                {
                    CurrentStageData.inst().cameraShake();
                }
                else
                    if (Camera.main.transform.position.x > 360)
                {
                    Camera.main.transform.Translate(new Vector3(-15, 0, 0));
                }if (Camera.main.transform.position.x < 360)
                {
                    Camera.main.transform.Translate(new Vector3(15, 0, 0));
                }
            }
            else
            {
                resultText.setText("YOU LOSE");
                if (monster.getState() != 2) { monster.setState(2); }
                
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
