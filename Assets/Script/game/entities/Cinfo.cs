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
        public int TargetScore;
        public int movements;

    public CInfo(int aKaiju, int aStartStage,int aFirstStage, int aSecondStage, int aTargetScore, int aMovements)
    {
        Kaiju = aKaiju;
        startStage = aStartStage;
        firstStage = aFirstStage;
        secondStage = aSecondStage;
        TargetScore = aTargetScore;
        movements = aMovements;
    }
    
}


