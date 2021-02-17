# tvengine
TextVentureEngine is a dynamic, powerful engine that can be used to play and make text-adventures playable in the command line.

## How it works
TVEngine is written in C#. It loades JSON-like files called TVEScripts, or TVEHexes. These contain various "labels" that ask the player a question and gets responses, which can in turn direct the player to other labels.

## File types
.tvsc - Standard files containing a JSON-like script. These are **great** for testing but not so great for any production release, as the user can edit them easily.
.tvhx - A .tvsc file in hexadecimal format. Harder to read by the user, so are better for commercial release.
.tvcm - A fully compiled .tvsc practically impossible to edit or view by the user in any meaningful format. Support for these hasn't been released yet, and won't be for a while.

## Developing
The .tvsc format has not been finalized, as we are still in active development. This section will contain details on creating your TextVenture.

## TVEngine Make
Make is a separate program for easy, flowchart-like development of your TextVentures. It will be built into the TVE Development Kit.

## TVEDevKit
TVEDevKit will be a distribution of TVEngine, containing several development utilities including:
*TVEngine - Base player system.
*TVEngine Make - GUI TextVenture Creator.
*.tvhx converter - converts a .tvsc to .tvhx.
*.tvcm converter - converts a .tvsc or .tvhx to .tvcm.
*.exe converter - create a fully packed .exe from a .tvsc, .tvhx, or .tvcm. This can be played without the engine installed.\

## Expected Release
TVEngine Beta and it's accompanying utilities will release around mid-April.
