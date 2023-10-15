using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

public class PauseInGame : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuManager;
    private Transform avatar;

    private void Start()
    {
        // find avatar with tag (try catch)
        try
        {
            avatar = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        }
        catch (System.Exception)
        {
            Debug.LogError("Avatar not found");
            Assert.raiseExceptions = true;
        }
    }

    public void PauseToggle()
    {
        if (IsPauseMenuManagerActive())
        {
            Recover();
        }
        else
        {
            Pause();
        }
    }

    private bool IsPauseMenuManagerActive()
    {
        if (pauseMenuManager.activeSelf)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsSettingsMenuActive()
    {
  /*      if ()
        {
            return true;
        }
        else
        {
            return false;
        }*/
        return false;
    }

    private void Pause()
    {
        // disable player input
        avatar.GetComponent<PlayerInput>().enabled = false;

        /*            // disable main cam, enable dialog cam
                    mainCamera.SetActive(false);
                    toActivate.SetActive(true);*/

        // d©¥splay cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        //Time.timeScale = 0;
    }

    private void Recover()
    {
        avatar.GetComponent<PlayerInput>().enabled = true;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //Time.timeScale = 1;
    }
}
