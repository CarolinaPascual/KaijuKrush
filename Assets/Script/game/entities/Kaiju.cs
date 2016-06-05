using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

//namespace Assets.Script.game.entities
//{
public class Kaiju
{
    public CSprite beast { get; set; }
    public int type { get; set; }

    public Kaiju(int aType)
    {
        type = aType;
        beast = new CSprite();
        beast.setImage(Resources.Load<Sprite>("Sprites/Placeholders_Prototype/Kaijus/001/Large000"));
        beast.setY(535);
        beast.setX(CGameConstants.SCREEN_WIDTH / 4 - 25);
        beast.setSortingLayer("Icons");

    }
    public void update()
    {
        beast.update();
    }
    public void render()
    {
        beast.render();
    }
}
//}
