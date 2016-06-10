using UnityEngine;
using System.Collections;
//using Assets.Script.game.entities;

public class CLevelState : CGameState
{
    //private CPlayer mPlayer;
    private Board mBoard;
    private Dinosaur monster;
    private Enemy building;
    private CSpriteManager mSpriteManager;
    private CText mText;     
    public CLevelState()
    {
        
        mBoard = new Board();
        monster = new Dinosaur(1);
        building = new Enemy();        
        mText = new CText("TEST", CText.alignment.TOP_CENTER);
        mText.setX(0);
        mText.setY(0);

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
        CMouse.update();
        mBoard.update();
        monster.update();
        building.update();
        mText.setText("Movements Left : " + mBoard.getMovementsLeft().ToString());       
        mText.update();
    }

    override public void render()
    {
        base.render();
        mSpriteManager.render();
        mBoard.render();
        
        monster.render();
        building.render();
        
        mText.render();
    }

    override public void destroy()
    {
        base.destroy();
        mSpriteManager.destroy();
        mSpriteManager = null;
        mText.destroy();
        mText = null;
       
    }
}
