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

            //CGame.inst().setState(new CLevelState(1));
            CInfo aux1 = new CInfo(1, 1, 50, 70, 300, 0, 0, 0);
            CGame.inst().setState(new CSurvivalState(aux1));
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

