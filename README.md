# EasyNavigation
This mod was created to help with finding where you left off or where you entered in a factory!

# Features
- Enable or disable permanent trails
- Clear trails regardless of whether permanent trails is enabled or disabled

# Controls
These controls are hard coded and can not be changed.\
O - Enable or disable permanent trails\
I - Clear all trails regardless of if permanent trails is on or not

# Planned Features
- Displaying when the permanent trail is enabled or disabled (This one will be worked on next)
- Config settings\
-Set the duration, color and width of the trail.
- Enable the trail to be used for getting to and from the ship

# Information
I developed this mod in about 6 hours total. This is my first mod for a game, but I do have a lot of experience developing Unity games. I plan to clean the code up and add more features in the future.\
As for right now, the TrailRenderer does not have the HDRP/Lit shader and will show as pink. This is better for visibility as setting it to use the HDRP/Lit shader will make it blend into certain parts of the factory.\
The enable permanent trail bool variable defaults to "False" and does not display what it is currently set to.

# Changelog
## Version 0.2.3
- Added "Version 0.2.2" to the Changelog section in the README file. (Sorry for the spam!)

## Version 0.2.2
- Added the "CHANGELOG" file back to the zip file.

## Version 0.2.1
- Added a "Changelog" section to the README file.

## Version 0.2.0
- Added a GUI at the top of the player's screen to show when the permanent trail bool is enabled or disabled for a breif moment when they press the key binding for it.
- The console will no longer be spammed with "Object reference not set to an instance of an object" warnings (When the player leaves a server, there will be warnings that get spammed, though).
- Added a trail duration, permanent trail and clear trail key bindings, trail width and trail color in a config file that is auto-generated when the game is launched with the mod for the first time or through LethalConfig.

## Version 0.1.0
- Initial upload.
- Added the base functionality for the trail.
- Added the ability to set the isPermanentTrail bool to true or false.
- Added the ability to clear the trail while in the factory.
