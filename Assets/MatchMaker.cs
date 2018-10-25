using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class MatchMaker : MonoBehaviour {
    NetworkManager manager;
    public string roomName;

    List<GameObject> roomList = new List<GameObject>();

    [SerializeField]
    GameObject btn;
    [SerializeField]
    Transform parent;
    private void Start()
    {
        manager = NetworkManager.singleton; 
        if (manager.matchMaker==null)
        {
            manager.StartMatchMaker();
        }
         
    }
    public void SetRoomName(string  name)
    {
        roomName = name;
    }
    public void OnCreateRoomBtn()
    {
        manager.matchMaker.CreateMatch(roomName, 3, true, "", "", "", 0, 0, manager.OnMatchCreate);

    }

    public void OnRefreshBtn()
    {
        // 每次刷新都是 先删除集合中的元素 从新添加进去
        manager.matchMaker.ListMatches(0,10,"",true,0,0,OnmatchList);
    }

    private void OnmatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
    {
        if (!success)
        {
            Debug.Log("error"); return;
        }
        ClearList();

        foreach (var match in matches)
        {
            GameObject go = GameObject.Instantiate(btn, parent);
            roomList.Add(go);
            go.GetComponent<JionButton>().SetUp(match);

        }
    }

    public void ClearList()
    {

        for (int i = 0; i < roomList.Count; i++)
        {
           Destroy(roomList[i]);
        }
        roomList.Clear();
    }
}
