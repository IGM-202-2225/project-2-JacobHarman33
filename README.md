# Project "The Space Alliance vs The Space Pirates"

[Markdown Cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Here-Cheatsheet)

### Student Info

-   Name: Jacob Harman
-   Section: 2 (Tues/Thurs 9:30 - 10:45)

## Simulation Design

The pirates have been a nuisance to the galaxy, and it's time to stop them, once and for all!
Join up with your friends in the space alliance to defeat the pirates. However, the pirates have
a captain of their own, so watch out!

My premise for the simulation is that the Player is a hero of the Space Alliance so allied ships will rally to the
player to attack the pirates, and won't attack until they have the courage to do so. 
The pirates, on the other hand, will immediately start attacking the allies, but only
seek their captain as a secondary objective.

Points are tallied for both sides when an ally or pirate ship is destroyed.

### Controls

- Movement
	- Up: Up Arrow/W
	- Down: Down Arrow/S
	- Left: Left Arrow/A
	- Right: Right Arrow/D

## Alliance Member

An allied spaceship of the player that tries to find the player to try
and defeat the pirates.

### Finding Player

Before an ally will attack the pirates, it has to find the player to
have the confidence to attack. In this state, it flees from pirates.

#### Steering Behaviors

- Wander
- Evade - nearest Pirate
- Seek - Player
- StayInBounds

- Obstacles - Asteroids
- Seperation - Other alliance members
   
#### State Transistions

- Spawn in
- When an ally is defeated, a new ally is spawned to continue transitioning into this state
   
### Attack Pirates

Once an ally has found the Player, they become inspired and
start attacking the pirates.

#### Steering Behaviors

- Pursue - closest pirate once in range
- StayInBounds
   
- Obstacles - Asteroids
- Seperation - Other alliance members

#### State Transistions

- Found the Player

## Space Pirate Member

These troublesome pirates attack members of the alliance as well as the player
to try and continue their reign on the galactic black market. When the find
the pirate leader, they become even more deadly!

### Attack Allies

Seek and pursue allied ships to try and take them out.

#### Steering Behaviors

- Wander
- Pursue - nearest ally
- Seek - Pirate leader
- StayInBounds

- Obstacles - Asteroids
- Seperation - Other pirates
   
#### State Transistions

- Spawn in
- When a pirate is defeated, a new pirate is spawned to continue transitioning into this state
   
### Follow the Leader

Once the captain has found other members of his pirate coalition, he coordinates them
to make attacks against allies more efficient.

#### Steering Behaviors

- Wander
- Pursue - closest ally once in range
- StayInBounds

- Obstacles - Asteroids
- Seperation - Other pirates
   
#### State Transistions

- Find the pirate captain

## Sources

- Spaceships and Asteroid Images from https://www.kenney.nl/assets/space-shooter-redux

## Make it Your Own

- Using Player from SHMUP
	- Player Input from SHMUP
	- Player can run into pirates to destroy them
	- However, if a player hits an asteroid they destroy the asteroid but it's a point off for allies
- Using Pirate Captain as counter to player coordination mechanic
	- Is an agent, but does not have any states similar to the player
	- Steering Behaviors
		- Wander
		- Pursue - nearest ally
		- StayInBounds
	- Obstacles - Asteroids
	- Seperation - Other pirates

## Known Issues

- N/A - runs normally

### Requirements not completed

- Not sure about states

