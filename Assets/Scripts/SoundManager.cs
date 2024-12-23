using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource source;
    
    // public static AudioManager instance;

    // public AudioSource[] soundEffects;

    // public AudioSource bgm, levelEndMusic;

    private void Awake()
    {

        source = GetComponent<AudioSource>();

        //Keep this object even when we go to new scene
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //Destroy duplicate gameobjects
        else if (instance != null && instance != this)
            Destroy(gameObject);
    }
    public void PlaySound(AudioClip _sound)
    {
        source.PlayOneShot(_sound);
    }
    // public void PlaySFX(int soundToPlay){
    //     soundEffects[soundToPlay].Stop();

    //     soundEffects[soundToPlay].pitch = Random.Range(.9f, 1.1f);

    //     soundEffects[soundToPlay].Play();
    // }
}