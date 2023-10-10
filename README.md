# KART RACING:
A 2D Top Down Kart Racing Game

# Setup Game:
To setup the game extract the zip file included and run the Application. 
Alternatively add it into Unity by using the "Open" button and "Add Project from disk" and choosing the downloaded folder.

# How To Play:
The Game is a Two Player Game. There are three controls, forward/backwards movement, turning, and braking.
The controls for Player 1 is WASD for Moving and Turning, and the Space button for Braking.
The controls for Player 2 is the Arrow Keys for Moving and Turning, and the right Control button for Braking.

# Game Mechanics:
Through the Main menu you can choose any or all levels, as well as choosing between two karts and customizing them with different colors. At any time during the race you can pause and go back to main menu if you want to switch kart or change level. At the start of each level there is a 3 second countdown starting the race and at the end of a race there is a win screen displaying the laps time of each player. At each level there are three laps with four mandatory checkpoints throughout the stage. Along the track there are rows of Item boxes similar to Mario Kart where a random item effect is chosen and applied. There is also a set knockback with the walls of the tracks and the other player.

# Work Process:
First thing I did was setup a Player Controller, establishing movement using Unity's new Input System. Movement is done with a 1D axis, forward and backwards and Turning/rotating is done the same way, left and right. It's done using the built in physics system. The Movement is calulated seperatly with acceleration/deceleration to make it less jagged and more realistic to how normal cars work. I've structured my code in a way that I have a Game Manager, Input Manager, Scene Manager, PlayerController all handling their seperate part. I decided to spawn in both Players at the start of each level instead of simply having the Players in each scene.
After felt movement was good I set up a simple track with a finishline and checkpoints. The Player has set knockback which is done with an OnCollisionEnter2D and knocks the Player both on walls and the other Player. I then worked a bit on UI and all the seperate tracks. All sprites is done by me. Once I had my tracks I did the Scene Manager, some minor tweaking here and there. The next major thing I did was PowerUps, but instead of cherry picking what power up you want on the track I put them in an Itembox similar to Mario kart where I simply pick a random index from a list and activate and apply the powerup. Lastly I did some more UI along with a laps timer, adding a Pause menu and Win screen along with some minor UI elements.

#Issues
Along the the work process I've had some minor issues which simply made me need to rethink and it was often fixed. One of the biggest issues I had was the kart customization, how to link the selected colors in the UI and add them to the Players, or rather adding the correct color the the correct player. I fixed this with the use of lists, saving the selected color in the UI and finding the kart part I want to color for each player and seting the color with the use of a for loop.

# Sources:
- Some sources I used was tips/help from classmates
- Unity documentation
- Youtube:
- https://www.youtube.com/@BMoDev
- https://www.youtube.com/@CodeMonkeyUnity


Created By:
Rasmus Göransson

Unity Ver. 2022.3.8f1
