using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    class CCreditsState:CGameState
    {
    public CCreditsState()
    {
        CGame.inst().setImage("Sprites/credits");
        CGame.inst().getBakcground().setX(0);
        CGame.inst().getBakcground().setY(0);
    }
    public override void update()
    {
        base.update();
        if (CMouse.firstPress())
        {
            CGame.inst().setState(new CMenuState());
        }
    }

}

