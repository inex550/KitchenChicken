using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader {

    public enum GameScene {
        LoadingScene,
        MainMenuScene,
        GameScene,
    }

    private static float unitySceneLoadingProgressMax = 0.9f;

    private static GameScene targetScene;

    public static void Load(GameScene targetScene) {
        SceneLoader.targetScene = targetScene;
        SceneManager.LoadScene(GameScene.LoadingScene.ToString());
    }

    public static IEnumerator TargetSceneLoadingCoroutine(ISceneLoadingProgressHandler progressHandler) {
        AsyncOperation sceneLoadingOperation = SceneManager.LoadSceneAsync(targetScene.ToString());

        while (!sceneLoadingOperation.isDone) {
            float progress = Mathf.Clamp01(sceneLoadingOperation.progress / unitySceneLoadingProgressMax);
            progressHandler.HandleProgress(progress);
            
            yield return null;
        }
    }
}