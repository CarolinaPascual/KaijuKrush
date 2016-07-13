using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Timer
    {
        public float maxTime = 15;
        public float timeLeft { get; set; }
        public float cumulativeTime { get; set; }
        public float difficultyTime { get; set; }

    public Timer()
    {
        timeLeft = maxTime;
        cumulativeTime = 0;
    }

    public void update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            timeLeft = 0;
        }
        cumulativeTime += Time.deltaTime;
        difficultyTime += Time.deltaTime;
        if (difficultyTime >= 15& CurrentStageData.difficulty<=5)
        {
            difficultyTime = 0;
            CurrentStageData.difficulty += 1;
        }
        
    }

    public float getTimeLeft()
    {
        return timeLeft;
    }
    public void addTime(float aScore)
    {
        timeLeft += (aScore / 6);
        if(timeLeft> maxTime)
        {
            timeLeft = maxTime;
        }
        if (timeLeft < 0)
        {
            timeLeft = 0;
        }
    }
}




