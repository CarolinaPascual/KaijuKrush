using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    class CMenuState:CGameState
    {
    

    public CMenuState()
    {
        CGame.inst().setImage("Sprites/Main-Menu-PH");
        CGame.inst().getBakcground().setX(0);
        CGame.inst().getBakcground().setY(0);
        
        
    }

    override public void init()
    {
        base.init();
    }
    override public void update()
    {
        base.update();        
        CMouse.update();
        if (CMouse.firstPress())
        {
            CGame.inst().setImage("Sprites/level_Background");
            CGame.inst().setState(new CLevelState(1));
            return;
        }
        CSpriteManager.inst().update();
    }
    override public void render()
    {
        base.render();
        CSpriteManager.inst().render();
    }
    override public void destroy()
    {
        base.destroy();
        

    }
}

