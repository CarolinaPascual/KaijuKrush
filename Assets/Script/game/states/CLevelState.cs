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
    private CText mText;
    private CText resultText;
    private CSprite screenDim;
    private CText nextScreen;



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
        nextScreen = new CText("Back to Menu",CText.alignment.CENTER,70);
        nextScreen.setX(CGameConstants.SCREEN_WIDTH / 2);
        nextScreen.setY(CGameConstants.SCREEN_HEIGHT / 2);
        nextScreen.setVisible(false);
        
        
        mBoard.movementsLeft = 15;
        mBoard.targetScore = 110;
        float scoreCoefficient = (float)70 / (float)mBoard.targetScore;
        CurrentStageData.assignData(monster, mBoard, scoreCoefficient);
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
        //CSpriteManager.update();
        CMouse.update();
        mBoard.update();
        monster.update();
        building.update();
        screenDim.update();
        mText.setText("Movements Left : " + mBoard.getMovementsLeft().ToString());
        mText.update();
        nextScreen.update();
        resultText.update();
        switch (current_state)
        {
            case STATE_PLAYING:
                if (mBoard.current_state == 0)
                {
                    if (CurrentStageData.currentKaiju.scale >= 100)
                    {
                        current_state = STATE_WIN;
                        resultText.setText("YOU WIN");
                        monster.setState(4);
                        building.setState(1);

                    }
                    else
                    {
                        
                                                
                        screenDim.setImage(Resources.Load<Sprite>("Sprites/screenShade"));
                        screenDim.setX(0);
                        screenDim.setY(0);
                        nextScreen.setVisible(true);
                        current_state = STATE_LOSE;
                        resultText.setText("YOU LOSE");
                        monster.setState(2);

                    }
                }
                break;

            case STATE_WIN:
                if (!building.building.isEnded())
                {
                    CurrentStageData.cameraShake();
                }
                else
                {
                    screenDim.setImage(Resources.Load<Sprite>("Sprites/screenShade"));
                    screenDim.setX(0);
                    screenDim.setY(0);
                    nextScreen.setVisible(true);

                    if (CMouse.firstPress())
                    {
                        CGame.inst().setState(new CMenuState());
                    }
                
                if (Camera.main.transform.position.x > 360)
                {
                    
                    Camera.main.transform.Translate(new Vector3(-15, 0, 0));
                }
                if (Camera.main.transform.position.x < 360)
                {
                    Camera.main.transform.Translate(new Vector3(15, 0, 0));
                }
                }
                break;

            case STATE_LOSE:
                if (CMouse.firstPress())
                {
                   CGame.inst().setState(new CMenuState());
                }
                break;
        }



    }

    override public void render()
    {
        base.render();
        //CSpriteManager.render();
        mBoard.render();
        screenDim.render();
        monster.render();
        building.render();
        nextScreen.render();
        mText.render();
        resultText.render();
    }

    override public void destroy()
    {
        base.destroy();        
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
        screenDim.destroy();
        screenDim = null;
        nextScreen.destroy();
        nextScreen = null;
        CurrentStageData.clearData();    
    }

}
