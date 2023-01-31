# OCB Console Helpers Mod - 7 Days to Die (A20) Addon

This Mod is intended to be used by other modders. It adds a few console
commands to help modders with miscellaneous tasks. I will add more commands
as I found the need for it or you can make Pull Requests if you like to have
your own helpers added to this mod.

You can use the commands by bringing up the console via F1, although you
probably should already know that if you want to use this mod, again this
mod is not meant to be used by end users!

## Helpers to adjust hand/foot positions for vehicles

Optimizing hand/foot positions/rotations via xml-edit and re-starting
the game is very tedious! This mod allows you to adjust the positions
in-game and see the results right away, which helps greatly to speed
up this process. The primary player must be attached to a vehicle in
order to use these functions properly.

- `ocb ikts`
- `ocb ikp LeftHand 5,8.4,-7.2`
- `ocb ikr LeftHand 28,-32.2,-0.2`
- `ocb ikp RightFoot -4.2,3.6,2`

Note: vector3 positions/rotations must no include any whitespace!

## Other utility functions

- `ocb cvars` to list all player.Buffs.CVars
- `ocb progression` to list ProgressionValues 
- `ocb qfp` to list all QuestFactionPoints

You can also use this mod to modify certain values:

- `ocb cvar name [float]`
- `ocb progression name [level]`
- `ocb qfp name value`

Not that there is a great chance that the game will overwrite
your changes on the next update. So don't expect to stuff just
magically happen if only changing CVars etc.

## Persisting console command history

This mod will persist the previous commands through restarts.
