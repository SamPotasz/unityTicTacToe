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

    [SerializeField]
    private EyeController[] eyes;
    private int numEyes;

    [SerializeField]
    private PieceParticleController particles;

    private bool _isInBoard = false;
    public bool isInBoard {
        get { return _isInBoard; }
    }

    private Vector3 startPos;

    private static float SPEED = 5.0f;
    private static float ARRIVAL_EPS = 0.01f;

    public void MoveToSpace( float x, float y ) {
        _isInBoard = true;
        isMoving = true;
        // rigidBody.bodyType = RigidbodyType2D.Kinematic;
        // boxCollider.enabled = false;
        this.movingTo = new Vector3( x, y, this.transform.position.z );
    }

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        numEyes = eyes.Length;
        particles.Stop();
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

    public void OpenEyes() {
        for(var i = 0; i < numEyes; i++) {
            eyes[i].Open();
        }
    }

    public void CloseEyes() {
        for(var i = 0; i < numEyes; i++) {
            eyes[i].Close();
        }
    }

    public void OnGameOver( bool didWin ) {
        if( didWin ) {
            particles.PlayWin();
        }
        else {
            particles.PlayLose();
        }
    }

    /// summary
    // on game restart
    public void Reset() {
        isMoving = false;
        _isInBoard = false;
        transform.position = startPos;
        particles.Stop();
        // rigidBody.bodyType = RigidbodyType2D.Dynamic;
    }
}
