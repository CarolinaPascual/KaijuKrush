using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class CMenuState:CGameState
    {
    private CSprite storyBtn;
    private CSprite endlessBtn;
    private CSprite exit;
    private int buttonClicked = 0;
    
    public CMenuState()
    {
        
        CGame.inst().setImage("Sprites/Menu");
        CGame.inst().getBakcground().setX(0);
        CGame.inst().getBakcground().setY(0);

        storyBtn = new CSprite();
        storyBtn.setImage(Resources.Load<Sprite>("Sprites/Buttons/Button_Story_Mode"));
        storyBtn.setXY(200, 732);
        storyBtn.setSortingLayer("Icons");
        endlessBtn = new CSprite();
        endlessBtn.setImage(Resources.Load<Sprite>("Sprites/Buttons/Button_Endless"));
        endlessBtn.setXY(200, 878);
        endlessBtn.setSortingLayer("Icons");
        exit = new CSprite();
        exit.setImage(Resources.Load<Sprite>("Sprites/Buttons/Button_Exit"));
        exit.setXY(200, 1020);
        exit.setSortingLayer("Icons");
    }

    override public void init()
    {
        base.init();
    }
    override public void update()
    {
        base.update();        
        CMouse.update();
        storyBtn.update();
        endlessBtn.update();
        exit.update();
        if (storyClick())
        {
            storyBtn.setImage(Resources.Load<Sprite>("Sprites/Buttons/Button_Story_Mode_p"));
            buttonClicked = 1;                 
                        
            return;
        }else
        if (endlessClick())
        {
            endlessBtn.setImage(Resources.Load<Sprite>("Sprites/Buttons/Button_Endless_p"));
            buttonClicked = 2;
           
            return;
            
        }else if (exitClick())
        {
            exit.setImage(Resources.Load<Sprite>("Sprites/Buttons/Button_Exit_p"));
            buttonClicked = 3;
            

        }
        CSpriteManager.inst().update();
        switch (buttonClicked)
        {
            case 0:            
                break;
            case 1:
                CGame.inst().setState(new CLevelState(1));
                break;
            case 2:
                CGame.inst().setState(new CKaijuSelectionState());
                break;
            case 3:
                Application.Quit();
                break;
        }
    }
    override public void render()
    {
        base.render();
        CSpriteManager.inst().render();
        storyBtn.render();
        endlessBtn.render();
        exit.render();
    }
    override public void destroy()
    {
        base.destroy();
        storyBtn.destroy();
        endlessBtn.destroy();
        exit.destroy();

    }
    public bool storyClick()
    {
        bool clicked = false;

        if (CMouse.firstPress())
        {
            float button1MinX = storyBtn.getX();
            float button1MaxX = storyBtn.getX() + storyBtn.getWidth();
            float button1MinY = storyBtn.getY();
            float button1MaxY = storyBtn.getY() + storyBtn.getHeight();
            float mouseX = CMouse.getPos().x;
            float mouseY = CMouse.getPos().y;
            if (mouseX >= button1MinX && mouseX <= button1MaxX && mouseY >= button1MinY && mouseY <= button1MaxY)
            {
                clicked = true;
            }

        }

        return clicked;
    }

    public bool endlessClick()
    {
        bool clicked = false;

        if (CMouse.firstPress())
        {
            float button1MinX = endlessBtn.getX() ;
            float button1MaxX = endlessBtn.getX() + endlessBtn.getWidth() ;
            float button1MinY = endlessBtn.getY();
            float button1MaxY = endlessBtn.getY() + endlessBtn.getHeight();
            float mouseX = CMouse.getPos().x;
            float mouseY = CMouse.getPos().y;
            if (mouseX >= button1MinX && mouseX <= button1MaxX && mouseY >= button1MinY && mouseY <= button1MaxY)
            {
                clicked = true;
            }

        }

        return clicked;
    }

    public bool exitClick()
    {
        bool clicked = false;

        if (CMouse.firstPress())
        {
            float button1MinX = exit.getX() ;
            float button1MaxX = exit.getX() + exit.getWidth();
            float button1MinY = exit.getY() ;
            float button1MaxY = exit.getY() + exit.getHeight();
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

