using UnityEngine;

public class HelpMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject menu;

    [SerializeField]
    private GameObject minimisedIcon;
    
    bool menuMaximised = true;
    public void toggleMenu() 
    {
        if (menuMaximised)
        {
            menuMaximised = false;
            menu.SetActive(false);
            minimisedIcon.SetActive(true);
        }
        else 
        {
            menuMaximised = true;
            menu.SetActive(true);
            minimisedIcon.SetActive(false);
        }
    }

    public void closeGame()
    {
        Application.Quit();
    }
}
