using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public struct MenuList 
    {
        public string key;
        public string value; 
    }

    public ServiceLocator Service;
    public List<MenuList> Menus;

    public void OpenMenu(string _key)
    {
        string nameQuery = GetValueFromKey(_key);
        Service.GetService<WindowManager>().OpenWindow(nameQuery);
    }

    private string GetValueFromKey(string _key)
    {
        string result = null;

        foreach(MenuList menu in Menus)
        {
            if(menu.key.Equals(_key))
            {
                result = menu.value;
                break;
            }
        }

        return result;
    }
}
