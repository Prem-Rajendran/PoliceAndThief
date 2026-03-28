using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaughtProbabilityUIHandler : MonoBehaviour
{
    [SerializeField] private Transform m_Follower;
    [SerializeField] private Transform m_Target;

    private void Start()
    {
        if(UIManager.Instance!=null)
        {
            UIManager.Instance.GetView<HUD>().CaughtProbilityUI.SetupCaughtProbabilityUI(m_Follower, m_Target);
        }
        
    }

    private void OnDisable()
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.GetView<HUD>().CaughtProbilityUI.gameObject.SetActive(false);
        }
    }
}
