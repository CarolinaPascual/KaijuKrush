using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Timer
    {
        public float maxTime = 60;
        public float timeLeft { get; set; }
        public float cumulativeTime { get; set; }

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
        
    }

    public float getTimeLeft()
    {
        return timeLeft;
    }
    public void addTime(float aScore)
    {
        timeLeft += (aScore / 5);
        if(timeLeft> maxTime)
        {
            timeLeft = maxTime;
        }
    }
}




