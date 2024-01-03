# The-AprenticeKnight
 
What was meant to be a 3D platformer game turned into a 3D person souls-like project. Cast spells, and fight with a variety of weapons in a three-level adventure.

## Table of Contents
- [Installation](#installation)
- [Features](#features)
- [Contributing](#contributing)
- [Acknowldgement](#acknowledgment)
- [Badges](#badges)
- [Debugging](#debugging)

## Installation
1. Clone repository ` bash git clone https://github.com/ACartaya96/The-AprenticeKnight.git ` 
2. Download at: https://acartaya96.itch.io/the-apprentice-knight

### Features
●	We have implemented our 3 significant mechanics:
  ○	A Modular Spell System that allows art and tech to add spells into the game with little to no additional code. Some spells in the game are as follows:
     
     ■	FireBall plays a significant role in our first level as it will be used to clear brambles from the path and destroy your enemies from range.
     
     ■	Heal Spell can be used to keep the player topped off and avoid things like poison effect from killing you.
     
     ■	The Earth Pillar Spell can be used to launch the player into the air a huge distance. Good for platforming.
     
     ■	The Magic Missile Spell can be used to target fast-moving or small enemies. It does slightly less damage than the fireball spell, but will track locked-on enemies so you hit with every cast.
  
  ○	Inventory/Weapon System that is also modular and implementable by Art and Tech
    
     ■	Weapon Swap system
     
     ■	The inventory system has a swap function that allows the player to swap out weapons, spells, and potions, each created to have their own animations.

  ○	An AI State Machine that allows our player to have combatants, this is also designed for modular design to expand our project with multiple enemies and multiple                behaviors. 
    
    ■	Are A.I. has a complex State Machine that not only chases the player, but determines the player's angle, and distance and uses this information to choose the best attack within it's attack array to strike the player.
    
    ■	The Behavior System is modular so that it can be expanded on for further expansions with new behaviors.

### Acknowledgment
At the moment I am trying to contact the old team to get their last names and links onto this page, but the main contributors as I have now are:
Alexander Cartaya
Luis Gonzalez
Kevin 
Manny

### Badges
![Build Status]([https://github.com/ACartaya96/The_ApprenticeKnight/workflows/CI/badge.svg?branch=main])
