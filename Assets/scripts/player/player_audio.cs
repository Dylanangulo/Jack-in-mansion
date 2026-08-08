using UnityEngine;

public class player_audio : MonoBehaviour
{

    [SerializeField] private AudioSource audiosource;
    [SerializeField] private AudioClip audioatk;
    [SerializeField] private AudioClip audiodead;
    [SerializeField] private AudioClip audiohurt;

    void SoundAtk()
    {
        audiosource.PlayOneShot(audioatk);
    }

    void SoundDead()
    {
        audiosource.PlayOneShot(audiodead);
    }

    void SoundHurt()
    {
        audiosource.PlayOneShot(audiohurt);
    }


}
