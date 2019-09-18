using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace PT
{
    public class Launcher : Photon.PunBehaviour
    {
        #region Public Variables

        public PhotonLogLevel Loglevel = PhotonLogLevel.Full;

        [Tooltip("The maximum number of players per room, When a room is full, it can't be joined by new players, and so new room will be created")]
        public byte MaxPlayersPerRoom = 4;

        [Tooltip("The UI Panel to let the user enter name, connect and play")]
        public GameObject controlPanel;

        [Tooltip("The UI Label to inform the user that the connection is in progress")]
        public GameObject progressLabel;

        #endregion

        #region Private Variables

        string _gameVersion = "1";
        bool isConnecting;

        #endregion

        #region MonoBehaviour CallBacks

        private void Awake()
        {
            //Screen.SetResolution(1980, 1080, true);

            PhotonNetwork.logLevel = Loglevel;

            PhotonNetwork.autoJoinLobby = false;

            PhotonNetwork.automaticallySyncScene = true;
        }

        private void Start()
        {
            progressLabel.SetActive(false);
            //controlPanel.SetActive(true);
        }

        #endregion

        #region Public Methods

        public void Connect()
        {
            isConnecting = true;
            progressLabel.SetActive(true);
            controlPanel.SetActive(false);

            if (PhotonNetwork.connected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                PhotonNetwork.ConnectUsingSettings(_gameVersion);
            }

            //SceneManager.LoadScene("Auction");
        }

        #endregion

        #region Photon.PunBehaviour CallBacks

        public override void OnConnectedToMaster()
        {
            //base.OnConnectedToMaster(); 
            Debug.Log("TerraFormmingMars/Launcher : OnConnectedToMaster() was called by PUN");
            if (isConnecting)
                PhotonNetwork.JoinRandomRoom();
        }

        public override void OnDisconnectedFromPhoton()
        {
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
            //base.OnDisconnectedFromPhoton();
            Debug.LogWarning("TerraFormmingMars/Laucher : OnDisconnectedFromPhoton() was called by PUN");
        }

        public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
        {
            //base.OnPhotonRandomJoinFailed(codeAndMsg);

            Debug.Log("TerraFormmingMars/Launcher : OnPhotonRandomJoinFailed() was called by PUN. No random room available, so we create one.\n Calling : PhotonNetwork.CreateRoom(null, new RoomOptions(){maxPlayers = MaxPlayerPerRoom}, null;");

            PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = MaxPlayersPerRoom }, null);
        }

        public override void OnJoinedRoom()
        {
            // base.OnJoinedRoom();

            if (PhotonNetwork.room.PlayerCount == 1)
            {
                Debug.Log("We load the 'Lobby' ");

                PhotonNetwork.LoadLevel("Lobby");
            }

            Debug.Log("TerraFormmingMars/Launcher : OnJoinedRoom() called by PUN, Now this client is in a room");
        }

        #endregion
    }
}