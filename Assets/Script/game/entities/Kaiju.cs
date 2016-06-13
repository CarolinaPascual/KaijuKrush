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
    public int stage { get; set; }
    public int prefferedFood { get; set; }    
    public float scale { get; set; }
    public float growCounter { get; set; }
    public int currentState { get; set; }
    public int firstBreakpoint { get; set; }
    public int secondBreakpoint { get; set; }
    
    

    public Kaiju()
    {
       

    }
    virtual public void update()
    {
        beast.update();
    }
    public void render()
    {
        beast.render();
    }
    public void destroy()
    {

    }
    virtual public void setState(int aState)
    {

    }
    public int getState()
    {
        return currentState;
    }
     public void addGrow(float grow)
    {
        growCounter += grow;
    }
}
//}
