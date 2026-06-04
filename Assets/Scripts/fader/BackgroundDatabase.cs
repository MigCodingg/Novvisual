using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Backgrounds/Background Database")]
public class BackgroundDatabase : ScriptableObject
{
    public List<BackgroundEntry> backgrounds;

    public Sprite Get(string id)
    {
        return backgrounds.Find(b => b.id == id)?.sprite;
    }
}

[Serializable]
public class BackgroundEntry
{
    public string id;
    public Sprite sprite;
}