using UnityEngine;
using Scripts.Player;

namespace Scripts.World
{
    [RequireComponent(typeof(Collider2D))]
    public class Collectable : MonoBehaviour
    {
		new private bool enabled = true;

        void OnTriggerEnter2D(Collider2D otherCollider)
        {
            if (otherCollider.name == "Player" && enabled)
            {
                // Inform the game of points collected
				this.SendMessageUpwards ("ConstraintSuccess", this.gameObject);
				enabled = false;
            }
        }

		void Reset() {
			enabled = true;
		}
    }
}