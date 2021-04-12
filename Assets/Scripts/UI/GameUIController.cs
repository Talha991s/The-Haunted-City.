using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private GameHUDWidget GameCanvas;
    [SerializeField] private GameHUDWidget PauseCanvas;
    [SerializeField] private GameHUDWidget InventoryCanvas;

    private GameHUDWidget ActiveMenu;

    // Start is called before the first frame update
    void Start()
    {
        DisableAllMenus();
        EnableGameMenu();
    }

    public void EnablePauseMenu()
    {
        if (ActiveMenu) ActiveMenu.DisableWidget();

        ActiveMenu = PauseCanvas;
        ActiveMenu.EnableWidget();
    }

    public void EnableGameMenu()
    {
        if (ActiveMenu) ActiveMenu.DisableWidget();

        ActiveMenu = GameCanvas;
        ActiveMenu.EnableWidget();
    }

    public void EnableInventoryMenu()
    {
        if (ActiveMenu) ActiveMenu.DisableWidget();

        ActiveMenu = InventoryCanvas;
        ActiveMenu.EnableWidget();
    }

    public void DisableAllMenus()
    {
        GameCanvas.DisableWidget();
        PauseCanvas.DisableWidget();
        InventoryCanvas.DisableWidget();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
