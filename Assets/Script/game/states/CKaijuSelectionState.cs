using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class CKaijuSelectionState:CGameState
    {
    private KaijuSelectButton dinoBtn;
    private KaijuSelectButton kongBtn;
    private KaijuSelectButton krakenBtn;
    private CSprite backBtn;
    private int selected = 0;

    public CKaijuSelectionState()
    {
        SoundList.instance.playSelection();
        CGame.inst().setImage("Sprites/Menu-Sin-Logo");
        CGame.inst().getBakcground().setX(0);
        CGame.inst().getBakcground().setY(0);

        dinoBtn = new KaijuSelectButton();
        dinoBtn.setImage(Resources.Load<Sprite>("Sprites/KaijuSelection/dinoBtn"));
        dinoBtn.setPosition(-dinoBtn.getWidth(), 0, 0, 0);        
        dinoBtn.setSortingLayer("Icons");

        kongBtn = new KaijuSelectButton();
        kongBtn.setImage(Resources.Load<Sprite>("Sprites/KaijuSelection/kongBtn"));
        kongBtn.setPosition(kongBtn.getWidth(), dinoBtn.getHeight(), 0, dinoBtn.getHeight());
        kongBtn.setSortingLayer("Icons");

        krakenBtn = new KaijuSelectButton();
        krakenBtn.setImage(Resources.Load<Sprite>("Sprites/KaijuSelection/krakenbtn"));
        krakenBtn.setPosition(-krakenBtn.getWidth(), kongBtn.getY() + kongBtn.getHeight(), 0, kongBtn.getY() + kongBtn.getHeight());        
        krakenBtn.setSortingLayer("Icons");

        backBtn = new CSprite();
        backBtn.setImage(Resources.Load<Sprite>("Sprites/Buttons/back_button"));
        backBtn.setXY(650, 1220);
        backBtn.setSortingLayer("TextUI");
    }

    override public void init()
    {
        base.init();
    }
    override public void update()
    {
        base.update();
        CMouse.update();
        backBtn.update();
        dinoBtn.update();
        kongBtn.update();
        krakenBtn.update();

        if (backClick())
        {
            SoundList.instance.playMenuMusic();
            CGame.inst().setState(new CMenuState());
            return;
        }
        
        switch (selected)
        {
           case 0:
                if (dinoClick())
                {

                   
                    kongBtn.leave();
                    krakenBtn.leave();                    
                    selected = 1;
                    return;
                }
                else
        if (kongClick())
                {
                    
                    dinoBtn.leave();
                    krakenBtn.leave();
                   
                    selected = 2;
                    return;

                }
                else if (krakenClick())
                {
                    
                    kongBtn.leave();
                    dinoBtn.leave();
                    
                    selected  = 3;
                    return;
                }
                break;
            case 1:
                if (kongBtn.currentState == 1 & krakenBtn.currentState== 1)
                {
                    CInfo aux1 = new CInfo(1, 1, 50, 70, CGameConstants.HIGH_SCORE, 0, 0, 0);
                    CGame.inst().setState(new CSurvivalState(aux1));
                }
                break;
            case 2:
                if (dinoBtn.currentState == 1 & krakenBtn.currentState == 1)
                {
                    CInfo aux1 = new CInfo(2, 1, 50, 70, CGameConstants.HIGH_SCORE, 0, 0, 0);
                    CGame.inst().setState(new CSurvivalState(aux1));
                }
                break;
            case 3:
                if (dinoBtn.currentState == 1 & kongBtn.currentState == 1)
                {
                    CInfo aux1 = new CInfo(3, 1, 50, 70, CGameConstants.HIGH_SCORE, 0, 0, 0);
                    CGame.inst().setState(new CSurvivalState(aux1));
                }
                break;
        }

        
        CSpriteManager.inst().update();
    }
    override public void render()
    {
        base.render();
        CSpriteManager.inst().render();
        backBtn.render();
        dinoBtn.render();
        kongBtn.render();
        krakenBtn.render();
        
    }
    override public void destroy()
    {
        base.destroy();
        dinoBtn.destroy();
        kongBtn.destroy();
        krakenBtn.destroy();
        backBtn.destroy();

    }
    public bool dinoClick()
    {
        bool clicked = false;

        if (CMouse.firstPress())
        {
            float button1MinX = dinoBtn.getX();
            float button1MaxX = dinoBtn.getX() + dinoBtn.getWidth();
            float button1MinY = dinoBtn.getY();
            float button1MaxY = dinoBtn.getY() + dinoBtn.getHeight();
            float mouseX = CMouse.getPos().x;
            float mouseY = CMouse.getPos().y;
            if (mouseX >= button1MinX && mouseX <= button1MaxX && mouseY >= button1MinY && mouseY <= button1MaxY)
            {
                clicked = true;
            }

        }

        return clicked;
    }

    public bool kongClick()
    {
        bool clicked = false;

        if (CMouse.firstPress())
        {
            float button1MinX = kongBtn.getX();
            float button1MaxX = kongBtn.getX() + kongBtn.getWidth();
            float button1MinY = kongBtn.getY();
            float button1MaxY = kongBtn.getY() + kongBtn.getHeight();
            float mouseX = CMouse.getPos().x;
            float mouseY = CMouse.getPos().y;
            if (mouseX >= button1MinX && mouseX <= button1MaxX && mouseY >= button1MinY && mouseY <= button1MaxY)
            {
                clicked = true;
            }

        }

        return clicked;
    }

    public bool krakenClick()
    {
        bool clicked = false;

        if (CMouse.firstPress())
        {
            float button1MinX = krakenBtn.getX();
            float button1MaxX = krakenBtn.getX() + krakenBtn.getWidth();
            float button1MinY = krakenBtn.getY();
            float button1MaxY = krakenBtn.getY() + krakenBtn.getHeight();
            float mouseX = CMouse.getPos().x;
            float mouseY = CMouse.getPos().y;
            if (mouseX >= button1MinX && mouseX <= button1MaxX && mouseY >= button1MinY && mouseY <= button1MaxY)
            {
                clicked = true;
            }

        }

        return clicked;
    }

    public bool backClick()
    {
        bool clicked = false;

        if (CMouse.firstPress())
        {
            float button1MinX = backBtn.getX();
            float button1MaxX = backBtn.getX() + backBtn.getWidth();
            float button1MinY = backBtn.getY();
            float button1MaxY = backBtn.getY() + krakenBtn.getHeight();
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

