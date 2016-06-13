using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class CurrentStageData
    {
    private static CurrentStageData mInst = null;

    public float score { get; set; }    
    public Kaiju currentKaiju { get; set; }
    public Board currentBoard { get; set; }
    public float growScoreRelation { get; set; }
    private int shakeAux = 1;
    

    public CurrentStageData()
    {
        registerSingleton();

    }
    public static CurrentStageData inst()
    {
        return mInst;
    }
    private void registerSingleton()
    {
        if (mInst == null)
        {
            mInst = this;
        }
        else
        {
            throw new UnityException("Error: You cannot create another instance of singleton class " + this + " ");
        }

    }
    public void assignData(Kaiju aKaiju, Board aBoard,float aNumber)
    {
        currentBoard = aBoard;
        currentKaiju = aKaiju;
        growScoreRelation = aNumber;
    }

    public void clearData()
    {
        score = 0;
        currentKaiju = null;
        currentBoard = null;
    }

    public void update()
    {        
    }
    public void render()
    {       

    }
    public  void destroy()
    {
        
        mInst = null;
    }
    public void addScore(float aScore) {
        score += aScore;
        currentKaiju.addGrow(aScore * growScoreRelation);
    }

    public void cameraShake()
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
   
    

 

}

