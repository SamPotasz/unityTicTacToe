using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RefereeController : MonoBehaviour
{

    private int[,] boardData = new int [,] {{0, 0, 0},{0,0,0},{0,0,0}};

    //board controller to get the actual x/y position?

    private int currPlayer = 1;
    private int numMoves;

    private bool gameIsOver = false;
    private bool isTied = false;

    [SerializeField]
    TextMeshProUGUI headerText;

    [SerializeField]
    PlayerController player1;

    [SerializeField]
    PlayerController player2;

    [SerializeField]
    GameObject restartButton;

    [SerializeField]
    private AudioClip moveClip; //to play on sending piece to space
    
    [SerializeField]
    private AudioClip winClip; //to play on someone winning
    
    [SerializeField]
    private AudioClip tieClip; //to play on cats' game
    
    [SerializeField]
    private AudioClip restartClip; //to play on starting a new game

    [SerializeField]
    private AudioSource audioSource;

    /**
     *  properties to get controllers based on whose turn it is
     */
    public PlayerController currPlayerController {
        get {
            return currPlayer == 1 ? player1 : player2;
        }
    }
    public PlayerController otherPlayerController {
        get {
            return currPlayer == 1 ? player2 : player1;

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        updateHeaderText();
        restartButton.SetActive( false );
        SetPlayerEyes();
    }

    // public void onSpaceClicked( int spaceIndex, float x, float y ){
    public void onSpaceClicked( SpaceController space ) {
        int spaceIndex = space.spaceIndex;
        float x = space.gameObject.transform.position.x;
        float y = space.gameObject.transform.position.y;
       
        if(gameIsOver) {
            return;
        }

        Vector2Int rowCol = getRowColFromIndex(spaceIndex);
        int row = rowCol.x;
        int col = rowCol.y;
        //don't allow moves on non-empty spaces
        if(boardData[row, col] > 0) {
          return;
        }
        
        numMoves++;
        space.SetIsFull(true);

        currPlayerController.MovePieceTo( x, y );
        audioSource.PlayOneShot( moveClip, 1.0f );

        //update the board
        boardData[row, col] = currPlayer;

        //check for wins
        gameIsOver = checkForWin( rowCol );
        // Debug.Log("Winner? " + gameIsOver);

        //if nine moves made, draw.
        if( !gameIsOver && numMoves >= 9) {
            // Debug.Log("It's a tie!");
            gameIsOver = true;
            isTied = true;
            audioSource.PlayOneShot( tieClip, 1.0f );

        }

        //switch currPlayer
        if( gameIsOver ) {
          restartButton.SetActive( true );
          if( !isTied ) {
            audioSource.PlayOneShot( winClip, 1.0f );

            otherPlayerController.OnGameOver( false );
            currPlayerController.OnGameOver( true );
          }
        }
        else {
          switchPlayer();
        }

        // printBoardData();
        updateHeaderText();
    }

    public void RestartGame() {
      boardData = new int [,] {{0, 0, 0},{0,0,0},{0,0,0}};
      player1.RestartGame();
      player2.RestartGame();
      numMoves = 0;
      isTied = false;
      gameIsOver = false;
      currPlayer = 1;

      restartButton.SetActive(false);
      SetPlayerEyes();
      updateHeaderText();
      
      audioSource.PlayOneShot( restartClip, 1.0f );
    }

    void updateHeaderText() {
        headerText.text = this.statusString;
    }

    /**
     *  Go through rows, cols, and diagonals checking for three in a row
     */
    bool checkForWin( Vector2Int rowCol ) {
        int row = rowCol.x;
        int col = rowCol.y;
        
        //check rows
        bool rowWin = true;
        for(var i = 0; i < 3; i++) {
            if(boardData[i, col] != currPlayer){
                rowWin = false;
                break;
            }
        }
        if( rowWin ){
            return true;
        }

        //check cols
        bool colWin = true;
        for(var i = 0; i < 3; i++) {
            if(boardData[row, i] != currPlayer) {
                colWin = false;
                break;
            }
        }
        if( colWin ) { 
            return true;
        }

        //check diagonals
        if(row == col) {
            bool diagWin = true;
            for(var i = 0; i < 3; i++) {
                if(boardData[i, i] != currPlayer) {
                    diagWin = false;
                    break;
                }
            }
            if(diagWin) {
                return true;
            }
        }

        if( row + col == 2 ) {
            bool antiDiagWin = true;
            for( var i = 0; i < 3; i++ ) {
                if(boardData[i, 2 - i] != currPlayer) {
                    antiDiagWin = false;
                    break;
                }
            }
            if(antiDiagWin) {
                return true;
            }
        }

        return false;
    }

    void switchPlayer() {
      if( currPlayer == 1) {
            currPlayer = 2;
      }
      else {
          currPlayer = 1;
      }
      SetPlayerEyes();
    }
    
    // open and close the eyes of the pieces to show whose turn it is
    void SetPlayerEyes() {
      currPlayerController.StartTurn();
      otherPlayerController.FinishTurn();
    }

    Vector2Int getRowColFromIndex( int spaceIndex ){
        int row = (int) Mathf.Floor( spaceIndex / 3 );
        int col = spaceIndex % 3;
        return(new Vector2Int(row, col));
    }

    private void printBoardData() {
        string toPrint = "";
        for(var i = 0; i < 3; i++) {
            for(var j = 0; j < 3; j++) {
                toPrint += boardData[i, j];
            }
            toPrint += "/";
        }
        Debug.Log(toPrint);
    }

    
    /**
     *  to be displayed at the head of the game
     */
    public string statusString {
        get {
            string toReturn = "";
            if( gameIsOver ) {
                toReturn = "Game over! ";
                if( isTied ) {
                    toReturn += "It\'s a tie!";
                }
                else {
                    toReturn += "Player " + currPlayer + " wins!";
                }
            }
            else {
                if( numMoves == 0 ) {
                    toReturn += "Player " + currPlayer + " goes first.";
                }
                else {
                    toReturn += "Now it's Player " + currPlayer + "\'s turn.";
                }
            }
            return toReturn;
        }
    }
}
