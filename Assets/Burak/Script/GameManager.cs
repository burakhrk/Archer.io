using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    CameraFovController cameraFovController;
    private void Awake()
    {
        cameraFovController = GetComponent<CameraFovController>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadLevel();
        }

    }
    void ReloadLevel()
    {
        SceneManager.LoadScene(0);
    }

    public void UpdatePlayerCount(int playerCount)
    {
        cameraFovController.UpdateFov(playerCount);
    }
}
