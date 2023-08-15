# LeapPad-Manager
A decompilation of the very frequently used tool for hacking LeapPads/Leapster Explorers.

What needs to be fixed:
- The ability to connect to the devices broke during the decompilation process. Find out what causes this and make it work again.
- Figure out if it's possible to add Didj support to LeapPad Manager (would be great for disabling the "your Didj needs a tune up" message, as the method on the eLinux wiki doesn't work in most cases)
- Find out what prevents LeapPad Manager from connecting to devices after dev mode has been enabled (or find a workaround for it)
- Add an option to batch decrypt LF3 files while also not unpacking them
- There's some issues when it comes to installing custom apps on certain LeapPad models (the apps just never show up)
- Figure out if it's possible to add a "dump NAND" option, as being able to make backups of the entire device would be insanely useful for recovering from a brick (and save me from having to set up an ancient version of Python to do it)
