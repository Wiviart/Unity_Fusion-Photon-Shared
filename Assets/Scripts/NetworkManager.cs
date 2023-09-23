using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class NetworkManager : MonoBehaviour, INetworkRunnerCallbacks
{
    public void OnConnectedToServer(NetworkRunner runner)
    {

    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {

    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {

    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {

    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {

    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {

    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {

    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {

    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (runner.LocalPlayer != player) return;
        // if(!runner.IsServer) return;

        Vector3 position = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));

        NetworkPlayer nPlayer = runner.Spawn(playerPrefab, position, Quaternion.identity, player);
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
       
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {

    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {

    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {

    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        if (RoomList.Instance == null) return;

        RoomList.Instance.ClearList();

        foreach (SessionInfo sessionInfo in sessionList)
        {
            RoomList.Instance.AddToList(sessionInfo);
        }
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {

    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {

    }


    public static NetworkManager Instance { get; private set; }
    NetworkRunner runner;
    [SerializeField] NetworkPlayer playerPrefab;
    
    void Awake()
    {
        Instance = this;
        runner = GetComponent<NetworkRunner>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    internal void StartGame(string sessionName)
    {
        var clientTask = InitializeNetworkRunner(runner, GameMode.Shared, sessionName);
    }

    internal void JoinGame(SessionInfo sessionInfo)
    {
        var clientTask = InitializeNetworkRunner(runner, GameMode.Shared, sessionInfo.Name);
    }

    protected virtual Task InitializeNetworkRunner(NetworkRunner runner, GameMode gameMode, string sessionName)
    {
        return runner.StartGame(new StartGameArgs
        {
            GameMode = gameMode,
            SessionName = sessionName,
            CustomLobbyName = "OurLobbyID",
        });
    }

    internal void OnJoinLobby()
    {
        var clientTask = JoinLobby();
    }

    private async Task JoinLobby()
    {
        string lobbyID = "OurLobbyID";

        var result = await runner.JoinSessionLobby(SessionLobby.Custom, lobbyID);
    }

    internal string GetPlayerCount()
    {
        return runner.ActivePlayers.Count().ToString();
    }
}
