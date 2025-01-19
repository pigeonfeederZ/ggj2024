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
    public AudioSource audioSource2;
    public List<AudioClip> voiceList = new List<AudioClip>();

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

    public void PlayVoice(int index)
    {
        if (audioSource2.isPlaying)
        {
            // 如果正在播放，则克隆一个新的AudioSource来播放音效
            AudioSource newSource = gameObject.AddComponent<AudioSource>();
            newSource.clip = voiceList[index];
            newSource.Play();
        }
        else
        {
            // 如果没有播放，直接使用现有的AudioSource播放音效
            audioSource2.clip = voiceList[index];
            audioSource2.Play();
        }
    }

}
