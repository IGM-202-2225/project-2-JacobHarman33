# Project "The Space Alliance vs The Space Pirates"

[Markdown Cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Here-Cheatsheet)

_REPLACE OR REMOVE EVERYTING BETWEEN "\_"_

### Student Info

-   Name: Jacob Harman
-   Section: 2 (Tues/Thurs 9:30 - 10:45)

## Simulation Design

The pirates have been a nuisance to the galaxy, and it's time to stop them, once and for all!
Join up with your friends in the space alliance to defeat the pirates. However, the pirates have
a captain of their own, so watch out!

My premise for the simulation is that the Player is a hero of the Space Alliance so allied ships will rally to the
player to attack the pirates, and won't attack until they have the courage to do so. 
The pirates, on the other hand, will immediately start attacking the allies, but don't actively
seek their captain.

### Controls

-   _List all of the actions the player can have in your simulation_
    -   _Include how to preform each action ( keyboard, mouse, UI Input )_
    -   _Include what impact an action has in the simulation ( if is could be unclear )_

## Alliance Member

An allied spaceship of the player that tries to find the player to try
and defeat the pirates.

### Finding Player

Before an ally will attack the pirates, it has to find the player to
have the confidence to attack. In this state, it flees from pirates.

#### Steering Behaviors

- Flee - nearest Pirate
- Seek - Player
- StayInBounds

- Obstacles - _List all obstacle types this state avoids_
- Seperation - Other alliance members, Player
   
#### State Transistions

- Spawn in
- When an ally is defeated, a new ally is spawned to continue transitioning into this state
   
### Attack Pirates

Once an ally has found the Player, they group up with the player and other inspired
allies to attack the pirates.

#### Steering Behaviors

- Seek - closest pirate
- Pursue - closest pirate once in range
- Cohesion - other inspired allies and Player
- Alignment - other inspired allies and Player
- StayInBounds
   
- Obstacles - _List all obstacle types this state avoids_
- Seperation - Other alliance members, Player

#### State Transistions

- Found the Player

## Space Pirate Member

These troublesome pirates attack members of the alliance as well as the player
to try and continue their reign on the galactic black market. When the find
the pirate leader, they become even more deadly!

### Attack Allies

Seek and pursue allied ships to try and take them out.

#### Steering Behaviors

- Seek - nearest ally
- Pursue - found ally once in range
- StayInBounds

- Obstacles - _List all obstacle types this state avoids_
- Seperation - Other pirates, Pirate captain
   
#### State Transistions

- Spawn in
- - When a pirate is defeated, a new pirate is spawned to continue transitioning into this state
   
### Follow the Leader

Once the captain has found other members of his pirate coalition, he coordinates them
to make attacks against allies more efficient. Pirates in the captain's range will
group up with the captain.

#### Steering Behaviors

- Seek - closest ally
- Pursue - closest ally once in range
- Cohesion - other coordinated pirates and pirate captain
- Cohesion - other coordinated pirates and pirate captain
- StayInBounds

- Obstacles - _List all obstacle types this state avoids_
- Seperation - Other pirates, Pirate captain
   
#### State Transistions

- Find the pirate captain

## Sources

- Spaceship Image from https://www.kenney.nl/assets/space-shooter-redux

## Make it Your Own

- Using Player from SHMUP
- Using Pirate Captain as counter to player coordination mechanic, could become seperate agent

## Known Issues

_List any errors, lack of error checking, or specific information that I need to know to run your program_

### Requirements not completed

_If you did not complete a project requirement, notate that here_

