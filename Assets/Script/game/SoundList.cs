using UnityEngine;
using System.Collections;

public class SoundList : MonoBehaviour
{

    
    public static SoundList instance = null;
    public AudioClip levelMusic;
    public AudioClip menuMusic;
    public AudioClip comer;
    public AudioClip destruccion1;
    public AudioClip lose1;
    public AudioClip lose2;
    public AudioClip lose3;
    public AudioSource musicSource;
    public AudioClip kaijuSelection;
    public AudioClip dino;
    public AudioClip kraken1;
    public AudioClip kraken2;
    public AudioClip kong;
    public AudioClip match;
    public AudioClip match2;
    public AudioClip match3;
    public AudioClip match4;
    public AudioClip newGame;
    public AudioClip bomb;
    public AudioClip newGame2;
    
    void Awake()
    {
        if (instance == null)
            //Si no es null, la setea a esta.
            
            instance = this;
            
        //Si la instancia ya existe
        else if (instance != this)
            //Si no es esta la destruye para que esta sea la unica instancia de SoundManager.
            Destroy(gameObject);

        //Se setea SoundManager como "DontDestroyOnLoad" para que la musica se reproduzca de corrido durante intercambio de level.
        DontDestroyOnLoad(gameObject);
    }

    public void playLevelMusic()
    {
        //AudioSource.PlayClipAtPoint(levelMusic, Camera.main.transform.position);
        musicSource.clip = levelMusic;
        musicSource.loop = true;
        musicSource.Play();

    }
    public void playMenuMusic()
    {
        //AudioSource.PlayClipAtPoint(levelMusic, Camera.main.transform.position);
        musicSource.clip = menuMusic;
        musicSource.loop = true;
        musicSource.Play();

    }
    public void playComer()
    {
        AudioSource.PlayClipAtPoint(comer, Camera.main.transform.position);
    }
    public void playBomb()
    {
        AudioSource.PlayClipAtPoint(bomb, Camera.main.transform.position);
    }
   public void playNewGame2()
    {
        AudioSource.PlayClipAtPoint(newGame2, Camera.main.transform.position);
    }
    public void playDestruccion1()
    {
        musicSource.clip = destruccion1;
        musicSource.loop = false;
        musicSource.Play();
    }
    public void stopMusic()
    {
        musicSource.Stop();
    }
    public void playSelection()
    {
        musicSource.clip = kaijuSelection;
        musicSource.loop = false;
        musicSource.Play();
    }
    public void playLose()
    {
        int randomIndex = Random.Range(1, 3);
        switch (randomIndex)
        {
            case 1:
                musicSource.clip = lose1;
                break;
            case 2:
                musicSource.clip = lose2;
                break;
            case 3:
                musicSource.clip = lose3;
                break;
        }
        musicSource.loop = false;
        musicSource.Play();


    }
    public void playDino()
    {
        AudioSource.PlayClipAtPoint(dino, Camera.main.transform.position);
    }
    public void playKraken()
    {
        //int randomIndex = Random.Range(1, 2);
        //switch (randomIndex)
        //{
            //case 1:
              //  AudioSource.PlayClipAtPoint(kraken1, Camera.main.transform.position);
                //break;
            //case 2:
                AudioSource.PlayClipAtPoint(kraken2, Camera.main.transform.position);
              //  break;
        //}
    }
    public void playKong()
    {
        AudioSource.PlayClipAtPoint(kong, Camera.main.transform.position);
    }
    public void playMatch()
    {
        int randomIndex = Random.Range(1, 4);
        switch (randomIndex)
        {
            case 1:
                AudioSource.PlayClipAtPoint(match, Camera.main.transform.position);
                break;
            case 2:
                AudioSource.PlayClipAtPoint(match2, Camera.main.transform.position);
                break;
            case 3:
                AudioSource.PlayClipAtPoint(match3, Camera.main.transform.position);
                break;
            case 4:
                AudioSource.PlayClipAtPoint(match4, Camera.main.transform.position);
                break;
        }
        
    }
    public void playNewGame()
    {
        AudioSource.PlayClipAtPoint(newGame, Camera.main.transform.position);
    }
    
}
