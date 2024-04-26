using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : Service
{
    [SerializeField] List<GameObject> WindowPrefabs;
    GameObject CurrentWindow;

    public void OpenWindow(string _windowName)
    {
        GameObject prefabQuery = GetWindowPrefab(_windowName);

        if(prefabQuery != null)
        {
            CurrentWindow = prefabQuery;
            //TEST
            prefabQuery.SetActive(true);
        }
    }

    public void CloseWindow(string _windowName)
    {
        GameObject prefabQuery = GetWindowPrefab(_windowName);

        if (prefabQuery != null)
        {
            //TEST
            prefabQuery.SetActive(false);
        }
    }

    public void CloseCurrentWindow()
    {
        CloseWindow(CurrentWindow.name);
    }

    private GameObject GetWindowPrefab(string _windowName)
    {
        GameObject result = null;

        foreach (GameObject windowPrefab in WindowPrefabs)
        {
            if (windowPrefab.name == _windowName)
            {
                result = windowPrefab;
                break;
            }
        }

        return result;
    }
}
