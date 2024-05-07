using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public ServiceLocator Service;
    public List<MenuList> Menus;

    public bool HaveOpenMenu;

    public void OpenMenu(string _key)
    {
        MenuList menuQuery = GetMenuFromKey(_key);
        string value = menuQuery.value;

        if (menuQuery != null)
        {
            HaveOpenMenu = false;
            CloseMenu(_key);

            if (!menuQuery.opened)
            {
                HaveOpenMenu = true;
                Service.GetService<WindowManager>().OpenWindow(value);
            }

            menuQuery.opened = !menuQuery.opened;
        }
    }

    public void CloseMenu(string _key)
    {
        MenuList menuQuery = GetMenuFromKey(_key);
        string value = menuQuery.value;

        if (menuQuery != null)
        {
            HaveOpenMenu = false;
            Service.GetService<WindowManager>().CloseWindow(value);
        }
    }

    public void CloseCurrentMenu()
    {
        HaveOpenMenu = false;
        Service.GetService<WindowManager>().CloseCurrentWindow();
    }

    private MenuList GetMenuFromKey(string _key)
    {
        MenuList result = null;

        foreach(MenuList menu in Menus)
        {
            if(menu.key.Equals(_key))
            {
                result = menu;
                break;
            }
        }

        return result;
    }
}

[System.Serializable]
public class MenuList
{
    public string key;
    public string value;
    public bool opened;
}