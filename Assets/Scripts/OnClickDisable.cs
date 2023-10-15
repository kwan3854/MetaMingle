using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnClickDisable : MonoBehaviour
{
    public GameObject gameObject;
    public void OnClick()
    {
        gameObject.SetActive(false);
    }
}
