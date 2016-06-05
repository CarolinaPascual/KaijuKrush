using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

//namespace Assets.Script.game.entities
//{
public class Enemy
{
    public CSprite building { get; set; }

    public Enemy()
    {
        building = new CSprite();
        building.setImage(Resources.Load<Sprite>("Sprites/Placeholders_Prototype/Building"));
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
}
//}
