using UnityEngine;
using System.Collections;

public class CGame : MonoBehaviour
{
    static private CGame mInstance;
    private CGameState mState;
    private CSprite imgBackground;
    private CSpriteManager mSpriteManager;
    

    void Awake()
    {
        if (mInstance != null)
        {
            throw new UnityException("Error in CGame(). You are not allowed to instantiate it more than once.");
        }
        
        //CGameConstants.HIGH_SCORE =int.Parse( System.IO.File.ReadAllText("score.txt"));
        
        
        mInstance = this;
        Resolution res;

     
            res = Screen.currentResolution;
            if (res.refreshRate == 60)
                QualitySettings.vSyncCount = 1;
            if (res.refreshRate == 120)
                QualitySettings.vSyncCount = 2;
           
        

        CMouse.init();
        CurrentStageData.init();        
        LevelsInfo.init();
        imgBackground = new CSprite();
        
        //Handheld.PlayFullScreenMovie("IntroGame2.mp4", Color.black, FullScreenMovieControlMode.CancelOnInput);
        setState(new CMenuState());
        
        mSpriteManager = new CSpriteManager();
        
        
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
        mSpriteManager.update();
    }

    private void render()
    {
        imgBackground.render();
        mState.render();
        mSpriteManager.render();
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
