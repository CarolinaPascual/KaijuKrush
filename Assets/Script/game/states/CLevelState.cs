using UnityEngine;
using System.Collections;
//using Assets.Script.game.entities;

public class CLevelState : CGameState
{
    const int STATE_PLAYING = 0;
    const int STATE_WIN = 1;
    const int STATE_LOSE = 2;
    //private CPlayer mPlayer;
    private int current_state;
    private Board mBoard;
    private Dinosaur monster;
    private Enemy building;
    private CSpriteManager mSpriteManager;
    private CurrentStageData mCurrentData;
    private CText mText;
    private CText resultText;

    private CSprite screenDim;




    public CLevelState()
    {
        current_state = STATE_PLAYING;
        mBoard = new Board();
        monster = new Dinosaur(1, 53, 76);
        building = new Enemy();
        mText = new CText("TEST", CText.alignment.TOP_CENTER);
        mText.setX(0);
        mText.setY(0);
        resultText = new CText("", CText.alignment.TOP_CENTER);
        resultText.setX(0);
        resultText.setY(100);
        mSpriteManager = new CSpriteManager();
        mCurrentData = new CurrentStageData();
        mBoard.movementsLeft = 2;
        mBoard.targetScore = 110;
        float scoreCoefficient = (float)70 / (float)mBoard.targetScore;
        CurrentStageData.inst().assignData(monster, mBoard, scoreCoefficient);
        screenDim = new CSprite();
        screenDim.setSortingLayer("ScreenShade");
        screenDim.setName("Sombra");

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
        screenDim.update();
        mText.setText("Movements Left : " + mBoard.getMovementsLeft().ToString());
        mText.update();
        resultText.update();
        switch (current_state)
        {
            case STATE_PLAYING:
                if (mBoard.current_state == 0)
                {
                    if (CurrentStageData.inst().currentKaiju.scale >= 100)
                    {
                        current_state = STATE_WIN;
                        resultText.setText("YOU WIN");
                        monster.setState(4);
                        building.setState(1);

                    }
                    else
                    {
                        
                                                
                        screenDim.setImage(Resources.Load<Sprite>("Sprites/Placeholders_Prototype/screenShade"));
                        screenDim.setX(0);
                        screenDim.setY(0);
                        current_state = STATE_LOSE;
                        resultText.setText("YOU LOSE");
                        monster.setState(2);

                    }
                }
                break;

            case STATE_WIN:
                if (!building.building.isEnded())
                {
                    CurrentStageData.inst().cameraShake();
                }
                else
                     if (Camera.main.transform.position.x > 360)
                {
                    Camera.main.transform.Translate(new Vector3(-15, 0, 0));
                }
                if (Camera.main.transform.position.x < 360)
                {
                    Camera.main.transform.Translate(new Vector3(15, 0, 0));
                }
                break;

            case STATE_LOSE:
                break;
        }



    }

    override public void render()
    {
        base.render();
        mSpriteManager.render();
        mBoard.render();
        screenDim.render();
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
        resultText.destroy();
        resultText = null;
        mBoard.destroy();
        mBoard = null;
        monster.destroy();
        monster = null;
        building.destroy();
        building = null;
        CurrentStageData.inst().clearData();    
    }

}
