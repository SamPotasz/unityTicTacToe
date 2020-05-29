using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceController : MonoBehaviour
{
    private Vector3 movingTo;
    private bool isMoving = false;

    // [SerializeField]
    // private Rigidbody2D rigidBody;

    // [SerializeField]
    // private BoxCollider2D boxCollider;

    private bool _isInBoard = false;
    public bool isInBoard {
        get { return _isInBoard; }
    }

    private static float SPEED = 5.0f;
    private static float ARRIVAL_EPS = 0.01f;

    public void MoveToSpace( float x, float y ) {
        Debug.Log("starting to move " + gameObject.name);
        _isInBoard = true;
        isMoving = true;
        // rigidBody.bodyType = RigidbodyType2D.Kinematic;
        // boxCollider.enabled = false;
        this.movingTo = new Vector3( x, y, this.transform.position.z );
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if( isMoving ) {
            float interp = SPEED * Time.deltaTime;

            transform.position = Vector3.Lerp( transform.position, movingTo, interp );

            if( Vector3.Distance( transform.position, movingTo ) <= ARRIVAL_EPS ){
                isMoving = false;
            }

        }
    }
}
