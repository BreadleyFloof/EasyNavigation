# EasyNavigation
This mod was created to help with finding where you left off or where you entered in a factory!

# Features
- Enable or disable permanent trails.
- Clear trails regardless of whether permanent trails is enabled or disabled.
- Customize how the trail looks and the functionality of it.\
Currently available colors: black, blue, clear, cyan, gray, green, grey, magenta, red, white, yellow (Type in a color in the appropriate field in the config file or LethalConfig. These colors are not case sensitive.)

# Controls
These key binds can be changed in the configuration file or through LethalConfig.\
O - Enable or disable permanent trails\
I - Clear all trails regardless of if permanent trails is on or not

# Planned Features
- Enable the trail to be used for getting to and from the ship.
- Add a trail to all the players in a lobby with a random color.

# Information
I developed this mod in about 6 hours total. This is my first mod for a game, but I do have a lot of experience developing Unity games. I plan to clean the code up and add more features in the future.\
As for right now, the TrailRenderer does not have the HDRP/Lit shader and will show as pink. This is better for visibility as setting it to use the HDRP/Lit shader will make it blend into certain parts of the factory.\
The enable permanent trail bool variable defaults to "False" and does not display what it is currently set to.