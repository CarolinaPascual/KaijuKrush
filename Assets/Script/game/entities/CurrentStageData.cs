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
}

