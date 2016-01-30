using UnityEngine;
using Scripts.Player;

namespace Scripts.World
{
    [RequireComponent(typeof(Collider2D))]
    public class Collectable : MonoBehaviour
    {
        public float m_Value = 10f;

        // Use this for initialization
        void Start()
        {
        }

        void OnTriggerEnter2D(Collider2D otherCollider)
        {
            if (otherCollider.name == "Player")
            {
                // Inform the game of points collected
                Destroy(gameObject);
            }
        }
    }
}