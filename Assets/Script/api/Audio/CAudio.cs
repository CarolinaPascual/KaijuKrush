using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class CAudio:CGameObject
    {
    private AudioSource mAudio;
    private GameObject mObject;

    public CAudio()
    {
        mObject = new GameObject();
        mAudio = mObject.AddComponent<AudioSource>();
        
    }
    public void setAudioSource(string aAudio)
    {
        
    }
}

