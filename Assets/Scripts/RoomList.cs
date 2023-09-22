using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class RoomList : MonoBehaviour
{
    // Dictionary<string, SessionInfo> roomListEntries = new Dictionary<string, SessionInfo>();

    public static RoomList Instance { get; private set; }
    [SerializeField] Transform content;

    [SerializeField] GameObject listItemPrefab;

    public event Action<SessionInfo> OnJoinSession;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {

    }

    internal void ClearList()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }

    internal void AddToList(SessionInfo sessionInfo)
    {
        ListItem item = Instantiate(listItemPrefab, content).GetComponent<ListItem>();

        item.SetItemName(sessionInfo);

        OnJoinSession += AddedSessionInfoListUIItem_OnJoinSession;
    }

    private void AddedSessionInfoListUIItem_OnJoinSession(SessionInfo sessionInfo)
    {
        NetworkManager.Instance.JoinGame(sessionInfo);
    }

    internal void OnJoinSessionTrigger(SessionInfo sessionInfo)
    {
        OnJoinSession?.Invoke(sessionInfo);
    }
}
