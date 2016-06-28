using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class SkillBar
{
    private const int STATE_NORMAL = 0;
    private const int STATE_FULL = 1;
    private const int pointsToPower = 50;
    private int currentState = 0;
    public float scale { get; set; }
    private float scaleCounter { get; set; }
    public CSprite button { get; set; }
    public CSprite emptyBar { get; set; }
    public CSprite barFill { get; set; }
    

    public SkillBar()
    {
        currentState = STATE_NORMAL;
        scale = 0;
        scaleCounter = 0;
        button = new CSprite();
        button.setImage(Resources.Load<Sprite>("Sprites/SkillBar/Button-Skill1-Unable"));
        button.setSortingLayer("Icons");
        button.setXY(40, 520);

        barFill = new CSprite();
        barFill.setImage(Resources.Load<Sprite>("Sprites/SkillBar/Energy_BarFull"));
        barFill.setX(40);
        barFill.setY(425);
        barFill.setScaleY(scale);

        emptyBar = new CSprite();
        emptyBar.setImage(Resources.Load<Sprite>("Sprites/SkillBar/Energy_Bar"));
        emptyBar.setX(40);
        emptyBar.setY(425);

    }

    public void update()
    {
        button.update();
        barFill.update();
        emptyBar.update();
        
    }
    public void render()
    {
        button.render();
        barFill.render();
        emptyBar.render();

        if (scaleCounter != 0 & scale <= 100)
        {
            float aux = scaleCounter / 10;
            scale += aux;
            scaleCounter -= aux;
            barFill.setScaleY(scale);
            if (scale >= 100)
            {
                scale = 100;
                scaleCounter = 0;
                button.setImage(Resources.Load<Sprite>("Sprites/SkillBar/Button-Skill1-Active"));
                currentState = STATE_FULL;
            }

        }

        if (currentState == STATE_FULL)
        {
            if (skill1Click() && CurrentStageData.currentBoard.current_state != 0)
            {               
               CurrentStageData.currentKaiju.firstPower();
               scaleCounter = -100;
               button.setImage(Resources.Load<Sprite>("Sprites/SkillBar/Button-Skill1-Unable"));
                currentState = STATE_NORMAL;
            }
        }

    }

    private bool skill1Click()
    {
        bool clicked = false;

        if (CMouse.firstPress())
        {
            float button1MinX = button.getX() - button.getWidth() / 2;
            float button1MaxX = button.getX() + button.getWidth() / 2;
            float button1MinY = button.getY() - button.getHeight();
            float button1MaxY = button.getY();
            float mouseX = CMouse.getPos().x;
            float mouseY = CMouse.getPos().y;
            if (mouseX >= button1MinX && mouseX <= button1MaxX && mouseY >= button1MinY && mouseY <= button1MaxY)
            {
                clicked = true;
            }
            
        }

        return clicked;
    }

    public void addScore(float aScore)
    {
         if (scale <= 100 & aScore > 0)
        {
            scaleCounter += (aScore * 100) / pointsToPower;            
        }
    }

    public void destroy()
    {
        button.destroy();
        emptyBar.destroy();
        barFill.destroy();
}
}

