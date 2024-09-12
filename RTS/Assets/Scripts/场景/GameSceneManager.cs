using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameSceneManager
{
    // ����ö��
    public enum Scene
    {
        GameScene,
        MainMenuScene,
    }

    // ���س����ľ�̬����
    public static void Load(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
}

