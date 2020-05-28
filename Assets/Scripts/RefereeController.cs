using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RefereeController : MonoBehaviour
{

    private int[,] boardData = new int [,] {{0, 0, 0},{0,0,0},{0,0,0}};

    private int currPlayer = 1;
    private int numMoves;

    private bool gameIsOver = false;
    private bool isTied = false;

    [SerializeField]
    TextMeshProUGUI headerText;

    // Start is called before the first frame update
    void Start()
    {
        updateHeaderText();
    }

    public void onSpaceClicked( int spaceIndex ){
        if(gameIsOver){
            return;
        }

        numMoves++;
        Vector2Int rowCol = getRowColFromIndex(spaceIndex);
        //update the board
        boardData[rowCol.x, rowCol.y] = currPlayer;

        //check for wins
        gameIsOver = checkForWin( rowCol );
        Debug.Log("Winner? " + gameIsOver);

        //if nine moves made, draw.
        if( numMoves >= 9) {
            Debug.Log("It's a tie!");
            gameIsOver = true;
            isTied = true;
        }

        //switch currPlayer
        if( currPlayer == 1) {
            currPlayer = 2;
        }
        else {
            currPlayer = 1;
        }

        printBoardData();
        updateHeaderText();
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
            toPrint += "\n";
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
                    toReturn += "\It's a tie!";
                }
                else {
                    toReturn += "Player " + currPlayer + " wins!";
                }
                toReturn += "\nPlay again?";
            }
            else {
                toReturn = "It's Player " + currPlayer + "\'s turn.";
            }
            return toReturn;
        }
    }
}
