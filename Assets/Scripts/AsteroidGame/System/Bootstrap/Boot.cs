using UnityEngine;
using UnityEngine.SceneManagement;

namespace AsteroidGame.System
{
    public class Boot : MonoBehaviour
    {
        private const string MAIN_SCENE = "MainScene";
        private void Awake()
        {
            LoadSceneAsync();
        }

        private async void LoadSceneAsync()
        {
            Debug.Log($"Start load Main scene");
            await SceneManager.LoadSceneAsync(MAIN_SCENE, LoadSceneMode.Single).AsTask();
            Debug.Log($"Main scene loaded");
        }
    }
}