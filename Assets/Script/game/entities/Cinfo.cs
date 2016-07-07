using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    public class CInfo
    {    
        public int Kaiju;
        public int startStage;
        public int firstStage;
        public int secondStage;
        public float TargetScore;
        public int movements;
        public int dif;
        public int building;

    public CInfo(int aKaiju, int aStartStage,int aFirstStage, int aSecondStage, float aTargetScore, int aMovements,int aDif,int aBuilding)
    {
        Kaiju = aKaiju;
        startStage = aStartStage;
        firstStage = aFirstStage;
        secondStage = aSecondStage;
        TargetScore = aTargetScore;
        movements = aMovements;
        dif = aDif;
        building = aBuilding;
    }
    
}


