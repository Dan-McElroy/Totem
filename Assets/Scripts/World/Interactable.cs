using Scripts.Player;
using UnityEngine;


namespace Scripts.World
{
    /// <summary>
    ///     A script assigned to items that are interactable with using the Interact key.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public abstract class Interactable : MonoBehaviour
    {
        // Reference to the player.
        private PlatformerCharacter2D Player;

        [SerializeField]private float m_InteractionCooldown;
        
        // Use this for initialization
        void Start()
        {
            Player = FindObjectOfType<PlatformerCharacter2D>();
        }

        void OnTriggerEnter2D(Collider2D otherCollider)
        {
            if (otherCollider.name == "Player")
            {
                Player.m_Interactables.Add(this);
            }
        }

        void OnTriggerExit2D(Collider2D otherCollider)
        {
            if (otherCollider.name == "Player")
            {
                Player.m_Interactables.Remove(this);
            }
        }

        public abstract void Interact();
    }
}
