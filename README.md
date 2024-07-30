# OCB Console Helpers Mod - 7 Days to Die (V1.0 exp) Addon

This Mod is intended to be used by other modders. It adds a few console
commands to help modders with miscellaneous tasks. I will add more commands
as I found the need for it or you can make Pull Requests if you like to have
your own helpers added to this mod.

You can use the commands by bringing up the console via F1, although you
probably should already know that if you want to use this mod, again this
mod is not meant to be used by end users!

## Helpers to adjust hand/foot/seat position/rotation for vehicles

Optimizing hand/foot positions/rotations via xml-edit and re-starting
the game is very tedious! This mod allows you to adjust the positions
in-game and see the results right away, which helps greatly to speed
up this process. The primary player must be attached to a vehicle in
order to use these functions properly.

- `ocb vikt` // open ui for adjustments
- `ocb ikts` // list all ikt targets
- `ocb ikp LeftHand 5,8,-7` // set position
- `ocb ikr RightFoot -4,3,2` // set rotation
- `ocb seat` // show seat position/rotation
- `ocb seatp -.41,.33,.06` // seat position
- `ocb seatr -25,0,0` // seat rotation

Note: vector3 positions/rotations must not include any whitespace!

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

## Download and Install

[Download from GitHub releases][1] and extract into your Mods folder!  
Ensure you don't have double nested folders and ModInfo.xml is at right place!

[![GitHub CI Compile Status][3]][2]

## Changelog

### Version 0.3.1

- Mitigate crash when modifying rig for feet IKs
- Add seat switching command (experimental)
- Increase some UI min/max values per demand

### Version 0.3.0

- First compatibility with V1.0 (exp)
- Add `vikt` cmd to open adjustment UI

### Version 0.2.3

- Fix issue setting IK targets on some vehicles

### Version 0.2.2

- Add support for vehicle seat adjustments

### Version 0.2.1

- Update compatibility for 7D2D A21.2(b14)

### Version 0.2.0

- Update compatibility for 7D2D A21.0(b313)

### Version 0.1.2

- Last A20.6/7 compatible version

[1]: https://github.com/OCB7D2D/OcbConsoleHelpers/releases
[2]: https://github.com/OCB7D2D/OcbConsoleHelpers/actions/workflows/ci.yml
[3]: https://github.com/OCB7D2D/OcbConsoleHelpers/actions/workflows/ci.yml/badge.svg
