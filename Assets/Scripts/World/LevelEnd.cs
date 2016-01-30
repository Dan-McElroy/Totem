using Scripts.Player;
using UnityEngine;

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
            DoRestart();
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

        void DoRestart()
        {
            // Move the player back to the player start
            m_PlayerTransform.position = 
                new Vector3(m_PlayerStart.position.x, m_PlayerStart.position.y, m_PlayerStart.position.z);
            // Message the constraint manager to restart
            gameObject.SendMessage("Restart", SendMessageOptions.DontRequireReceiver);
        }
    }
}