using UnityEngine;

namespace Scripts.World
{
    public class Painting : MonoBehaviour
    {

        [SerializeField]
        private int constraintIndex;
        [SerializeField]
        private GameObject m_Target;

        private SpriteRenderer m_Renderer;


        void Awake()
        {
            m_Renderer = GetComponent<SpriteRenderer>();
        }

        void Reveal(int i)
        {
			if (i == constraintIndex) {
				m_Renderer.enabled = true;
                m_Renderer.color = Color.white;
            }
        }

		void Hide(int i)
		{
			if (i == constraintIndex) {
				m_Renderer.enabled = false;
			}
		}

        void Fail(int i)
        {
            if (i == constraintIndex)
            {
                m_Renderer.color = Color.red;
            }
        }
    }
}