using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.World
{
    [RequireComponent(typeof(Collider2D))]
    public class LevelEnd : MonoBehaviour
    {
        private int m_LevelIndex = 0;

        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.name == "Player")
            {
                // End Level
                SceneManager.LoadScene(m_LevelIndex);
            }
        }
    }
}