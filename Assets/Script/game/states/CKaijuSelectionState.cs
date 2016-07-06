using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class CKaijuSelectionState:CGameState
    {
    private CSprite dinoBtn;
    private CSprite kongBtn;
    private CSprite krakenBtn;

    public CKaijuSelectionState()
    {
        CGame.inst().setImage("Sprites/Menu");
        CGame.inst().getBakcground().setX(0);
        CGame.inst().getBakcground().setY(0);

        dinoBtn = new CSprite();
        dinoBtn.setImage(Resources.Load<Sprite>("Sprites/KaijuSelection/dinoBtn"));
        dinoBtn.setXY(0,0);
        dinoBtn.setSortingLayer("Icons");
        kongBtn = new CSprite();
        kongBtn.setImage(Resources.Load<Sprite>("Sprites/KaijuSelection/kongBtn"));
        kongBtn.setXY(0,dinoBtn.getHeight());
        kongBtn.setSortingLayer("Icons");
        krakenBtn = new CSprite();
        krakenBtn.setImage(Resources.Load<Sprite>("Sprites/KaijuSelection/krakenbtn"));
        krakenBtn.setXY(0,kongBtn.getY()+kongBtn.getHeight());
        krakenBtn.setSortingLayer("Icons");
    }

    override public void init()
    {
        base.init();
    }
    override public void update()
    {
        base.update();
        CMouse.update();
        dinoBtn.update();
        kongBtn.update();
        krakenBtn.update();
        if (dinoClick())
        {

            CInfo aux1 = new CInfo(1, 1, 50, 70, 300, 0, 0, 0);
            CGame.inst().setState(new CSurvivalState(aux1));
            return;
        }
        else
        if (kongClick())
        {
            CInfo aux1 = new CInfo(2, 1, 50, 70, 300, 0, 0, 0);
            CGame.inst().setState(new CSurvivalState(aux1));
            return;

        }
        else if(krakenClick()){
            CInfo aux1 = new CInfo(3, 1, 50, 70, 300, 0, 0, 0);
            CGame.inst().setState(new CSurvivalState(aux1));
            return;
        }
        CSpriteManager.inst().update();
    }
    override public void render()
    {
        base.render();
        CSpriteManager.inst().render();
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
}

