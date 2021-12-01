# tvengine
TextVentureEngine is a CLI TextVenture engine/player

## How to use
Open the app, then type /game <path at which a .tves is located>, obviously replacing the text in <> with a path to a tves. For example, /game C:\Users\Your Username\Desktop\example.tves

## How it works
TVEngine is written in C#. It loades JSON-like files called TVEScripts. These contain various "labels" that ask the player a question and gets responses, which can in turn direct the player to other labels.

## File type
.tves - Standard files containing a JSON-like script. <br>

## Developing
The .tves format has not been finalized, as we are still in active development. .tves are the only supported files at the moment. Here is an example:
```
{
  "labels": {
    "start" : {
      "action" : "ASK",
      "message" : "What do you want to do, {name}? \"kill\" or \"run\"?",
      "responses" : {
        "kill" : "label3",
        "run" : "label4"
      }
    },
    "label3" : {
      "action" : "ASK",
      "message" : "We are on Number 2! \"proc\" to proceed!",
      "responses" : {
        "proc" : "label4"
      }
    },
    "label4" : {
      "action" : "ASK",
      "message" : "We are on the final one. \"end\" to finish up.",
      "responses" : {
        "end" : "final"
      }
    },
    "final" : {
      "action" : "END"
    }
  }
}
```
"action" is required for both types. "message" and "responses" are required for ASK types. An END label will quit the game. A label called "start" **must** exist as this is where the engine starts looking.
