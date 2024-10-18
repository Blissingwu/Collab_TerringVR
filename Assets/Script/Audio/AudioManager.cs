using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum BGMTag
{
    BGM_Main
}

public enum SFXTag
{
    SFX_Hit
}

[System.Serializable]
public class AudioClipEntry
{
    public BGMTag bgmTag;
    public AudioClip bgmClip;
}

[System.Serializable]
public class SFXClipEntry
{
    public SFXTag sfxTag;
    public AudioClip sfxClip;
}

public class AudioManager : MonoBehaviour
{
    public List<AudioClipEntry> bgmClips;
    public List<SFXClipEntry> sfxClips;

    private Dictionary<BGMTag, AudioClip> bgmDictionary;
    private Dictionary<SFXTag, AudioClip> sfxDictionary;

    private AudioSource bgmSource;

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        bgmSource = this.GetComponent<AudioSource>();

        bgmDictionary = new Dictionary<BGMTag, AudioClip>();
        sfxDictionary = new Dictionary<SFXTag, AudioClip>();

        foreach (var entry in bgmClips)
        {
            bgmDictionary[entry.bgmTag] = entry.bgmClip;
        }

        foreach (var entry in sfxClips)
        {
            sfxDictionary[entry.sfxTag] = entry.sfxClip;
        }

        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.loop = true;

        PlayBGM(BGMTag.BGM_Main);
    }

    public void PlayBGM(BGMTag bgmTag)
    {
        if (bgmDictionary.TryGetValue(bgmTag, out AudioClip clip))
        {
            if (bgmSource.clip != clip)
            {
                bgmSource.clip = clip;
                bgmSource.Play();
            }
        }
    }

    public void PlaySFX(SFXTag _sfxTag, AudioSource _source)
    {
        if (sfxDictionary.TryGetValue(_sfxTag, out AudioClip clip))
        {
            _source.PlayOneShot(clip);
        }
    }
}
