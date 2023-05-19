Try it out here: https://simmer.io/@bulat/game-dev-b2


Plane Glider (12 Points)
In this exercise the player has to maneuver a plane through an obstacle course without crashing. The package can be found on the Assets tab. Please note that the SFX have different volume levels and have to be adjusted in code.
1) Player Controller (4 Points)
   Choose one of the planes for the player and add a rigidbody and a fitting collider. When doing nothing, the plane should automatically fly to the right and slightly downwards.
   Holding Space boosts the player upwards and repeatedly plays a sound effect (the example game uses a pitched-up version of SFXJump) . Make sure that the sprite can't leave the screen.
   The player has limited fuel and can only boost if there's still some left. The remaining fuel is shown by an UI Image of type Filled with a text that goes up to 100. At 0 remaining fuel the text says 'Empty' instead.
   The plane is animated by flipping through the individual sprites. Furthermore, the plane is always rotated in its moving direction (see code from exercise 3). If you use your own sprite, it also has to have some kind of animation.
2) Obstacle Course (5 Points)
   For the following tasks you are allowed to create prefabs of the items and basic obstacles together as a group. Each member can then use them to build their own obstacle course.
   Build one level per group member that contains the elements described in the next few tasks and a goal at the end. The camera follows the player on the x-axis throughout each level.
   The player can collect items that restore some of the plane's fuel. Collecting an item plays a fitting sound effect.
   Create a ground and at least two basic obstacles. When the plane collides with either of them, a sound effect is played and the controls are deactivated. The player can then restart the level by pressing Space.
   [Hard] Create two different moving obstacles per group member and add them to your own level. If you want, you can also use the other group members' obstacles in your own course.
   [Hard] When the plane crashes, instead of just falling down, it will now bounce on the ground indefinitely. While bouncing around, collisions with items and every obstacle other than the ground are deactivated.
3) Menu (1 Point)
   Create a main menu that is shown at the start of the game. The level can be selected by clicking on a screenshot of your course. If the player presses R while in a level, or reaches the goal, they are brought back to the menu.
   (In case you don't implement a menu, please provide some other way of reaching all of your levels, so that we can grade your submission.)