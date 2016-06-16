using UnityEngine;
using System.Collections;

public class CShip : CSprite
{
    Sprite _Sprite = Resources.Load<Sprite>("Sprites/player00");

    float count = 0;
    float countMax = 3;
    Vector2 firstTouch;
    Vector2 lastTouch;
    float _distance = 0;

    public CShip()
    {
        setImage(_Sprite);

        setSortingLayer("Icons");

    }

    override public void update()
    {
        base.update();
        /*  if(CMouse.firstPress(0))
          {
              firstTouch = CMouse.getPos();
              Debug.Log("First touch is :" + firstTouch);
          }
          if (CMouse.release(0))
          {

              lastTouch = CMouse.getPos();
              Debug.Log("Last touch is : " + lastTouch);
              _distance = Mathf.Abs(Vector2.Distance(firstTouch, lastTouch));
              Debug.Log("Distance is " + _distance);
          }
         */




        count = count + 2f;


        if (count > countMax)
        {
            Debug.Log("Create");
            CShadowSprite aSpr = new CShadowSprite(0.02f);
            aSpr.setX(getX());
            aSpr.setY(getY());
            aSpr.setImage(_Sprite);
            CSpriteManager.add(aSpr);
            count = 0;

        }

    }

    override public void render()
    {
        base.render();
        setX(CMouse.getPos().x);
        setY(CMouse.getPos().y);
    }

    override public void destroy()
    {
        base.destroy();

    }
}
