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
    public CSprite button01 { get; set; }
    public CSprite button02 { get; set; }
    public CSprite emptyBar { get; set; }
    public CSprite barFill { get; set; }
    private int button2Type { get; set; }

    public SkillBar(int aType)
    {
        currentState = STATE_NORMAL;
        scale = 0;
        scaleCounter = 0;
        button01 = new CSprite();
        button01.setImage(Resources.Load<Sprite>("Sprites/SkillBar/Button-Skill1-Unable"));
        button01.setSortingLayer("TextUI");
        button01.setXY(40, 410);

        button02 = new CSprite();
        button2Type = aType;
        button02.setImage(Resources.Load<Sprite>("Sprites/SkillBar/Button-Skill2-Unable0" + button2Type.ToString()));
        button02.setSortingLayer("TextUI");
        button02.setXY(40, 510);

        barFill = new CSprite();
        barFill.setImage(Resources.Load<Sprite>("Sprites/SkillBar/Energy_BarFull2"));
        barFill.setX(40);
        barFill.setY(300);
        barFill.setScaleY(scale);
        

        emptyBar = new CSprite();
        emptyBar.setImage(Resources.Load<Sprite>("Sprites/SkillBar/Energy_Bar2"));
        emptyBar.setX(40);
        emptyBar.setY(300);
        

    }

    public void update()
    {
        button01.update();
        button02.update();
        barFill.update();
        emptyBar.update();
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
                button01.setImage(Resources.Load<Sprite>("Sprites/SkillBar/Button-Skill1-Active"));
                button02.setImage(Resources.Load<Sprite>("Sprites/SkillBar/Button-Skill2-Active0" + button2Type.ToString()));
                currentState = STATE_FULL;
            }

        }

        if (currentState == STATE_FULL)
        {
            if (skill1Click() && CurrentStageData.currentBoard.current_state != 0)
            {
                CurrentStageData.currentKaiju.firstPower();
                scaleCounter = -100;
                button01.setImage(Resources.Load<Sprite>("Sprites/SkillBar/Button-Skill1-Unable"));
                button02.setImage(Resources.Load<Sprite>("Sprites/SkillBar/Button-Skill2-Unable0" + button2Type.ToString()));
                currentState = STATE_NORMAL;
            }
            else if (skill2Click() && CurrentStageData.currentBoard.current_state != 0)
            {
                
                CAnimatedSprite skillAnim = new CAnimatedSprite();
                Skill2Animation auxSkill = new Skill2Animation(button2Type);
                CSpriteManager.inst().add(auxSkill);

                scaleCounter = -100;
                button01.setImage(Resources.Load<Sprite>("Sprites/SkillBar/Button-Skill1-Unable"));
                button02.setImage(Resources.Load<Sprite>("Sprites/SkillBar/Button-Skill2-Unable0" + button2Type.ToString()));
                currentState = STATE_NORMAL;
            }
        }
    }
    public void render()
    {
        button01.render();
        button02.render();
        barFill.render();
        emptyBar.render();

        

    }

    private bool skill1Click()
    {
        bool clicked = false;

        if (CMouse.firstPress())
        {
            float button1MinX = button01.getX() - button01.getWidth() / 2;
            float button1MaxX = button01.getX() + button01.getWidth() / 2;
            float button1MinY = button01.getY() - button01.getHeight();
            float button1MaxY = button01.getY();
            float mouseX = CMouse.getPos().x;
            float mouseY = CMouse.getPos().y;
            if (mouseX >= button1MinX && mouseX <= button1MaxX && mouseY >= button1MinY && mouseY <= button1MaxY)
            {
                clicked = true;
            }
            
        }

        return clicked;
    }
    private bool skill2Click()
    {
        bool clicked = false;

        if (CMouse.firstPress())
        {
            float button2MinX = button02.getX() - button02.getWidth() / 2;
            float button2MaxX = button02.getX() + button02.getWidth() / 2;
            float button2MinY = button02.getY() - button02.getHeight();
            float button2MaxY = button02.getY();
            float mouseX = CMouse.getPos().x;
            float mouseY = CMouse.getPos().y;
            if (mouseX >= button2MinX && mouseX <= button2MaxX && mouseY >= button2MinY && mouseY <= button2MaxY)
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
        button01.destroy();
        button02.destroy();
        emptyBar.destroy();
        barFill.destroy();
    }
   
}

