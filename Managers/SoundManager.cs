using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;

    public AudioSource[] audios;

    public float envorinmentMusicTime = 0f;
    float envorinmentMusicTimer = 10f;
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);

        envorinmentMusicTime = envorinmentMusicTimer;
        
    }
    private void Start()
    {
        //Music starts at 60 seconds and repeat every 50 seconds
        InvokeRepeating("envorinmentMusic",60f,50f);
    }

   
    

    //--------Movement Sounds--------//
    public void playFoodStep()
    {
        if (!audios[1].isPlaying)
        {
            //Random volume for realistic sound
            audios[1].volume = Random.Range(0.2f, 0.5f);
            //audios[1].pitch = Random.Range(0.8f, 1.2f);
            audios[1].Play();
        }       
    }
    public void walkSound()
    {
        audios[1].pitch = 1f;
    }
    public void runSound()
    {
        audios[1].pitch = 1.3f;
    }
    public void stopFoodStep()
    {
        audios[1].Stop();
    }


    //-----Weapon Sounds-----//
    public void fireSound()
    {        
       audios[2].Play();
    }

    public void reloadSound()
    {
        if (!audios[3].isPlaying)
            audios[3].Play();
    }


    //--Ambiance musics--//
    public void envorinmentMusic()
    {
        if(!audios[4].isPlaying && !audios[5].isPlaying)
        {
            audios[Random.Range(4, 7)].Play();                     
        }              
    }



    public void DetectionAlert()
    {
        if(!audios[7].isPlaying)
        {
            audios[7].Play();
        }
    }

}
