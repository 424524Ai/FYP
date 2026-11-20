# Escape the Office
## Game Design Document (GDD)

**Version:** 0.3 — Current Gameplay Controls and Development Status  
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

| Input | Action | Status |
|---|---|---|
| W / A / S / D | Move | Implemented |
| Mouse | Look around | Implemented |
| E | Interact with objects or use the selected item | Implemented |
| Mouse Wheel | Cycle through the six hotbar slots, including empty slots | Implemented |
| F | Inspect the currently selected item | Implemented |
| Left Mouse Button + Drag | Rotate the inspected 3D item | Implemented |
| Esc | Exit item inspection; pause-menu behaviour is planned | Partially implemented |

The inventory is no longer opened with **Tab**. During normal gameplay, the six-slot hotbar remains visible. The planned Pause Menu will extend the context-sensitive Esc behaviour; it should not be described as implemented yet.

## 4. Inventory and Item Interaction

### Inventory and Hotbar

Players collect items through the existing world interaction system. Collected items are stored in an inventory represented by an **always-visible, six-slot hotbar**.

- The mouse wheel changes the selected slot; selection can include empty slots.
- The selected slot is visually highlighted.
- Collected items are represented by their item data and icons.
- The inventory enforces its capacity limit and provides independent feedback when full.
- The earlier Tab-operated inventory panel and its clickable **Select / Inspect** options are no longer part of the current control design.

### Item Selection and Usage

The selected item remains active while the player explores the office. The player aims at an interactable object and presses **E** to interact or attempt to use the selected item.

The first implemented item-use example is the office exit:

- The exit door checks the **currently selected** item against its assigned Access Card asset; it does not search the other hotbar slots.
- An empty or incompatible selection produces the message `Requires an access card`.
- Selecting the Access Card and pressing E unlocks the door, hides its renderer, disables its blocking collider and displays `Door unlocked` for approximately two seconds.
- The Access Card is **not consumed**, and the selected slot and highlight are preserved.
- Other item-use interactions, including any consumable-item rules, remain planned rather than fully implemented.

### 3D Item Inspection

Pressing **F** while an item is selected opens its 3D inspection view. The player can rotate the inspected object by dragging with the left mouse button, then press **Esc** to exit inspection.

Inspection is separate from using the selected item on environmental objects. Future puzzle items may reveal symbols, numbers or other clues through inspection.

### Interactable Item Visibility

Collectible items use a cyan, depth-aware outline to help players recognise objects they can pick up. The outline system is implemented, while broader scene-level visibility and occlusion regression testing remains ongoing.

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

- **Gameplay HUD:** Crosshair and interaction prompts are implemented; timer remains planned.
- **Six-Slot Hotbar (implemented):** Always-visible inventory slots, item icons and selected-slot highlight; mouse-wheel selection.
- **3D Inspection Interface (implemented):** F to inspect, left-mouse drag to rotate, Esc to exit.
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

*Status snapshot: 20 November 2026. This section describes the current Unity implementation rather than the earlier GDD prototype.*

### Implemented

- First-person movement, mouse look, character collision and grounded movement.
- Raycast-based interaction, E-key interaction and contextual prompts.
- Item pickup and inventory storage with six-slot capacity handling.
- Always-visible six-slot hotbar, mouse-wheel item selection and selection highlighting.
- Item icons and associated item data.
- F-key 3D item inspection, mouse-drag rotation and Esc to exit inspection.
- Cyan depth-aware outlines for collectible items.
- Selected-item use through the existing interaction system.
- Access Card exit interaction: selected-card validation, door unlock, renderer/collider disabling and temporary success/rejection feedback.
- Initial compilation, incremental code review and read-only QA checks for the Item Use System; nine core user-performed Play Mode observations reported as passing.

### In Progress / Partially Verified

- Regression and edge-case testing for item use, inspection, hotbar behaviour, outline occlusion and interaction boundaries.
- Office scene layout and room content; the current exit interaction exists, but the full five-area office level is not yet complete.
- Windows standalone build and end-to-end release verification.

### Planned

- Locked drawers, keys, containers and connected clue progression.
- Manager's Office keypad and computer puzzles.
- Full five-area office environment and final puzzle content.
- Seed-based randomisation and solvability validation.
- Timer, Pause Menu, New Game / Restart and game-state reset.
- Win-condition integration and Completion Screen.
- Final UI styling, visual art direction, lighting, audio and external playtesting.

The previous Tab-based inventory panel is **superseded** by the always-visible hotbar. The original GDD's description of item selection and inspection through a clickable inventory panel no longer represents the implemented controls.

## 13. Future Development

The following areas require further design or refinement:

- Detailed office layout and environmental assets.
- Exact puzzle content and clue placement.
- Visual and audio direction.
- UI presentation and accessibility.
- Randomisation implementation and testing.
- Final development scope and prioritisation.

## 14. Development Sprint Plan (November 2026 – March 2027)

### Schedule and delivery policy

- **Target full game completion / Feature Freeze:** **18 March 2027**.
- **19–31 March 2027:** maintenance only: small bug fixes, minor UI/text/audio adjustments, performance improvements, documentation and submission preparation. No major new features, new rooms, substantial puzzle redesigns or architectural rewrites.
- **Holiday development window:** **15 December 2026 – 17 January 2027**, with approximately **20–25 hours per week** available for a concentrated development sprint.
- **Term-time planning allowance:** approximately **10 hours per week**. Hours are estimates and will be adjusted to actual coursework and progress.
- **Feature Complete target:** **17 January 2027**. **Content Complete target:** **21 February 2027**. From **22 February**, prioritise testing, fixes and polish rather than adding features.

### Weekly sprints

| Sprint | Dates | Planned work | Definition of done |
|---|---|---|---|
| W0 | 20–22 Nov 2026 | Merge the Item Use System PR; reconcile GDD and development plan | Feature merged into `main`; next sprint scoped |
| W1 | 23–29 Nov | Design the five-area office floor plan and build a navigable greybox | Player can navigate the planned accessible areas |
| W2 | 30 Nov–6 Dec | Reusable locked drawer/container, key and item-based puzzle interactions | Key unlocks drawer and reveals a clue/item |
| W3 | 7–13 Dec | Keypad UI, code validation and Manager's Office access | Correct code opens the office; incorrect code gives feedback |
| Transition | 14 Dec | Stabilise scene and prepare holiday backlog | No known blocker to holiday integration |
| W4 | 15–20 Dec | Computer login puzzle, clue-reading/inspection content and full fixed puzzle chain | First Playable: complete escape possible without debug intervention |
| W5 | 21–27 Dec | Timer, pause, completion screen, New Game/Restart and state reset | Full start-to-finish and replay loop works |
| W6 | 28 Dec–3 Jan | Seed generation/reproduction, random keypad codes and selected clue variations | Same seed reproduces matching puzzle data |
| W7 | 4–10 Jan | Random Access Card container/location, final code and computer information; solvability tests | Multiple seeds can be completed without softlocks |
| W8 | 11–17 Jan | Integration fixes, art direction, initial props/audio and first Windows Alpha Build | Feature Complete: core systems integrated and Alpha Build runs |
| W9 | 18–24 Jan | Office furniture, materials and environmental assets | Five areas have recognisable visual identities |
| W10 | 25–31 Jan | Lighting, atmosphere and material polish | Cohesive lighting; clues remain legible |
| W11 | 1–7 Feb | Interaction sounds, ambience and feedback polish | Key interactions have clear audio/visual feedback |
| W12 | 8–14 Feb | Consistent HUD, keypad, computer, pause and completion UI | UI navigation is clear and reliable |
| W13 | 15–21 Feb | Puzzle pacing, guidance and first external playtests | Content Complete; feedback and issues recorded |
| W14 | 22–28 Feb | High-priority bug fixes and end-to-end regression testing | Critical playthroughs pass |
| W15 | 1–7 Mar | Release Candidate, performance checks and second external playtest | Standalone Windows candidate available |
| W16 | 8–14 Mar | Remaining defects, balance, documentation and demonstration preparation | No known progression-blocking defects |
| W17 | 15–18 Mar | Final regression tests, Windows Build and release sign-off | **Full game complete; Feature Freeze on 18 March** |
| Maintenance | 19–31 Mar | Minor corrections, documentation and submission | No substantial feature or design changes |

### Milestones

| Target date | Milestone | Expected outcome |
|---|---|---|
| 13 Dec 2026 | Core Puzzle Foundation | Office greybox, drawer and keypad interactions |
| 20 Dec 2026 | First Playable | Fixed puzzle chain can be completed |
| 27 Dec 2026 | Complete Game Loop | Timer, pause, completion and restart work |
| 10 Jan 2027 | Randomisation Complete | Reproducible seeds and valid puzzle configurations |
| 17 Jan 2027 | Feature Complete | All core gameplay features integrated; Alpha Build |
| 21 Feb 2027 | Content Complete | Environment, UI, audio and puzzle content ready |
| 7 Mar 2027 | Release Candidate | Tested Windows build with major defects addressed |
| **18 Mar 2027** | **Full Release / Feature Freeze** | **Complete game; only small changes thereafter** |

### Sprint workflow and risk management

Each feature follows a GitHub Issue and feature branch, implementation plan and developer approval, scoped Codex implementation (including Unity MCP where appropriate), incremental Code Review and QA assessment, developer-led Play Mode testing, development-log updates, then commit, pull request and merge. Review findings are assessed before any changes are made. Actual tests and unexecuted tests must be reported separately.

Prioritise a complete, playable fixed puzzle chain before expanding seed-based variation. If a sprint slips, reduce optional visual content or randomisation breadth before sacrificing end-to-end testing. W8 is also an integration buffer. Progress and estimates will be reviewed weekly; this schedule is a plan, not a claim that future work has already been completed.

## 15. AI Usage Disclosure

Generative AI tools from OpenAI, including **ChatGPT** and **OpenAI Codex**, have been used to support the planning, documentation and development of *Escape the Office*.

**ChatGPT** has assisted with brainstorming game mechanics and puzzle progression, structuring and refining the GDD, creating and revising the sprint plan, explaining implementation approaches, and reviewing development decisions. The developer evaluates suggestions and determines the game's requirements, scope and final direction.

**OpenAI Codex** has been used as an AI-assisted software development agent. Under developer-defined requirements and approval checkpoints, Codex has inspected existing Unity/C# code, proposed implementation plans, written or modified gameplay scripts, and used **Unity MCP** to inspect and configure Unity scene objects and component references. For example, during the Item Use System feature, Codex implemented the selected Access Card check, locked-door interaction and temporary feedback UI, then configured the associated scene objects. This coding and scene-configuration work is attributed to AI assistance rather than represented as entirely hand-written by the developer.

The development workflow also uses specialised **Code Reviewer** and **QA Tester** agents to assess changes and document findings. These assessments are not substitutes for actual gameplay testing: compilation checks, static/read-only checks, automated tests and developer-performed manual Play Mode tests are distinguished in feature logs. Agents do not independently approve final game behaviour or apply review recommendations without developer authorisation.

The developer retains responsibility for defining requirements, choosing among proposed designs, approving implementation scope, manually testing gameplay, deciding whether results meet acceptance criteria, managing GitHub changes and accepting the final work. AI-generated code and recommendations are reviewed and may be revised or rejected. Relevant AI-assisted activities, limitations and verification evidence are recorded in feature-specific development logs and review/QA reports.

This disclosure describes the development process to date and will be updated if the tools or their roles change. The developer remains accountable for the correctness, originality, attribution and final submission of the project.

---

**Document Status:** Working GDD — Sprint plan approved on 20 November 2026; subject to controlled revision as development progresses.
