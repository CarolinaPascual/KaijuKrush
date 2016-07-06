using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Timer
    {
        public float maxTime = 60;
        public float timeLeft { get; set; }

    public Timer()
    {
        timeLeft = maxTime;
    }

    public void update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            timeLeft = 0;
        }
        
    }

    public float getTimeLeft()
    {
        return timeLeft;
    }

}




