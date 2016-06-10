using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

//namespace Assets.Script.game.entities
//{
public class token
{
    const int STATE_NORMAL = 0;
    const int STATE_MOVING = 1;

    const int SPEED = 8;

    public CSprite imgIcon { get; set; }
    public int Type { get; set; }
    public bool matched { get; set; }
    public int cascadeAmount { get; set; }
    public int current_state { get; set; }
    public float finalX { get; set; }
    public float finalY { get; set; }

    public token()
    {
        imgIcon = new CSprite();
        matched = false;
        cascadeAmount = 0;
        current_state = STATE_NORMAL;
    }
    public void setIcon(string Aicon)
    {
        imgIcon.setImage(Resources.Load<Sprite>(Aicon));
        imgIcon.setSortingLayer("Icons");
    }
    public void update()
    {
        if (imgIcon != null)
        {
            if (imgIcon.getX() != finalX | imgIcon.getY() != finalY)
            {
                current_state = STATE_MOVING;
                move();
            }
            else if (current_state == STATE_MOVING) {
                current_state = STATE_NORMAL;
            }            
           imgIcon.update();
        }
    }
    public void render()
    {
        if (imgIcon != null)
        {
            imgIcon.render();
        }
    }
    public void markMatch()
    {
        matched = true;
    }
    public void destroy()
    {
        imgIcon.destroy();

    }
    public void move()
    {
        if (imgIcon.getX() > finalX)
        {
            imgIcon.setX(imgIcon.getX() - SPEED);
        }
        if (imgIcon.getX() < finalX)
        {
            imgIcon.setX(imgIcon.getX() + SPEED);
        }

        //Corrigo la posicion si estoy mas cerca que la velocidad
        if (Math.Abs(imgIcon.getX() - finalX)< SPEED)            
        {
            imgIcon.setX(finalX);            
        }

        if (imgIcon.getY() < finalY)
        {
            imgIcon.setY(imgIcon.getY() + SPEED);
        }
        if (imgIcon.getY() > finalY)
        {
            imgIcon.setY(imgIcon.getY() - SPEED);
        }
        if (Math.Abs(imgIcon.getY() - finalY) < SPEED)
        {
            imgIcon.setY(finalY);           
        }



    }
   
    
}
//}
