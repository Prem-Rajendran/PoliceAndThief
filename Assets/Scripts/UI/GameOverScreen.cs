using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : View
{
    public void OnMainMenuClicked()
    {
        SequenceManager.Instance.GoToMainMenu();
    }
}
