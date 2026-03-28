using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScreen : View
{
   public void OnLevelSelectionButtonClicked(string sceneName)
   {
        SequenceManager.Instance.LoadGameScene(sceneName);
   }
}
