using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

//namespace Assets.Script.game.entities
//{
public class Enemy
{
    private const int STATE_NORMAL = 0;
    private const int STATE_DESTRUCTION = 1;
    private const int BUILDING = 0;
    private const int HOUSE = 1;
    private int type;
    public int currentState;
    public CAnimatedSprite building { get; set; }

    public Enemy(int aType)
    {
        building = new CAnimatedSprite();
        building.setName("Building");
        type = aType;
        switch (type)
        {
            case BUILDING:
                building.setFrames(Resources.LoadAll<Sprite>("Sprites/Building"));
                break;
            case HOUSE:
                building.setFrames(Resources.LoadAll<Sprite>("Sprites/House"));
                break;
        }        
        setState(STATE_NORMAL);
        building.setY(535);
        building.setX((CGameConstants.SCREEN_WIDTH / 4) * 3 + 30);
        building.setSortingLayer("Icons");
    }
    public void update()
    {
        building.update();
    }
    public void render()
    {
        building.render();
    }

       public void setState(int aState)
    {
        building.setState(aState);
        currentState = aState;
        building.setVisible(true);
        switch (getState())
        {
            case STATE_NORMAL:
                building.initAnimation(1, 1, 6, false);
                break;
            case STATE_DESTRUCTION:
                switch (type)
                {
                    case BUILDING:
                        building.initAnimation(2, 27, 6, false);
                        break;
                    case HOUSE:
                        building.initAnimation(2, 9, 6, false);
                        break;
                }
                
                
                break;
            
        }
    }
    public int getState()
    {
        return currentState;
    }
    public void destroy()
    {
        building.destroy();
    }
}
//}
