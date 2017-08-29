using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

    public Text text;

    private enum States { CELL, MIRROR, SHEET_0, LOCK_0, CELL2 };

    private States currentState;

    private bool tookMirror;

	// Use this for initialization
	void Start () {
		currentState = States.CELL;
        tookMirror = false;
	}
	
	// Update is called once per frame
	void Update () {
        switch (currentState) {
            case States.CELL:
                stateCell();
                break;
            case States.SHEET_0:
                stateSheet0();
                break;
            case States.LOCK_0:
                stateLock0();
                break;
            case States.MIRROR:
                stateMirror();
                break;
                case States.CELL2:
                stateCell2();
                break;
            default:
                break;
        }
    }

    void stateCell() {
        
        setTextAndOption("As you noticed, you just wake up in the prison cell.." + 
            "For now you can't remember what's going, I encourage you to look over " +
            "and try to find a way out.", 
            "Press S to view Sheets" + (tookMirror ? "" : ", M to view the Mirror") + "and L to view the Lock"
        );

		if (Input.GetKeyDown(KeyCode.S)) {
            this.currentState = States.SHEET_0;
        } else if (Input.GetKeyDown(KeyCode.M)) {
            this.currentState = States.MIRROR;
        } else if (Input.GetKeyDown(KeyCode.L)) {
            this.currentState = States.LOCK_0;
        }
    }

    void stateSheet0() {
        setTextAndOption("Wonderfull.. Looks like this sheet is there forever.." + 
            "Even with cold time I won't sleep with. Grhhh" +
            "and try to find a way out.",
            "Press R to return"
        );

        if (Input.GetKeyDown(KeyCode.R)) {
            this.currentState = States.CELL;
        }
    }

     void stateLock0() {

        if (!tookMirror) {
            setTextAndOption("Great, the lock seems usable, If I can found something to break it somehow",
                "Press R to return"
            );
        } else {
            setTextAndOption("Hum.. Let me think.. I should use my broken mirror to open the lock",
                "Press U to use the mirror, R to return"
            );

            if (Input.GetKeyDown(KeyCode.U)) {
                this.currentState = States.CELL2;
            }
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            this.currentState = States.CELL;
        }
    }

    void stateMirror() {
        if (!tookMirror) {
            setTextAndOption("This mirror is broken (and dusty), but it still can be used.", 
                "Press T to take it or R to return"
            );
            if (Input.GetKeyDown(KeyCode.T)) {
                this.tookMirror = true;
            }
        } else {
            setTextAndOption("Nothing to do here", "Press R to return");
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            this.currentState = States.CELL;
        }
    }

    void stateCell2() {
        
        setTextAndOption("Perfect, we should look around in this room");
    }

    void setTextAndOption(string text, string option = "") {
        this.text.text = text + "\r\n\r\n" + option;
    }
}
