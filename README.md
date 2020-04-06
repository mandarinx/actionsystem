# Action System

Exploring ways to handle actions, verbs, properties and such in a roguelike game.

# Challenges

Stuff I'd like to try solving. The action system might not be the best solution for all of the challenges, but they will make me look at the architecture and API design from different perspectives.

- Make something happen when a character steps on a tile.
- Make a bomb go off when a character opens a chest.

*Misc*

- Can the target property type be moved to the attribute of the action system without messing up the code too much?

# Notes

## Optimal play

Roguelikes are designed for the experienced player, and this comes with a special design aesthetic. If there are gameplay features that seem fun or interesting on the surface, but would lead to tedious gameplay if the player approached them with a single-minded attitude of playing to win, they should not be included.

Examples:

* Amnesia effects make you forget automaps and item identification. A player playing optimally should then write all maps and identifications down so that they could retain the information even when the game stops providing it. So you probably shouldn't have amnesia effects.
* If items can be sold in shops, it might be optimal to carry every piece of junk the player finds into a shop. You could provide some quicker way to convert junk items into cash, or alternatively make shops only sell items, never buy.
* If there is no food clock and the player's health regenerates over time, the optimal play is to stand still and regenerate any damage you have every chance you get, possibly leading to very slow progress.
* If the player has skills that improve with use, the optimal play might be to spend the first hour of the game shooting magic missiles at a wall to make the character very skilled with the spell.

## Reactions

Can be solved using an energy system. Player spends x time, all npc's are added x time to their time pool. When the pool is large enough, they spend time. That way, player will experience retaliations from npc's.

## Energy system

The time it takes for the player to do an action, is the amount of time the npc's are given.

- What about a wizard that needs to charge a spell before using it? He should probably make a decision first. Then wait for the time pool to accumulate. The AI could be given an option to cancel a decision and make a new one. Let's say the player manages to get up close to the wizard and starts attacking. The wizard should retreat. The AI needs to reevaluate the situation. Allthough, that can lead to dull enemies.

## Animation system

Animations are added to a list. After all actions are resolved, the animations are played in parallel. There are two lists, one for player actions, and one for all npc's. 

For cases like when a melee attack kills an entity, one would usually like the death anim to play at the right time. A simple solution is to to make all attack animations reach the blow that kills at the same frame. And the death anim has to reach the hit at the same frame. They can last longer, but has to have to the impact on the same frame.

Animations should be implemented as different types. That way each animation type can do custom things, like updating map lighting, playing sounds, playing keyframed animations, lerping and whatnot.

What about when the players shoots an arrow, hurts an npc, and the npc runs off. The arrow should animate all the way to the noc, play the hit animation, and then let the npc run off. With this approach, it seems hard to make a system that doesn't end up as one that needs to handle all kinds of edge cases. The shoot arrow system should do just that, fire arrows. The list of npc animations might need to be multiple lists, one for each npc. Each list should be able to hold multiple animations. That will solve this problem. As long as the hurt animation is played before running off. Hurt is added by the health component as a reaction to the player shooting an arrow at the npc. The run off animation is added by the monster ai on the next turn, so it should be ok.

