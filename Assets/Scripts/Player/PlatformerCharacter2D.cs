using Scripts.World;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Scripts.Player
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.

        #region Ladder-climbing variables
        public bool m_OnLadder = false;
        public float m_ClimbSpeed;
        private float m_ClimbVelocity;
        private float m_GravityStore;
        #endregion


        public bool m_HasStartedWalking = false;
        private float m_GroundedDuration = 0f;
        [SerializeField] float m_JumpLeniency = 0.1f;

        #region Interaction variables
        public HashSet<Interactable> m_Interactables;
        #endregion
        
        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            m_GravityStore = m_Rigidbody2D.gravityScale;
            m_Interactables = new HashSet<Interactable>();
        }


        private void FixedUpdate()
        {
            m_Grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    m_Grounded = true;
            }
            m_Anim.SetBool("Ground", m_Grounded);

            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
        }


        public void Move(float move, bool jump)
        {
            if (move < -1e-3 || move > 1e-3)
            {
                m_HasStartedWalking = true;
            }
            
            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            { 
                // The Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("Speed", Mathf.Abs(move));

                // Move the character
                m_Rigidbody2D.velocity = new Vector2(move*m_MaxSpeed, m_Rigidbody2D.velocity.y);

                if (m_Rigidbody2D.velocity.y <= 0 && gameObject.layer == 8)
                {
                    gameObject.layer = 0;
                }

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                    // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }


            if (m_Grounded)
            {
				if (gameObject.layer != 8) {
					gameObject.layer = 8;       // Set to DoNotCollide
				}
                if (jump)
                {
                    if (m_Anim.GetBool("Ground"))
                    {
                        m_Anim.SetBool("Ground", false);
                    }
                    // Add a vertical force to the player.
                    m_Grounded = false;
                    m_GroundedDuration = 0f;
                    m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
                }
                else if (m_HasStartedWalking)
                {
                    m_GroundedDuration += Time.fixedDeltaTime;
                    if (m_GroundedDuration > m_JumpLeniency)
                    {
                        SendMessage("ConstraintFailure", gameObject, SendMessageOptions.DontRequireReceiver);
                    }
                }
            }

            // Sets gravity scale if on ladder.
            if (m_OnLadder)
            {
                m_Rigidbody2D.gravityScale = 0f;
                m_ClimbVelocity = m_ClimbSpeed * CrossPlatformInputManager.GetAxis("Vertical");
                m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_ClimbVelocity);
            }
            else
            {
                m_Rigidbody2D.gravityScale = m_GravityStore;
            }
        }

        public void Interact()
        {
            // Interact with the objects the player is near.
            if (!m_Interactables.Any())
            {
                foreach (var interactable in m_Interactables)
                {
                    interactable.Interact();
                }
            }
        }


        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

		void Reset() {
			m_GroundedDuration = 0f;
		}
    }
}
