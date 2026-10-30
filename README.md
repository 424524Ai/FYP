# Escape the Office

Escape the Office is a 3D first-person escape room puzzle game developed as a Final Year Project at South East Technological University (SETU).

## Game Overview

The player is an office employee trying to leave work on Friday. However, the main exit is locked. The player must explore the office, collect items, solve puzzles, and find an access card to escape.

The game will include seed-based randomisation to change room accessibility, item locations, and puzzle clues between runs.

## Planned Features

- First-person exploration
- Interactive objects and item collection
- Inventory system
- 3D item inspection
- Puzzle solving
- Seed-based randomisation
- Run timer

## Current Progress

### Player Controller

- Created a basic office environment with walls, floor, and desks.
- Implemented first-person movement, mouse look, gravity, and collison detection.

### Interaction System

- Implemented raycast-based object detection.
- Created a reusable interaction system using the IInteractable interface.
- Added E-Key interaction for objects.
- Added a crosshair and interaction prompt UI.

### Inventory System

- Implemented item collection and inventory storage.
- Added an inventory panel that can be toggled using the Tab key.
- Disabled player movement and camera look while the inventory is open.
- Created a reusable inventory slot prefab.
- Implemented dynamic inventory slot generation for collected items.
- Added support for displaying item icons and names.
- Created a custom Access Card icon using GIMP.
- Verified that collected items display correctly without duplicate slots when reopening the inventory.

## Technologies

- Unity 6
- C#
- Git and GitHub
- GIMP

## Development Status

The project is in active development. First-person movement, object interaction, item collection, and basic inventory management have been implemented.

The next development stage will focus on inventory item selection and 3D item inspection, followed by puzzle mechanics and further development of the office environment.

## Documentation

- [Game Design Document (GDD)](docs/GDD.md)