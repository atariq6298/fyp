using System;
using System.ComponentModel;

#if ENABLE_UNET

namespace UnityEngine.Networking
{
	[AddComponentMenu("Network/NetworkManagerHUD")]
	[RequireComponent(typeof(NetworkManager))]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class NtManagerHUD : MonoBehaviour
	{
		public NetworkManager manager;
		[SerializeField] public bool showGUI = true;
		[SerializeField] public int offsetX;
		[SerializeField] public int offsetY;

		// Runtime variable
		bool m_ShowServer;

		void Awake()
		{
			manager = GetComponent<NetworkManager>();
		}

		void Update()
		{
			if (!showGUI)
				return;

			if (!manager.IsClientConnected() && !NetworkServer.active && manager.matchMaker == null)
			{
				if (UnityEngine.Application.platform != RuntimePlatform.WebGLPlayer)
				{
					if (Input.GetKeyDown(KeyCode.S))
					{
						manager.StartServer();
					}
					if (Input.GetKeyDown(KeyCode.H))
					{
						manager.StartHost();
					}
				}
				if (Input.GetKeyDown(KeyCode.C))
				{
					manager.StartClient();
				}
			}
			if (NetworkServer.active && manager.IsClientConnected())
			{
				if (Input.GetKeyDown(KeyCode.X))
				{
					manager.StopHost();
				}
			}
		}

		void OnGUI()
		{
			if (!showGUI)
				return;

			int xpos = 10 + offsetX;
			int ypos = 40 + offsetY;
			const int spacing = 24*3;

			bool noConnection = (manager.client == null || manager.client.connection == null ||
				manager.client.connection.connectionId == -1);

			if (!manager.IsClientConnected() && !NetworkServer.active && manager.matchMaker == null)
			{
				if (noConnection)
				{
					if (UnityEngine.Application.platform != RuntimePlatform.WebGLPlayer)
					{
//						if (GUI.Button(new Rect(xpos, ypos, 200 *3, 20*3), "LAN Host(H)"))
//						{
//							manager.StartHost();
//						}
						ypos += spacing;
					}

					if (GUI.Button(new Rect(xpos, ypos, 105*3, 20*3), "LAN Client(C)"))
					{
						manager.StartClient();
					}

					manager.networkAddress = GUI.TextField(new Rect(xpos + 100*3, ypos, 95*3, 20*3), manager.networkAddress);
					ypos += spacing;
					//manager.networkAddress="192.168.10.9";

					if (UnityEngine.Application.platform == RuntimePlatform.WebGLPlayer)
					{
//						// cant be a server in webgl build
//						GUI.Box(new Rect(xpos, ypos, 200 *3, 25*3), "(  WebGL cannot be server  )");
//						ypos += spacing;
					}
					else
					{
						if (GUI.Button(new Rect(xpos, ypos, 200*3, 20*3), "LAN Server Only(S)"))
						{
							manager.StartServer();
							Screen.orientation = ScreenOrientation.Landscape;
							GameObject.Find ("GvrViewerMain").GetComponent<GvrViewer> ().VRModeEnabled=true;
						}
						ypos += spacing;
					}
				}
				else
				{
					GUI.Label(new Rect(xpos, ypos, 200*3, 20*3), "Connecting to " + manager.networkAddress + ":" + manager.networkPort + "..");
					ypos += spacing;


					if (GUI.Button(new Rect(xpos, ypos, 200*3, 20*3), "Cancel Connection Attempt"))
					{
						manager.StopClient();
					}
				}
			}
			else
			{
				if (NetworkServer.active)
				{
					string serverMsg = "Server: port=" + manager.networkPort;
					if (manager.useWebSockets)
					{
						serverMsg += " (Using WebSockets)";
					}
					GUI.Label(new Rect(xpos, ypos, 300*3, 20*3), serverMsg);
					ypos += spacing;
				}
				if (manager.IsClientConnected())
				{
					GUI.Label(new Rect(xpos, ypos, 300*3, 20*3), "Client: address=" + manager.networkAddress + " port=" + manager.networkPort);
					ypos += spacing;
				}
			}
			if (manager.IsClientConnected() && !ClientScene.ready)
			{
				if (GUI.Button(new Rect(xpos, ypos, 200*3, 20*3), "Client Ready"))
				{
					ClientScene.Ready(manager.client.connection);

					if (ClientScene.localPlayers.Count == 0)
					{
						ClientScene.AddPlayer(0);
					}
				}
				ypos += spacing;
			}
			if (NetworkServer.active || manager.IsClientConnected())
			{
				//if (GUI.Button(new Rect(xpos, ypos, 200*3, 20*3), "Stop (X)"))
				//{
				//	manager.StopHost();
				//}
				//ypos += spacing;
			}

			if (!NetworkServer.active && !manager.IsClientConnected() && noConnection)
			{
				ypos += 10;

				if (UnityEngine.Application.platform == RuntimePlatform.WebGLPlayer)
				{
//					GUI.Box(new Rect(xpos - 5*3, ypos, 220*3, 25*3), "(WebGL cannot use Match Maker)");
//					return;
				}
//
				if (manager.matchMaker == null)
				{
//					if (GUI.Button(new Rect(xpos, ypos, 200*3, 20*3), "Enable Match Maker (M)"))
//					{
//						manager.StartMatchMaker();
//					}
//					ypos += spacing;
				}
				else
				{
					if (manager.matchInfo == null)
					{
						if (manager.matches == null)
						{
							if (GUI.Button(new Rect(xpos, ypos, 200*3, 20*3), "Create Internet Match"))
							{
								manager.matchMaker.CreateMatch(manager.matchName, manager.matchSize, true, "", "", "", 0, 0, manager.OnMatchCreate);
							}
							ypos += spacing;

							GUI.Label(new Rect(xpos, ypos, 100*3, 20*3), "Room Name:");
							manager.matchName = GUI.TextField(new Rect(xpos + 100*3, ypos, 100*3, 20*3), manager.matchName);
							ypos += spacing;

							ypos += 10;

							if (GUI.Button(new Rect(xpos, ypos, 200*3, 20*3), "Find Internet Match"))
							{
								manager.matchMaker.ListMatches(0, 20*3, "", false, 0, 0, manager.OnMatchList);
							}
							ypos += spacing;
						}
						else
						{
							for (int i = 0; i < manager.matches.Count; i++)
							{
								var match = manager.matches[i];
								if (GUI.Button(new Rect(xpos, ypos, 200*3, 20*3), "Join Match:" + match.name))
								{
									manager.matchName = match.name;
									manager.matchMaker.JoinMatch(match.networkId, "", "", "", 0, 0, manager.OnMatchJoined);
								}
								ypos += spacing;
							}

							if (GUI.Button(new Rect(xpos, ypos, 200*3, 20*3), "Back to Match Menu"))
							{
								manager.matches = null;
							}
							ypos += spacing;
						}
					}

					if (GUI.Button(new Rect(xpos, ypos, 200*3, 20*3), "Change MM server"))
					{
						m_ShowServer = !m_ShowServer;
					}
					if (m_ShowServer)
					{
						ypos += spacing;
						if (GUI.Button(new Rect(xpos, ypos, 100*3, 20*3), "Local"))
						{
							manager.SetMatchHost("localhost", 1337, false);
							m_ShowServer = false;
						}
						ypos += spacing;
						if (GUI.Button(new Rect(xpos, ypos, 100*3, 20*3), "Internet"))
						{
							manager.SetMatchHost("mm.unet.unity3d.com", 443, true);
							m_ShowServer = false;
						}
						ypos += spacing;
						if (GUI.Button(new Rect(xpos, ypos, 100*3, 20*3), "Staging"))
						{
							manager.SetMatchHost("staging-mm.unet.unity3d.com", 443, true);
							m_ShowServer = false;
						}
					}

					ypos += spacing;

					GUI.Label(new Rect(xpos, ypos, 300*3, 20*3), "MM Uri: " + manager.matchMaker.baseUri);
					ypos += spacing;

					if (GUI.Button(new Rect(xpos, ypos, 200, 20), "Disable Match Maker"))
					{
						manager.StopMatchMaker();
					}
					ypos += spacing;
				}
			}
		}
	}
}
#endif //ENABLE_UNET