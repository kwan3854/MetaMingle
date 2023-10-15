using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using Michsky.MUIP;

public class StoryManager : MonoBehaviour
{
    [SerializeField] private ModalWindowManager modalWindow; // assign your modal window in Inspector
    [SerializeField] private Transform avatar;

    void Start()
    {
        try
        {
            avatar = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        }
        catch (System.Exception)
        {
            Debug.LogError("Avatar not found");
            Assert.raiseExceptions = true;
        }

        StartCoroutine(ShowModalWindowAfterDelay(5));
    }

    IEnumerator ShowModalWindowAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        modalWindow.Open(); // This will enable the modal window
    }

    public void Pause()
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

    public void Recover()
    {
        avatar.GetComponent<PlayerInput>().enabled = true;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //Time.timeScale = 1;
    }
}
