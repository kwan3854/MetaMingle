using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChnageScene_QuickPlay : MonoBehaviour
{
    // Start is called before the first frame update
    public void MoveToScene(string NeonScene00)
    {
        SceneManager.LoadScene(NeonScene00);
    }
}
