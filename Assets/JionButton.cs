using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class JionButton : MonoBehaviour {
    NetworkManager manager;

    public MatchInfoSnapshot info;
    public Text nameText;
    private void Start()
    {
        manager = NetworkManager.singleton;
        if (manager.matchMaker == null)
        {
            manager.StartMatchMaker();
        } 
    } 
     public void SetUp(MatchInfoSnapshot _info )
    {
        info = _info;
        nameText.text = info.name;
    }

    public void OnJionBtn()
    {
        manager.matchMaker.JoinMatch(info.networkId, "", "", "", 0, 0, manager.OnMatchJoined);
    }
}
