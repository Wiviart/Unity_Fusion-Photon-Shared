using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ExitGames.Client.Photon.StructWrapping;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GUI_Menu : MonoBehaviour
{
    [Header("Main Menu")]
    [SerializeField] Button playButton;
    [SerializeField] Button findButton;
    [SerializeField] GameObject sessionList;
    [SerializeField] TMP_InputField sessionName;

    [Header("Gameplay")]
    [SerializeField] GameObject gameplayGUI;
    [SerializeField] TextMeshProUGUI playerCountText;

    void Awake()
    {
        playButton.onClick.AddListener(PlayButtonClicked);
        findButton.onClick.AddListener(FindButtonClicked);
    }

    void Start()
    {
        sessionList.SetActive(false);
        gameplayGUI.SetActive(false);

        RoomList.Instance.OnJoinSession += CloseMainMenuGUI;
        RoomList.Instance.OnJoinSession += ShowGameplayGUI;
    }

    void Update()
    {
        ShowPlayerCount();
    }

    private void FindButtonClicked()
    {
        sessionList.SetActive(true);

        NetworkManager.Instance.OnJoinLobby();
    }

    private void PlayButtonClicked()
    {
        NetworkManager.Instance.StartGame(sessionName.text);

        CloseMainMenuGUI(null);
        ShowGameplayGUI(null);
    }

    void CloseMainMenuGUI(SessionInfo sessionInfo)
    {
        sessionList.SetActive(false);
        playButton.gameObject.SetActive(false);
        findButton.gameObject.SetActive(false);
        sessionName.gameObject.SetActive(false);
    }

    void ShowGameplayGUI(SessionInfo sessionInfo)
    {
        gameplayGUI.SetActive(true);
    }

    internal void ShowPlayerCount()
    {
        if (!gameplayGUI.activeSelf) return;

        playerCountText.text = "Players: " + NetworkManager.Instance.GetPlayerCount();
    }
}
