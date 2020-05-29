using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PieceController[] pieces;
    private int numPieces;

    private PieceController activePiece;
    // Start is called before the first frame update
    void Start()
    {
        numPieces = pieces.Length;
        Debug.Log("Num pieces: " + numPieces);
    }

    public void StartTurn() {
      Debug.Log("Starting turn!");
      for(var i = 0; i < numPieces; i++) {
        pieces[i].OpenEyes();
      }
    }

    public void FinishTurn() {
      for(var i = 0; i < numPieces; i++) {
        pieces[i].CloseEyes();
      }
    }

    public void MovePieceTo( float x, float y ){
        //pick available piece
        int i = 0;
        PieceController toMove = pieces[i];
        while( toMove.isInBoard ) {
            i++;
            if( i < numPieces ) {
                toMove = pieces[i];
            }
            else {
                toMove = null;
                break;
            }
        }
        if( toMove ) {
            Debug.Log("Moving piece to " + x + " " + y);
            toMove.MoveToSpace( x, y );
        }
    }

    public void OnGameOver( bool didWin ) {
      for(var i = 0; i < numPieces; i++ ) {
        pieces[i].OnGameOver( didWin );
      }
    }

    public void RestartGame() {
      int numPieces = pieces.Length;
      for(var i = 0; i < numPieces; i++) {
        pieces[i].Reset();
      }
    }
}
