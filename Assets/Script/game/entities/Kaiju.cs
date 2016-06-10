using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

//namespace Assets.Script.game.entities
//{
public class Kaiju
{
    public CAnimatedSprite beast { get; set; }
    public int type { get; set; }
    public int prefferedFood { get; set; }
    public Sprite[] originalSprites { get; set; }

    public Kaiju()
    {
       

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
