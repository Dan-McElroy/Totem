using UnityEngine;
using Scripts.Player;

namespace Scripts.World
{
    public class LadderZone : MonoBehaviour
    {

        private PlatformerCharacter2D Player;

        // Use this for initialization
        void Start()
        {
            Player = FindObjectOfType<PlatformerCharacter2D>();
        }

        void OnTriggerEnter2D (Collider2D otherCollider)
        {
            if (otherCollider.name == "Player")
            {
                Player.m_OnLadder = true;
            }
        }

        void OnTriggerExit2D(Collider2D otherCollider)
        {
            if (otherCollider.name == "Player")
            {
                Player.m_OnLadder = false;
            }
        }
    }
}