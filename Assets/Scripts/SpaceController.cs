using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceController : MonoBehaviour
{
    [SerializeField]
    private int _spaceIndex;
    public int spaceIndex { get { return _spaceIndex; }}

    [SerializeField]
    private RefereeController referee;

    private float spaceX;
    private float spaceY;

    private bool _isFull = false;

    [SerializeField]
    private AudioClip onFullMouseOverClip;

    [SerializeField]
    private AudioClip onEmptyMouseOverClip;

    [SerializeField]
    private AudioSource audioSource;

    void Start() {
      // Sprite sprite = GetComponent<Sprite>();
      // spaceX = sprite.transform.position.x;
      // spaceY = sprite.transform.position.y;
    }

    public void OnMouseEnter() {
      if(audioSource) {
        AudioClip toPlay = _isFull ? onFullMouseOverClip : onEmptyMouseOverClip;
        audioSource.PlayOneShot( toPlay, 1.0f );
      }
    }

    public void HandleClick() {
      referee.onSpaceClicked( this );
    }

    public void SetIsFull( bool value ) {
      _isFull = true;
    }

}
