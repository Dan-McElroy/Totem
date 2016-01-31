using Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.World
{
    [RequireComponent(typeof(Collider2D))]
    public class LevelEnd : MonoBehaviour
    {
        private int m_LevelIndex = 0;
        private Transform m_PlayerTransform;

        [SerializeField] private Transform m_PlayerStart;
        

        void Start()
        {
            m_PlayerStart = GameObject.FindGameObjectWithTag("Respawn").transform;
            m_PlayerTransform = FindObjectOfType<PlatformerCharacter2D>().transform;
            RepositionPlayer();
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.name == "Player")
            {
                // End Level
                //SceneManager.LoadScene(m_LevelIndex);
                CameraFade.StartAlphaFade(Color.black, false, 3f, 0f, DoRestart, true);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               
            }
        }

        void RepositionPlayer()
        {
            m_PlayerTransform.position =
                new Vector3(m_PlayerStart.position.x, m_PlayerStart.position.y, m_PlayerStart.position.z);
            m_PlayerTransform.GetComponent<PlatformerCharacter2D>().m_HasStartedWalking = false;
        }

        void DoRestart()
        {
            // Move the player back to the player start
            // Message the constraint manager to restart
            gameObject.SendMessageUpwards("Restart", SendMessageOptions.DontRequireReceiver);
            SceneManager.LoadScene(0);
        }
    }
}