using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class homeButton : MonoBehaviour
{
    public MainMenuUIManager mainMenu;
    
    public void openMainMenu()
    {
        //Disable Current Screen and Headers
        mainMenu.activeScreen.SetActive(false);
        gameObject.SetActive(false);

        //Open Main Screen and Headers
        mainMenu.gameObject.SetActive(true);
        mainMenu.mainScreenHeaders.SetActive(true);
    }    
}
