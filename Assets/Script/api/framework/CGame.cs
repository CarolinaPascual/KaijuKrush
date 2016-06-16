using UnityEngine;
using System.Collections;

public class CGame : MonoBehaviour
{
    static private CGame mInstance;
    private CGameState mState;
    private CSprite imgBackground;

    
    void Awake()
    {
        if (mInstance != null)
        {
            throw new UnityException("Error in CGame(). You are not allowed to instantiate it more than once.");
        }

        mInstance = this;

        CMouse.init();
        CurrentStageData.init();
        CSpriteManager.init();
        imgBackground = new CSprite();
        setState(new CMenuState());
        //setImage("Sprites/Placeholders_Prototype/level_Background");
    }
    static public CGame inst()
    {
        return mInstance;
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        update();
    }

    void LateUpdate()
    {
        render();
    }

    private void update()
    {
        CMouse.update();
        imgBackground.update();
        mState.update();
    }

    private void render()
    {
        imgBackground.render();
        mState.render();
    }

    public void destroy()
    {
        CMouse.destroy();
        CurrentStageData.destroy();
        //CSpriteManager.destroy();
        if (mState != null)
        {
            mState.destroy();
            mState = null;
        }
        mInstance = null;
    }

    public void setState(CGameState aState)
    {
        if (mState != null)
        {
            mState.destroy();
            mState = null;
        }

        mState = aState;
        mState.init();
    }
    public void  setImage(string img)
    {
        
        imgBackground.setImage(Resources.Load<Sprite>(img));
        imgBackground.setX(351);
        imgBackground.setY(830);
    }

    public CGameState getState()
    {
        return mState;
    }
    public CSprite getBakcground()
    {
        return imgBackground;
    }

}
