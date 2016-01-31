using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour {

    private AudioSource m_AudioSource;
    private ConstraintController m_ConstraintController;

    [SerializeField] private float m_ActiveConstraintPitchDelta = 0;
    [SerializeField] private float m_FailureVolumeDelta = 0;
    [SerializeField] private float m_SuccessVolumeDelta = 0;

    private float m_StartVolume;
    private float m_StartPitch;
    
    // Use this for initialization
	void Start () {
        m_AudioSource = gameObject.GetComponent<AudioSource>();
        m_ConstraintController = gameObject.GetComponent<ConstraintController>();
        m_StartVolume = m_AudioSource.volume;
        m_StartPitch = m_AudioSource.pitch;
	}

    void NotifyNumberConstraints(int number)
    {
        m_AudioSource.pitch = m_StartPitch + (m_ConstraintController.NumberOfActiveConstraints() * m_ActiveConstraintPitchDelta);
    }

    void ConstraintSuccessSound()
    {
		Debug.Log ("Lowering volume");
        m_AudioSource.volume += m_SuccessVolumeDelta;
    }

    void ConstraintFailureSound()
    {
		Debug.Log ("Raising volume");
        m_AudioSource.volume += m_FailureVolumeDelta;
    }
}