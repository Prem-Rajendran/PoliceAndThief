using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMapUIHandler : MonoBehaviour
{
    private void Start()
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.GetView<HUD>().SceneMapUI.gameObject.SetActive(true);
        }

    }


    private void OnDisable()
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.GetView<HUD>().SceneMapUI.gameObject.SetActive(false);
        }
    }
}
