using UnityEngine;
using System.Collections;
//using Assets.Script.game.entities;

public class CLevelState : CGameState
{
    //private CPlayer mPlayer;
    private Board mBoard;
    private Kaiju monster;
    private Enemy building;
    private CSpriteManager mSpriteManager;
    private CText mText;
    //private CShip mShip; 
    public CLevelState()
    {
        //mPlayer = new CPlayer ();
        mBoard = new Board();
        monster = new Kaiju(1);
        building = new Enemy();
        //mShip = new CShip();
        mText = new CText("TEST", CText.alignment.TOP_CENTER);
        mText.setX(100);
        mText.setY(100);

        mSpriteManager = new CSpriteManager();

    }

    override public void init()
    {
        base.init();
    }

    override public void update()
    {
        base.update();
        mSpriteManager.update();
        //mPlayer.update ();
        CMouse.update();
        mBoard.update();
        monster.update();
        building.update();
        //mShip.update(); 
        mText.update();
    }

    override public void render()
    {
        base.render();
        mSpriteManager.render();
        mBoard.render();
        //mPlayer.render ();
        monster.render();
        building.render();
        //mShip.render(); 
        mText.render();
    }

    override public void destroy()
    {
        base.destroy();
        mSpriteManager.destroy();
        mSpriteManager = null;
        mText.destroy();
        mText = null;
        //mPlayer.destroy ();
        //mPlayer = null;
        //mShip.destroy();
        //mShip = null; 
    }
}
