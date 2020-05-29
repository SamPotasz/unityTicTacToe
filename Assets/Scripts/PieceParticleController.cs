using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceParticleController : MonoBehaviour
{
    [SerializeField]
    ParticleSystem happy;

    [SerializeField]
    ParticleSystem sad;
    
    public void PlayWin() {
        happy.Play();
    }

    public void PlayLose() {
        sad.Play();
    }

    public void Stop() {
        happy.Stop();
        sad.Stop();
    }
}
