using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class NetWorkUI : NetworkBehaviour {
    /// <summary>
    /// 连接主机
    /// </summary>
    public void StartHost()
    {
        NetworkManager.singleton.StartHost();  //192.168.2.102
        
    }
    public void StartClinet()
    {
        NetworkManager.singleton.networkAddress = GameObject.Find("ip").GetComponent<InputField>().text;

        NetworkManager.singleton.StartClient();
      
    }
    public void StopHost()
    {
        NetworkManager.singleton.StopHost();
    } 
    public void OfflineSet()
    {
        GameObject.Find("Host").GetComponent<Button>().onClick.AddListener(StartHost);
        GameObject.Find("Client").GetComponent<Button>().onClick.AddListener(StartClinet);
    }
    
    public void OnLineSet()
    {
        GameObject.Find("Return").GetComponent<Button>().onClick.AddListener(StartClinet);

    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode arg1)
    {
        switch (scene.buildIndex)
        {
            case 0:
                OfflineSet();
                break;
            case 1:
                OnLineSet();
                break; 
            default:
                break;
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
