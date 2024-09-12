using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameSceneManager
{
    // 场景枚举
    public enum Scene
    {
        GameScene,
        MainMenuScene,
    }

    // 加载场景的静态方法
    public static void Load(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
}

