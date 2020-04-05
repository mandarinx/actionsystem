# Action System

Exploring ways to handle actions, verbs, properties and such in a roguelike game.

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
