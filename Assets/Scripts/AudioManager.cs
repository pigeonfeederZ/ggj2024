using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // 单例
    public static AudioManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    public AudioSource audioSource;
    public List<AudioClip> audioList = new List<AudioClip>();

    public void PlayMusic(int index)
    {
        if (index >= 0 && index < audioList.Count)
        {
            audioSource.clip = audioList[index];
            audioSource.Play();
            Debug.LogError("Playing");
        }
        else
        {
            Debug.LogError("Index out of range");
        }
    }

}
