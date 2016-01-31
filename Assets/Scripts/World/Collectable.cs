using UnityEngine;
using Scripts.Player;

namespace Scripts.World
{
    [RequireComponent(typeof(Collider2D))]
    public class Collectable : MonoBehaviour
    {
		new private bool m_enabled = true;

        void OnTriggerEnter2D(Collider2D otherCollider)
        {
			if (otherCollider.name == "Player" && m_enabled)
            {
                // Inform the game of points collected
				this.SendMessageUpwards ("ConstraintSuccess", this.gameObject);
				m_enabled = false;
				GetComponent<SpriteRenderer> ().enabled = false;
            }
        }

		void Reset() {
			m_enabled = true;
			GetComponent<SpriteRenderer> ().enabled = true;
		}
    }
}