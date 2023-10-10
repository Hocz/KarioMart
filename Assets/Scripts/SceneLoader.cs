using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public static void LoadScene(Scenes scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
}

[Serializable]
public enum Scenes
{
    MainMenu, TrackLevel_1, TrackLevel_2, TrackLevel_3, TrackLevel_4
}
