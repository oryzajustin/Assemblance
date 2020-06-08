using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

// Handles matchmaking in the main menu.
// It's a very rudimentary system, but works for what we're doing for now.
public class MainMenu : MonoBehaviourPunCallbacks
{
    [Header("Networking Panels")]
    [SerializeField] GameObject find_players_panel;
    [SerializeField] GameObject waiting_panel;
    [SerializeField] TextMeshProUGUI waiting_status_text;
    [SerializeField] Button find_players_button;
    [SerializeField] Button start_button;

    private bool is_connecting = false;

    private const string game_version = "0.1";
    private const int max_players = 2;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start()
    {
        find_players_button.onClick.AddListener(delegate { FindPlayers(); });
        start_button.onClick.AddListener(delegate { StartMatch(); });
        start_button.gameObject.SetActive(false);
    }

    #region Networking
    public void FindPlayers()
    {
        is_connecting = true;

        find_players_panel.SetActive(false);
        waiting_panel.SetActive(true);

        waiting_status_text.text = "Searching for players...";

        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.GameVersion = game_version;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        if (is_connecting)
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        waiting_panel.SetActive(false);
        find_players_panel.SetActive(true);
        Debug.Log($"Disconnected due to: {cause}");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No clients are waiting for players; creating new room");

        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = max_players });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Client successfully connected to room");

        Debug.Log(PhotonNetwork.IsMasterClient);
        int player_count = PhotonNetwork.CurrentRoom.PlayerCount;

        Debug.Log(player_count);

        if(player_count < 2)
        {
            waiting_status_text.text = "Waiting for other players...";
            start_button.interactable = false;
            Debug.Log("Client waiting for players");
        }
        else
        {
            if(player_count != max_players)
            {
                waiting_status_text.text = player_count + " Players found! ";
                Debug.Log("More players can join; Match is ready");
            }
            else
            {
                waiting_status_text.text = "Found partner!";
                Debug.Log("Lobby is full! Match is ready");
            }
        }
    }

    public override void OnLeftRoom()
    {
        waiting_status_text.text = PhotonNetwork.CurrentRoom.PlayerCount + " Players found!";
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount == max_players)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false; // Don't let any other players join and auto start

            Debug.Log("Match is ready and full");
            waiting_status_text.text = "Found partner!";

            StartMatch(); // Start the game
        }
        else
        {
            Debug.Log("Match is ready; but not full");
            waiting_status_text.text = PhotonNetwork.CurrentRoom.PlayerCount + " Players found!";
            if (PhotonNetwork.IsMasterClient)
            {
                start_button.gameObject.SetActive(true);
                start_button.interactable = true;
            }
        }
    }

    public void StartMatch()
    {
        Debug.Log("Starting Match");
        PhotonNetwork.LoadLevel("SampleScene");
    }

    #endregion

    public void ExitGame()
    {
        Application.Quit();
    }
}