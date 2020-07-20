using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    // TODO: 'exit game' on in game menu should clear trees and go back to main menu, not quit application
    public void QuitGame(){
        Application.Quit();
    }
}
