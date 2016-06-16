using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    class CMenuState:CGameState
    {
    public CMenuState()
    {
        CGame.inst().setImage("Sprites/Placeholders_Prototype/Main-Menu-PH");
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
            CGame.inst().setImage("Sprites/Placeholders_Prototype/level_Background");
            CGame.inst().setState(new CLevelState());
            
        }
    }
    override public void render()
    {
        base.render();
        
    }
    override public void destroy()
    {
        base.destroy();
      

    }
}

