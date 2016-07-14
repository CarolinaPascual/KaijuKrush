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
    const int STATE_PAUSE = 3;
    private int current_state;
    private Board mBoard;
    private Kaiju monster;
    private Enemy building;

    private CText scoreText;
    
    private CSprite screenDim;
    private CText timeLeft;
    private SkillBar skills;
    private CSprite backMenuBttn;
    private CSprite tryAgainBttn;
    private Timer mTimer;
    private CInfo tryAgainInfo;
    private CSprite optionsBttn;
    private CSprite btnNextScreen;

    public CSurvivalState(CInfo stageInfo)
    {
        CGame.inst().setImage("Sprites/level_Background00");
        CurrentStageData.clearData();
        SoundList.instance.playLevelMusic();
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
        

        timeLeft = new CText("Time: ", CText.alignment.TOP_CENTER);
        timeLeft.setX(0);
        timeLeft.setY(0);
        timeLeft.setColor(Color.black);

        scoreText = new CText("SCORE :", CText.alignment.TOP_CENTER);
        scoreText.setX(400);
        scoreText.setY(0);
        scoreText.setColor(Color.black);
        btnNextScreen = new CSprite();
        btnNextScreen.setSortingLayer("TextUI");

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
        optionsBttn = new CSprite();
        optionsBttn.setImage(Resources.Load<Sprite>("Sprites/Buttons/Pause_Button"));
        optionsBttn.setXY(680, 40);
        optionsBttn.setSortingLayer("TextUI");

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
        if (current_state == STATE_PAUSE)
        {
            if (nextScreenClick())
            {
                screenDim.setImage(null);
                backMenuBttn.setImage(null);
                btnNextScreen.setImage(null);
                current_state = STATE_PLAYING;
            }
            if (backToMenuClick())
            {
                SoundList.instance.stopMusic();
                CGame.inst().setState(new CMenuState());
                return;
            }


            return;
        }
        mBoard.update();
        monster.update();
        building.update();
        screenDim.update();
        mTimer.update();
        optionsBttn.update();
        skills.update();
        backMenuBttn.update();
        tryAgainBttn.update();
        btnNextScreen.update();
        timeLeft.setText("TIME: " + (int)(CurrentStageData.currentTimer.getTimeLeft()));
        timeLeft.update();
        scoreText.setText("SCORE: " + (CurrentStageData.score * 10));
        scoreText.update();
        switch (current_state)
        {
            case STATE_PLAYING:
                if (optionsClick())
                {
                    current_state = STATE_PAUSE;
                    backMenuBttn.setImage(Resources.Load<Sprite>("Sprites/BackMenuButton"));
                    backMenuBttn.setXY(CGameConstants.SCREEN_WIDTH / 2, CGameConstants.SCREEN_HEIGHT / 2);
                    screenDim.setImage(Resources.Load<Sprite>("Sprites/screenShade"));
                    screenDim.setX(0);
                    screenDim.setY(0);
                    btnNextScreen.setImage(Resources.Load<Sprite>("Sprites/Buttons/Button_Continue"));
                    btnNextScreen.setXY(CGameConstants.SCREEN_WIDTH / 2, CGameConstants.SCREEN_HEIGHT / 2 + 150);
                    return;
                }
                if (mBoard.current_state == 0)
                {
                    if (CurrentStageData.currentKaiju.scale >= 100)
                    {
                        current_state = STATE_WIN;
                        SoundList.instance.stopMusic();
                        monster.setState(4);
                        building.setState(1);

                    }
                    else
                    {


                        screenDim.setImage(Resources.Load<Sprite>("Sprites/screenShade"));
                        screenDim.setX(0);
                        screenDim.setY(0);

                        current_state = STATE_LOSE;
                        
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
                //System.IO.File.WriteAllText("score.txt", CurrentStageData.score.ToString());
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
                    backMenuBttn.setImage(Resources.Load<Sprite>("Sprites/BackMenuButton"));
                    backMenuBttn.setXY(CGameConstants.SCREEN_WIDTH / 2, CGameConstants.SCREEN_HEIGHT / 2);
                    tryAgainBttn.setImage(Resources.Load<Sprite>("Sprites/tryAgainButton"));
                    tryAgainBttn.setXY(CGameConstants.SCREEN_WIDTH / 2, CGameConstants.SCREEN_HEIGHT / 2 + 100);

                    if (backToMenuClick())
                    {
                        CurrentStageData.clearData();
                        SoundList.instance.stopMusic();
                        CGame.inst().setState(new CMenuState());
                        return;
                    }
                    if (tryAgainClick())
                    {
                        CurrentStageData.clearData();
                        CGame.inst().setState(new CSurvivalState(tryAgainInfo));
                        return;
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
                    SoundList.instance.stopMusic();
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
        optionsBttn.render();
        skills.render();
        backMenuBttn.render();
        tryAgainBttn.render();
        scoreText.render();
        btnNextScreen.render();
    }

    override public void destroy()
    {
        base.destroy();
        btnNextScreen.destroy();
        btnNextScreen = null;
        optionsBttn.destroy();
        optionsBttn = null; 
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
    public bool optionsClick()
    {
        bool clicked = false;

        if (CMouse.firstPress())
        {
            float button1MinX = optionsBttn.getX() - optionsBttn.getWidth() / 2;
            float button1MaxX = optionsBttn.getX() + optionsBttn.getWidth() / 2;
            float button1MinY = optionsBttn.getY() - optionsBttn.getHeight() / 2;
            float button1MaxY = optionsBttn.getY() + optionsBttn.getHeight() / 2;
            float mouseX = CMouse.getPos().x;
            float mouseY = CMouse.getPos().y;
            if (mouseX >= button1MinX && mouseX <= button1MaxX && mouseY >= button1MinY && mouseY <= button1MaxY)
            {
                clicked = true;
            }

        }

        return clicked;
    }
    public bool nextScreenClick()
    {
        bool clicked = false;

        if (CMouse.firstPress())
        {
            float button1MinX = btnNextScreen.getX() - btnNextScreen.getWidth() / 2;
            float button1MaxX = btnNextScreen.getX() + btnNextScreen.getWidth() / 2;
            float button1MinY = btnNextScreen.getY() - btnNextScreen.getHeight() / 2;
            float button1MaxY = btnNextScreen.getY() + btnNextScreen.getHeight() / 2;
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

