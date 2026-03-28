using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : View
{
    [SerializeField] private CaughtProbabilityUI m_CaughtProbilityUI;
    [SerializeField] private SceneMapUI m_SceneMapUI;

    public CaughtProbabilityUI CaughtProbilityUI => m_CaughtProbilityUI;
    public SceneMapUI SceneMapUI => m_SceneMapUI;

    
}
