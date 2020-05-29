using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PieceController[] pieces;
    private PieceController activePiece;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void MovePieceTo( float x, float y ){
        //pick available piece
        int i = 0;
        PieceController toMove = pieces[i];
        while( toMove.isInBoard ) {
            i++;
            if( i < pieces.Length ) {
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

    public void RestartGame() {
      int numPieces = pieces.Length;
      for(var i = 0; i < numPieces; i++) {
        pieces[i].Reset();
      }
    }
}
