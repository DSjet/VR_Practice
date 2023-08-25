using UnityEngine;
using Photon;
using Photon.Realtime;
using System;

public class NetworkManager : Photon.PunBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ConnectToServer();
    }

    private void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings("Test");
        Debug.Log("Try to Connect...");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Server.");
        base.OnConnectedToMaster();
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 10;
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        PhotonNetwork.JoinOrCreateRoom("Room 1", roomOptions, TypedLobby.Default);
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
