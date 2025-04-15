using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleInteraction : MonoBehaviour
{
    private GameEnding gameEnding;
    // partical system colliders
    private ParticleSystem ps;
    private AudioSource particle_sfx;
    private static float hit_scale = 25.0f;
    // these lists are used to contain the particles which match
    // the trigger conditions each frame.
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();

    void OnEnable()
    {
        ps = GetComponent<ParticleSystem>();
        gameEnding = Object.FindFirstObjectByType<GameEnding>();
        particle_sfx = GameObject.Find("Ectoplasm_SFX").GetComponent<AudioSource>();
    }
    
    void OnParticleTrigger()
    {
        // get the particles which matched the trigger conditions this frame
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        // play sound effect
        particle_sfx.Play();

        // each partical hit adds to the detection bar
        gameEnding.DetectingPlayer((float) numEnter * hit_scale);
    }
}
