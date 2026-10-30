# Escape the Office
## Game Design Document (GDD)

**Version:** 0.1 — Initial Draft  
**Project Type:** Final Year Project  
**Platform:** PC  
**Engine:** Unity 6  
**Genre:** First-Person Escape Room / Puzzle Game  
**Status:** Work in Progress

---

## 1. Game Overview

*Escape the Office* is a single-player, first-person escape room puzzle game set inside a modern office building.

The player must explore the environment, collect items, discover hidden clues, and solve interconnected puzzles to obtain an Access Card and escape through the main office exit.

The game features a fixed office layout with seed-based randomisation. Certain clues, puzzle codes, and the Access Card's location vary between playthroughs, providing replayability while preserving a consistent puzzle structure.

### Target Audience

Players who enjoy exploration, logical problem-solving, and escape room games.

### Main Objective

Find the Access Card and use it to unlock the main office exit.

## 2. Story and Setting

The player is an office employee preparing to leave work on a Friday evening.

However, the company's main exit has unexpectedly become locked, preventing the player from leaving.

The player begins inside the office and receives an introductory message explaining the situation.

To escape, the player must explore the workplace, uncover clues, solve puzzles, and locate the Access Card required to unlock the exit.

## 3. Core Gameplay

### Gameplay Loop

1. Explore the office environment.
2. Discover clues and interactive objects.
3. Collect and inspect items.
4. Solve puzzles to unlock new areas or information.
5. Locate and retrieve the Access Card.
6. Return to the main exit and escape.

### Player Controls

| Input | Action |
|---|---|
| W / A / S / D | Move |
| Mouse | Look around |
| E | Interact with objects or use selected items |
| Tab | Open or close Inventory |
| Left Mouse Button | Select items and interact with UI |
| Mouse Drag | Rotate objects during 3D Inspection |
| Esc | Close the current interface or open Pause Menu |

The Esc key is context-sensitive. When an interface is open, Esc closes that interface first. During normal gameplay, Esc opens the Pause Menu.

## 4. Inventory and Item Interaction

Players can collect items and access them through an inventory interface.

Each collected item is displayed in an Inventory Slot containing its icon and name.

Clicking a slot displays two options:

- **Select:** Sets the item as the currently selected item.
- **Inspect:** Opens a 3D inspection view.

### Item Selection and Usage

Selected items remain active after closing the inventory.

Players can aim at compatible objects and press E to attempt to use the selected item.

Most items remain in the inventory after successful use. Items specifically designated as consumable are removed when used successfully.

Attempting to use an incompatible item displays a short text message without changing the puzzle state.

### 3D Item Inspection

Players will be able to examine selected items in a dedicated 3D inspection interface.

Items can be rotated to reveal hidden information, such as symbols, numbers, or clues on the back of an object.

Inspection is separate from selecting an item for environmental interaction.

## 5. Level Design

The game takes place on a single office floor containing five main areas.

| Area | Purpose | Access |
|---|---|---|
| Open Office Area | Starting area, exploration and clues | Initially accessible |
| Meeting Room | Hidden clues and item inspection puzzles | Initially accessible |
| Storage Room | Items, cabinets and locked containers | Initially accessible |
| Manager's Office | Computer puzzle and critical information | Locked by keypad |
| Reception & Main Exit | Final escape location | Reception accessible; exit locked |

The Open Office Area serves as the central exploration space connecting the other areas.

The office layout remains fixed between playthroughs.

## 6. Puzzle Design

The game uses **Hybrid Puzzle Progression**.

Players can explore accessible areas freely and solve some puzzles in different orders. However, critical puzzles must be completed to unlock later stages.

### Puzzle Types

- Hidden Clues
- Keypad Code Puzzle
- Item-Based Puzzle
- 3D Item Inspection Puzzle
- Computer Puzzle

### Main Puzzle Progression

**Stage 1 — Initial Exploration**

The player explores the accessible office areas and searches for clues.

Two early puzzle branches can be completed in either order:

- Inspecting an item to discover part of a keypad code.
- Finding and using a key to open a locked drawer containing another part of the code.

**Stage 2 — Manager's Office**

The player combines the discovered clues to determine the keypad code and unlock the Manager's Office.

**Stage 3 — Computer Puzzle**

Inside the Manager's Office, the player solves a computer login puzzle.

The computer reveals the Access Card's location and the code required to unlock its container.

**Stage 4 — Access Card Retrieval**

The player travels to the indicated location and unlocks the corresponding container.

**Stage 5 — Escape**

The player selects the Access Card from the inventory and uses it on the main exit's card reader to complete the game.

Specific puzzle clues, item appearances, and detailed solutions may be refined during development.

## 7. Seed-Based Randomisation

The game uses moderate seed-based randomisation to provide variation between playthroughs.

### Randomised Elements

| Element | Behaviour |
|---|---|
| Office Layout | Fixed |
| Puzzle Structure | Fixed |
| Keypad Code | Randomised |
| Selected Clue Locations | Randomised |
| Access Card Location | Randomised |
| Final Container Code | Generated for the current game |
| Computer Information | Updated to match the current configuration |

### Access Card Locations

The Access Card can appear in one of three predefined areas:

- Storage Room
- Meeting Room
- Open Office Area

Each area will contain a designated locked container suitable for storing the card.

The computer in the Manager's Office provides the correct container location and access code for the current game.

Only one Access Card location is active per playthrough.

### Seed Reproducibility

The same Seed must produce the same randomised game configuration.

Starting a New Game generates a new Seed, while restarting the current game reuses the existing Seed.

Manual Seed entry may be provided as a development and testing feature.

### Solvability

The randomisation system must preserve a valid path to completion.

Key rules include:

- Critical items must be placed in reachable locations.
- Randomised clues must match their corresponding puzzle solutions.
- Puzzle dependencies must not create circular requirements.
- Computer information must match the Access Card's actual location and container code.
- The player must always be able to obtain the Access Card and reach the exit.

Predefined placement rules will constrain randomisation. Additional automated solvability validation may be developed as the system progresses.

## 8. Win and Failure Conditions

### Win Condition

The player wins by obtaining the Access Card and successfully using it to unlock the main office exit.

The game then displays a completion screen showing the player's escape time and the current Seed.

### Failure Conditions

The game does not have a time limit or a traditional failure state.

Incorrect puzzle answers and incompatible item interactions provide feedback without ending the game.

### Timer

A count-up timer records the player's escape time.

The timer continues during inventory management, item inspection, and puzzle interactions.

Opening the Pause Menu pauses the timer.

The timer stops when the player successfully escapes.

### Restart

Restarting the game:

- Retains the current Seed.
- Resets the timer.
- Resets the player's position.
- Clears the inventory.
- Restores puzzle and door states.
- Restores collected items to their original locations.

Starting a New Game creates a new randomised configuration.

## 9. User Interface

The planned user interface includes:

- **Gameplay HUD:** Crosshair, interaction prompts, and timer.
- **Inventory Panel:** Collected items, selection, and inspection options.
- **3D Inspection Interface:** Rotatable item view.
- **Keypad Interface:** Numerical code entry and feedback.
- **Computer Interface:** Login puzzle and information display.
- **Pause Menu:** Resume, Restart, and navigation options.
- **Completion Screen:** Escape time, Seed, and replay options.

UI interactions must manage mouse visibility and player controls appropriately.

## 10. Visual and Audio Design

### Visual Style

**TBD**

The final art direction, lighting style, materials, and environmental details will be determined during development.

### Audio Design

**TBD**

The specific use of background music, environmental audio, and interaction sound effects will be determined during development.

## 11. Technical Design

### Development Tools

| Tool | Purpose |
|---|---|
| Unity 6 | Game engine |
| C# | Gameplay programming |
| Unity URP | Rendering pipeline |
| GIMP | 2D artwork and UI assets |
| Git / GitHub | Version control and project management |

Additional tools may be introduced as required.

### Main Game Systems

The game is planned around the following systems:

- First-Person Player Controller
- Raycast-Based Interaction System
- Inventory and Item Selection
- 3D Item Inspection
- Puzzle and Door Interaction
- Game State Management
- Seed-Based Randomisation
- Timer and Pause Management
- Win Condition and Completion UI

These systems will be implemented and refined incrementally.

## 12. Development Status

### Implemented

- First-person movement and mouse look.
- Character collision.
- Raycast-based object interaction.
- Contextual interaction prompts.
- Item pickup.
- Basic inventory storage.
- Inventory panel opened with Tab.
- Dynamically generated inventory slots.
- Item icons and names.

### Planned

- Inventory item selection.
- 3D item inspection.
- Using inventory items on environmental objects.
- Office level construction.
- Keypad and computer puzzles.
- Seed-based randomisation.
- Timer and Pause Menu.
- Access Card retrieval and exit interaction.
- Completion screen.

## 13. Future Development

The following areas require further design or refinement:

- Detailed office layout and environmental assets.
- Exact puzzle content and clue placement.
- Visual and audio direction.
- UI presentation and accessibility.
- Randomisation implementation and testing.
- Final development scope and prioritisation.

## 14. AI Usage Disclosure

Generative AI (ChatGPT by OpenAI) was used as a supporting tool during the planning and documentation of this project.

AI assistance was used to:

- Brainstorm and discuss game design ideas, including gameplay mechanics, puzzle progression, and seed-based randomisation.
- Organise the structure and content of the Game Design Document (GDD).
- Improve the clarity, grammar, and presentation of the written documentation.

The game's core concept, design decisions, and final direction were determined by the developer. AI-generated suggestions were reviewed and adapted rather than accepted automatically.

The developer remains responsible for the accuracy, implementation, and final content of the project.

---

**Document Status:** Initial Draft — Subject to change throughout development.