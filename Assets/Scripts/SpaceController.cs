using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceController : MonoBehaviour
{
    [SerializeField]
    private int spaceIndex;

    [SerializeField]
    private RefereeController referee;

    private float spaceX;
    private float spaceY;

    void Start() {
      // Sprite sprite = GetComponent<Sprite>();
      // spaceX = sprite.transform.position.x;
      // spaceY = sprite.transform.position.y;
    }

    public void HandleClick() {
      referee.onSpaceClicked( spaceIndex, 
        transform.position.x, transform.position.y );
    }

}
