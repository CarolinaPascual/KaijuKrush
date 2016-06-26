using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class LevelsInfo
    {
    static private bool mInitialized = false;
    static private List<CInfo> Levels;
    public LevelsInfo()
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
        loadData();
    }

    public static void update() { }

    public static void destroy()
    {
        if (mInitialized)
        {
            mInitialized = false;
        }
    }

    public static CInfo getLevel(int aLevel)
    {
        return Levels[aLevel - 1];
    }
       
    public static int getLevelsAmount()
    {
        return Levels.Count();
    }

    private static void loadData()
    {
        // Kaiju type 1 Dinosaur, type 2 kong, type 3 Kraken
        Levels = new List<CInfo>();
        //Kaiju type,startStage,1st breakpoint,2nd breakpoint,targetScore,Movements,Difficulty
        Levels.Add(new CInfo(1, 1, 65, 200, 110, 15,0));
        Levels.Add(new CInfo(1, 2, 0, 70, 110, 15, 1));
        Levels.Add(new CInfo(1, 3, 0, 0, 120, 15, 2));

        Levels.Add(new CInfo(2, 1, 65, 200, 130, 15,2));
        Levels.Add(new CInfo(2, 2, 0, 70, 140, 15, 2));
        Levels.Add(new CInfo(2, 3, 0, 0, 150, 15, 3));

    }

  
}

