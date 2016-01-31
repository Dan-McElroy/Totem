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


        void Start()
        {
            m_Renderer = GetComponent<SpriteRenderer>();
        }

        void Reveal(int i)
        {
            m_Renderer.enabled = (i >= constraintIndex);
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