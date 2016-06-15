using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

//namespace Assets.Script.game.entities
//{
public class Tile
{
    public CSprite background { get; set; }
    public token food { get; set; }
    public bool selected { get; set; }

    public Tile(string Abackground)
    {
        background = new CSprite();
        background.setImage(Resources.Load<Sprite>(Abackground));
        background.setSortingLayer("Background");
        food = new token();
    }

    public void update()
    {
        background.update();
        food.update();

    }
    public void render()
    {

        food.render();
        background.render();

    }
    public void select()
    {
        string spriteName = "Sprites/Placeholders_Prototype/tile_Selected01";
        background.setImage(Resources.Load<Sprite>(spriteName));
        background.setSortingLayer("Background");
        selected = true;
    }
    public void deselect()
    {
        string spriteName = "Sprites/Placeholders_Prototype/tile_Unselected01";
        background.setImage(Resources.Load<Sprite>(spriteName));
        background.setSortingLayer("Background");
        selected = false;
    }
    public void setIcon(string aIcon)
    {
        food.setIcon(aIcon);
    }

    public void clearFood()
    {
        food.imgIcon.setImage(null);
        food.Type = -1;
        food.matched = false;
        food.cascadeAmount = 0;
    }
    public void destroy()
    {
        background.destroy();
        food.destroy();

}

}
//}
