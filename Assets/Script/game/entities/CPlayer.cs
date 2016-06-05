using UnityEngine;
using System.Collections;

public class CPlayer : CGameObject
{
    private CSprite mShipSprite;

    private const int STATE_MOVE = 0;
    private const int STATE_FREEZE = 1;
    private const int STATE_JUMP_DYING = 2;

    private const float SPEED = 900;

    public CPlayer()
    {
        mShipSprite = new CSprite();
        mShipSprite.setImage(Resources.Load<Sprite>("Sprites/player00"));
        setX(CGameConstants.SCREEN_WIDTH / 2);
        setY(CGameConstants.SCREEN_HEIGHT - 100);

        setState(STATE_MOVE);

        render();
    }

    override public void update()
    {
        base.update();
        mShipSprite.update();

        if (getState() == STATE_MOVE)
        {
            //if (CKeyPoll.firstPress(CKeyPoll.SPACE))
            //{
            //	fire();
            //}

            //if (CKeyPoll.firstPress(CKeyPoll.UP))
            //{
            //	setVelY( -15);
            //}

            if (!CKeyboard.pressed(CKeyboard.LEFT) && !CKeyboard.pressed(CKeyboard.RIGHT))
            {
                setVelX(0);
            }
            if (CKeyboard.pressed(CKeyboard.LEFT))
            {
                setVelX(-SPEED);
            }
            else if (CKeyboard.pressed(CKeyboard.RIGHT))
            {
                setVelX(SPEED);
            }

            //if (getY() < getRadius())
            //{
            //	setY(getRadius());
            //	setVelY(0);
            //}

            // Control de borde abajo.
            //if (getY() > mYFloor)
            //{
            //	setY(mYFloor);
            //	setVelY(0);
            //}

            //var enemy:CGenericEnemy = CEnemyManager.inst().collides(this) as CGenericEnemy;
            //if (enemy != null)
            //{
            //	hitByEnemy();
            //	// TODO: Matar el enemigo.
            //}
        }
        else if (getState() == STATE_FREEZE)
        {
            if (getTimeState() > 1.0f)
            {
                setState(STATE_JUMP_DYING);
            }
        }
        else if (getState() == STATE_JUMP_DYING)
        {
            if (getTimeState() > 2.0f)
            {
                setState(STATE_MOVE);
            }
        }
    }

    override public void render()
    {
        base.render();

        mShipSprite.setX(getX());
        mShipSprite.setY(getY());

        mShipSprite.render();
    }

    override public void destroy()
    {
        base.destroy();
        mShipSprite.destroy();
        mShipSprite = null;
    }
}
