using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI playerCountText;
    [SerializeField] Button joinButton;
    SessionInfo sessionInfo;

    void Awake()
    {
        joinButton.onClick.AddListener(JoinRoom);
    }

    private void JoinRoom()
    {
        RoomList.Instance.OnJoinSessionTrigger(sessionInfo);
    }

    public void SetItemName(SessionInfo sessionInfo)
    {
        this.sessionInfo = sessionInfo;
        itemName.text = sessionInfo.Name;
        playerCountText.text = $"{sessionInfo.PlayerCount.ToString()}/{sessionInfo.MaxPlayers.ToString()}";
    }
}
