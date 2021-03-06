First person VR Shooter game

The following main functionalities are implemented:

Creating the basic enemy character

The enemy moves on a set path, following a series of target GameObjects in a circular fashion. The character is rotated towards the current target in the path in order to ensure that the character walks towards it.


Detecting the player

The enemy’s field of view is defined as 120°. The enemy can only detect the player’s character if he is within the enemy’s FOV. Furthermore, we added an additional condition for the enemy to have to be within 15 m range of the player in order to detect him. Once detected, the enemy is rotated towards the player and the ‘Rifle Run’ animation is triggered. The enemy runs towards the player till he reaches a distance of 10 m from the player. This is where the ‘Rifle Shoot’ animation is triggered.


Shooting to the player

The enemy stops and targets the player with his rifle and proceeds to shoot. A cooling period of 0.02 seconds is added so that the enemy does not fire continuously. Furthermore, randomization to the enemy’s gun is added so that he may miss the player at times. When the enemy manages to hit the player, the player’s health is decremented. Once the player’s health reaches zero, the death animation for the player is triggered , camera is moved to a fixed position and the game is reset in 10 seconds.


Player shooting and health

The player has the ability to shoot at the enemy and kill him. The player’s health is also displayed in the UI, and is updated/decremented every time the player gets shot. When the player shoots the enemy, the enemy detects the player and rotates towards him so that the player is within the enemy’s FOV. When the enemy is killed, the gun gets detached from the enemy.


Creating the environment

There is an enemy character in each room . Each enemy character was assigned his own list of targets that were placed within their respective rooms. There is an escape door in the third room, which uses a trigger collider to detect if the player has reached the escape door. Once the player reaches this door, the game is reset in 10 s.


Ammo supply

There is an ammo crate in the fourth room. When the player reaches it, the bullets will be refilled.
