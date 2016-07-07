using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class CSurvivalState : CGameState
{
    const int STATE_PLAYING = 0;
    const int STATE_WIN = 1;
    const int STATE_LOSE = 2;
    
    private int current_state;
    private Board mBoard;
    private Kaiju monster;
    private Enemy building;

    private CText scoreText;
    private CText resultText;
    private CSprite screenDim;
    private CText timeLeft;
    private SkillBar skills;
    private CSprite backMenuBttn;
    private CSprite tryAgainBttn;
    private Timer mTimer;
    private CInfo tryAgainInfo;


    public CSurvivalState(CInfo stageInfo)
    {
        CGame.inst().setImage("Sprites/level_Background00");
        tryAgainInfo = stageInfo;
        switch (stageInfo.Kaiju)
        {
            case 1:
                monster = new Dinosaur(stageInfo.startStage, stageInfo.firstStage, stageInfo.secondStage);
                break;
            case 2:
                monster = new Kong(stageInfo.startStage, stageInfo.firstStage, stageInfo.secondStage);
                break;
            case 3:
                monster = new Kraken(stageInfo.startStage, stageInfo.firstStage, stageInfo.secondStage);
                break;
        }
        current_state = STATE_PLAYING;
        CurrentStageData.difficulty = 0;
        mBoard = new Board(1);

        building = new Enemy(0);
        resultText = new CText("", CText.alignment.TOP_CENTER);
        resultText.setX(0);
        resultText.setY(100);

        timeLeft = new CText("Time: ", CText.alignment.TOP_CENTER);
        timeLeft.setX(0);
        timeLeft.setY(0);
        timeLeft.setColor(Color.black);

        scoreText = new CText("SCORE :", CText.alignment.TOP_CENTER);
        scoreText.setX(400);
        scoreText.setY(0);
        scoreText.setColor(Color.black);
        

        mBoard.targetScore = stageInfo.TargetScore; // MOVE TO CLASS
        float scoreCoefficient = (float)70 / (float)mBoard.targetScore;
        skills = new SkillBar(stageInfo.Kaiju);
        mTimer = new Timer();
        CurrentStageData.assignData(monster, mBoard, scoreCoefficient, skills);
        CurrentStageData.assignTimer(mTimer);
        screenDim = new CSprite();
        screenDim.setSortingLayer("ScreenShade");
        screenDim.setName("Sombra");
        backMenuBttn = new CSprite();
        backMenuBttn.setSortingLayer("TextUI");
        tryAgainBttn = new CSprite();
        tryAgainBttn.setSortingLayer("TextUI");

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
        mTimer.update();
        resultText.update();
        skills.update();
        backMenuBttn.update();
        tryAgainBttn.update();
        timeLeft.setText("TIME: " + (int)(CurrentStageData.currentTimer.getTimeLeft()));
        timeLeft.update();
        scoreText.setText("SCORE: " + (CurrentStageData.score * 10));
        scoreText.update();
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

                        current_state = STATE_LOSE;
                        resultText.setText("YOU LOSE");
                        backMenuBttn.setImage(Resources.Load<Sprite>("Sprites/BackMenuButton"));
                        backMenuBttn.setXY(CGameConstants.SCREEN_WIDTH / 2, CGameConstants.SCREEN_HEIGHT / 2);
                        tryAgainBttn.setImage(Resources.Load<Sprite>("Sprites/tryAgainButton"));
                        tryAgainBttn.setXY(CGameConstants.SCREEN_WIDTH / 2, CGameConstants.SCREEN_HEIGHT / 2 + 100);
                        monster.setState(2);

                    }
                }
                break;

            case STATE_WIN:
                CGameConstants.HIGH_SCORE = CurrentStageData.score;
                tryAgainInfo.TargetScore = CurrentStageData.score;
                if (!building.building.isEnded())
                {
                    CurrentStageData.cameraShake();
                }
                else
                {
                    screenDim.setImage(Resources.Load<Sprite>("Sprites/screenShade"));
                    screenDim.setX(0);
                    screenDim.setY(0);


                    if (CMouse.firstPress())
                    {
                        CurrentStageData.clearData();

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
                if (backToMenuClick())
                {
                    CurrentStageData.clearData();
                    CGame.inst().setState(new CMenuState());
                    return;
                }
                if (tryAgainClick())
                {
                    CurrentStageData.clearData();
                    CGame.inst().setState(new CSurvivalState(tryAgainInfo));
                    return;
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
        timeLeft.render();
        resultText.render();
        skills.render();
        backMenuBttn.render();
        tryAgainBttn.render();
        scoreText.render();
    }

    override public void destroy()
    {
        base.destroy();
      
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
        timeLeft.destroy();
        timeLeft = null;
        skills.destroy();
        skills = null;
        backMenuBttn.destroy();
        backMenuBttn = null;
        tryAgainBttn.destroy();
        tryAgainBttn = null;
        scoreText.destroy();
        scoreText = null;

    }
    public bool tryAgainClick()
    {
        bool clicked = false;

        if (CMouse.firstPress())
        {
            float button1MinX = tryAgainBttn.getX() - tryAgainBttn.getWidth() / 2;
            float button1MaxX = tryAgainBttn.getX() + tryAgainBttn.getWidth() / 2;
            float button1MinY = tryAgainBttn.getY() - tryAgainBttn.getHeight() / 2;
            float button1MaxY = tryAgainBttn.getY() + tryAgainBttn.getHeight() / 2;
            float mouseX = CMouse.getPos().x;
            float mouseY = CMouse.getPos().y;
            if (mouseX >= button1MinX && mouseX <= button1MaxX && mouseY >= button1MinY && mouseY <= button1MaxY)
            {
                clicked = true;
            }

        }

        return clicked;
    }

    public bool backToMenuClick()
    {
        bool clicked = false;

        if (CMouse.firstPress())
        {
            float button1MinX = backMenuBttn.getX() - backMenuBttn.getWidth() / 2;
            float button1MaxX = backMenuBttn.getX() + backMenuBttn.getWidth() / 2;
            float button1MinY = backMenuBttn.getY() - backMenuBttn.getHeight() / 2;
            float button1MaxY = backMenuBttn.getY() + backMenuBttn.getHeight() / 2;
            float mouseX = CMouse.getPos().x;
            float mouseY = CMouse.getPos().y;
            if (mouseX >= button1MinX && mouseX <= button1MaxX && mouseY >= button1MinY && mouseY <= button1MaxY)
            {
                clicked = true;
            }

        }

        return clicked;
    }
}

