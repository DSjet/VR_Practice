using UnityEngine;
using Photon;
using Photon.Realtime;
using System;
using System.Collections.Generic;

[System.Serializable]
public class DefaultRoom
{
    public string roomName;
    public int sceneIndex;
    public int maxPlayer;
}

public class NetworkManager : Photon.PunBehaviour
{
    [SerializeField] List<DefaultRoom> defaultRooms; 
    [SerializeField] GameObject roomUI;

    public void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings("Test");
        Debug.Log("Try to Connect...");
    }


    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Server.");
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("We Joined the Lobby");
        roomUI.SetActive(true);
    }

    public void InitializeRoom(int defaultRoomIndex)
    {
        DefaultRoom roomSettings = defaultRooms[defaultRoomIndex];

        // Load Scene
        PhotonNetwork.LoadLevel(roomSettings.sceneIndex); 

        // Create a Room
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = (byte)roomSettings.maxPlayer;
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        PhotonNetwork.JoinOrCreateRoom(roomSettings.roomName, roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a Room");
        base.OnJoinedRoom();
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        Debug.Log("A new player joined the room");
        base.OnPhotonPlayerConnected(newPlayer);
    }

}
