using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{

    public AudioSource efxSource;
    public AudioSource musicSource;
    public static SoundManager instance = null;
    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;

	void Awake ()
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

    public void PlaySingle (AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();
    }
    public void playLoop(AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.loop = true;
        efxSource.Play();
    }

    public void RandomizeSfx (params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        efxSource.pitch = randomPitch;
        efxSource.clip = clips[randomIndex];
        efxSource.Play();
    }

}
