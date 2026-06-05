using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio/Audio Database")]
public class AudioDatabase : ScriptableObject
{
    public List<AudioEntry> music;
    public List<AudioEntry> sfx;

    public AudioClip GetMusic(string id)
    {
        return music.Find(x => x.id == id)?.clip;
    }

    public AudioClip GetSFX(string id)
    {
        return sfx.Find(x => x.id == id)?.clip;
    }
}

[Serializable]
public class AudioEntry
{
    public string id;
    public AudioClip clip;
}