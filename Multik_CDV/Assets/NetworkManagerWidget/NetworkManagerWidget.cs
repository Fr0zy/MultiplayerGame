using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace Widgets
{
    public class NetworkManagerWidget : MonoBehaviour
    {
        [SerializeField] private GameObject connectionInfoObject;
        [SerializeField] private TextMeshProUGUI debugInfoLabel;
        
        [SerializeField] private Button hostButton;
        [SerializeField] private Button clientButton;
        [SerializeField] private Button serverButton;

        private void Awake()
        {
            hostButton.onClick.AddListener(  () => { NetworkManager.Singleton.StartHost();   connectionInfoObject.SetActive(false); debugInfoLabel.gameObject.SetActive(true); debugInfoLabel.text = "Host";   });
            clientButton.onClick.AddListener(() => { NetworkManager.Singleton.StartClient(); connectionInfoObject.SetActive(false); debugInfoLabel.gameObject.SetActive(true); debugInfoLabel.text = "Client"; });
            serverButton.onClick.AddListener(() => { NetworkManager.Singleton.StartServer(); connectionInfoObject.SetActive(false); debugInfoLabel.gameObject.SetActive(true); debugInfoLabel.text = "Server"; });
        }
    }
}
