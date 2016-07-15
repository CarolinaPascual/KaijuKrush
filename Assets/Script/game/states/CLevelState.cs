using UnityEngine;
using System.Collections;
//using Assets.Script.game.entities;

public class CLevelState : CGameState
{
    const int STATE_PLAYING = 0;
    const int STATE_WIN = 1;
    const int STATE_LOSE = 2;
    const int GO_BACKMENU = 3;
    const int GO_TRYAGAIN = 4;
    const int STATE_PAUSE = 5;
    //private CPlayer mPlayer;
    private int current_state;
    private Board mBoard;
    private Kaiju monster;
    private Enemy building;    
    private CText mText;
   
    private CSprite screenDim;
    private CSprite btnNextScreen;
    
    private SkillBar skills;
    private CSprite backMenuBttn;
    private CSprite tryAgainBttn;
    private CSprite optionsBttn;
    


    public CLevelState(int stageNumber)
    {
        SoundList.instance.playLevelMusic();
        CInfo stageInfo = LevelsInfo.getLevel(stageNumber);
        CGame.inst().setImage("Sprites/level_Background0" + stageInfo.building.ToString());
        CurrentStageData.currentStage = stageNumber;
        switch (stageInfo.Kaiju)
        {
            case 1:
                monster = new Dinosaur(stageInfo.startStage,stageInfo.firstStage,stageInfo.secondStage);
                break;
            case 2:
                monster = new Kong(stageInfo.startStage, stageInfo.firstStage, stageInfo.secondStage);
                break;
            case 3:
                monster = new Kraken(stageInfo.startStage, stageInfo.firstStage, stageInfo.secondStage);
                break;
        }
        current_state = STATE_PLAYING;
        CurrentStageData.difficulty = stageInfo.dif;
        mBoard = new Board(0);
        //monster = new Kong(1, 53, 76);  
        building = new Enemy(stageInfo.building);
        mText = new CText("TEST", CText.alignment.TOP_CENTER);
        mText.setX(0);
        mText.setY(0);
        mText.setColor(Color.black);
        
        
        
        
        mBoard.movementsLeft = stageInfo.movements; // MOVE TO CLASS
        mBoard.targetScore = stageInfo.TargetScore; // MOVE TO CLASS
        float scoreCoefficient = (float)70 / (float)mBoard.targetScore;
        skills = new SkillBar(stageInfo.Kaiju);
        CurrentStageData.assignData(monster, mBoard, scoreCoefficient,skills);
        screenDim = new CSprite();
        screenDim.setSortingLayer("ScreenShade");
        screenDim.setName("Sombra");
        backMenuBttn = new CSprite();
        backMenuBttn.setSortingLayer("TextUI");        
        tryAgainBttn = new CSprite();
        tryAgainBttn.setSortingLayer("TextUI");
        btnNextScreen = new CSprite();
        btnNextScreen.setSortingLayer("TextUI");
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
                SoundList.instance.playMenuMusic();
                CGame.inst().setState(new CMenuState());
                return;
            }
            

            return;
        }
        mBoard.update();
        monster.update();
        building.update();
        screenDim.update();
        mText.setText("Moves: " + mBoard.getMovementsLeft().ToString());
        mText.update();
        btnNextScreen.update();
        optionsBttn.update();
        skills.update();
        backMenuBttn.update();
        tryAgainBttn.update();
        switch (current_state)
        {
            case STATE_PLAYING:
                if (optionsClick())
                {
                    current_state = STATE_PAUSE;
                    backMenuBttn.setImage(Resources.Load<Sprite>("Sprites/BackMenuButton"));
                    backMenuBttn.setXY(CGameConstants.SCREEN_WIDTH / 2, CGameConstants.SCREEN_HEIGHT / 2);
                    screenDim.setImage(Resources.Load<Sprite>("Sprites/screenShade"));
                    
                    screenDim.setX(351);
                    screenDim.setY(830);
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
                        screenDim.setX(351);
                        screenDim.setY(830);
                        //nextScreen.setVisible(true);
                        current_state = STATE_LOSE;                       
                        backMenuBttn.setImage(Resources.Load<Sprite>("Sprites/BackMenuButton"));
                        backMenuBttn.setXY(CGameConstants.SCREEN_WIDTH / 2, CGameConstants.SCREEN_HEIGHT / 2);
                        tryAgainBttn.setImage(Resources.Load<Sprite>("Sprites/tryAgainButton"));
                        tryAgainBttn.setXY(CGameConstants.SCREEN_WIDTH / 2, CGameConstants.SCREEN_HEIGHT / 2+ 150);
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
                    screenDim.setX(351);
                    screenDim.setY(830);
                    btnNextScreen.setImage(Resources.Load<Sprite>("Sprites/Buttons/Button_Continue"));
                    btnNextScreen.setXY(CGameConstants.SCREEN_WIDTH / 2, CGameConstants.SCREEN_HEIGHT / 2);

                    if (nextScreenClick())
                    {
                        
                        CurrentStageData.clearData();                        
                        if (CurrentStageData.currentStage>= LevelsInfo.getLevelsAmount())
                        {
                            SoundList.instance.stopMusic();
                            SoundList.instance.playMenuMusic();
                            CGame.inst().setState(new CMenuState());
                            
                        }
                        else { 
                        CGame.inst().setState(new CLevelState(CurrentStageData.currentStage+1));
                    }
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
                    backMenuBttn.setImage(Resources.Load<Sprite>("Sprites/BackMenuButton_P"));
                    current_state = GO_BACKMENU;
                    return;
                }
                if (tryAgainClick())
                {
                    tryAgainBttn.setImage(Resources.Load<Sprite>("Sprites/tryAgainButton_P"));
                    current_state = GO_TRYAGAIN;
                    return;
                }
                break;
            case GO_BACKMENU:
                CurrentStageData.clearData();
                SoundList.instance.stopMusic();
                SoundList.instance.playMenuMusic();
                CGame.inst().setState(new CMenuState());
                return;
                
            case GO_TRYAGAIN:
                CurrentStageData.clearData();
                CGame.inst().setState(new CLevelState(CurrentStageData.currentStage));
                return;
            
                
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
        optionsBttn.render();
        mText.render();
        
        skills.render();
        backMenuBttn.render();
        tryAgainBttn.render();
        btnNextScreen.render();
    }

    override public void destroy()
    {
        base.destroy();
               
        mText.destroy();
        mText = null;
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
        
        skills.destroy();
        skills = null;
        backMenuBttn.destroy();
        backMenuBttn = null;
        tryAgainBttn.destroy();
        tryAgainBttn = null;
        btnNextScreen.destroy();
        btnNextScreen = null;

    }
    public bool tryAgainClick()
    {
        bool clicked = false;

        if (CMouse.firstPress())
        {
            float button1MinX = tryAgainBttn.getX() - tryAgainBttn.getWidth() / 2;
            float button1MaxX = tryAgainBttn.getX() + tryAgainBttn.getWidth() / 2;
            float button1MinY = tryAgainBttn.getY() - tryAgainBttn.getHeight() /2;
            float button1MaxY = tryAgainBttn.getY() + tryAgainBttn.getHeight() /2;
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

}
