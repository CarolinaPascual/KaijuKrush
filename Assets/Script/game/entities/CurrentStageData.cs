using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
//test
class CurrentStageData
    {
    
    private static bool mInitialized = false;
    public static float score { get; set; }    
    public static Kaiju currentKaiju { get; set; }
    public static  Board currentBoard { get; set; }
    public static float growScoreRelation { get; set; }
    private static int shakeAux;
    public static int difficulty { get; set; }

    public CurrentStageData()
    {
        throw new UnityException("Error in CMouse(). You're not supposed to instantiate this class.");
        

    }
    
   
    public static void init()
    {
        if (mInitialized)
        {
            return;
        }
        mInitialized = true;
        shakeAux = 1;
    }
    public static void assignData(Kaiju aKaiju, Board aBoard,float aNumber,int aDif)
    {
        currentBoard = aBoard;
        currentKaiju = aKaiju;
        growScoreRelation = aNumber;
        difficulty = aDif;
       
    }

    public static void clearData()
    {
        score = 0;
        currentKaiju = null;
        currentBoard = null;
        difficulty = 0;
    }

    public void update()
    {        
    }
    public void render()
    {       

    }
    
    public static void addScore(float aScore) {
        score += aScore;
        currentKaiju.addGrow(aScore * growScoreRelation);
    }

    public static void cameraShake()
    {
        if (shakeAux == 1)
        {            
            Camera.main.transform.Translate(new Vector3(15, 0, 0));
            if(Camera.main.transform.position.x == 375) { 
            shakeAux *= -1;
            }
        } else
        {            
            Camera.main.transform.Translate(new Vector3(-15, 0, 0));
            if (Camera.main.transform.position.x == 345)
            { 
                shakeAux *= -1;
            }
           
            
        }
        
    }

    public static void destroy()
    {
        if (mInitialized)
        {
            mInitialized = false;
        }
    }



}

