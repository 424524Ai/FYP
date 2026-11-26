# Open Office Area blockout

- Date: 2026-11-26 (Europe/Dublin).
- Branch observed before implementation: `feature/office-level-blockout`.
- Status: blockout manually approved by the user; approved camera settings saved and final Edit Mode verification complete. Six user-reported camera/regression tests passed on 26 November 2026; other unreported cases remain unverified.
- Scope: approved revised plan, 12 x 10 x 3 m clear interior. No other rooms or new gameplay mechanics.

## Objective and approvals

Replace the earlier temporary office geometry with a primitive-based open office that supports four opposing-desk workstation groups and continuous circulation behind all eight chairs. Preserve movement, camera look, interaction, pickup, Inventory, six-slot Hotbar, collectible outlines, Item Inspection and Item Use.

The user approved the revised plan before implementation, including removal of old scene geometry, retention of ExitDoor, a bounded temporary southern safety landing, placeholder materials and this log. Play Mode, subagents and Git writes were prohibited and were not performed. GitHub Issue linkage was not verified; the existing feature branch was used as explicitly instructed.

## Pre-change inspection and removal dependency gate

- Read AGENTS.md. Initial Git status had no changed-file entries, subject to the existing global-ignore permission warning.
- Unity MCP confirmed OfficeScene, idle Edit Mode, not compiling, with no unsaved scene changes.
- Recursively checked the nine removal candidates: only Transform, MeshFilter, MeshRenderer and BoxCollider components were present.
- Scanned serialized component references throughout the loaded scene: no external component references targeted the removal candidates or their components. Transform hierarchy references were excluded from this dependency scan.
- Searched project gameplay scripts for old environment object names and GameObject/Transform lookup dependencies; none were found.
- Recorded serialized snapshots of retained non-Transform components and SHA-256 hashes of existing files under Assets, ProjectSettings and Packages, excluding the bundled TextMesh Pro and TutorialInfo directories.
- Reparented the original ExitDoor before removing old geometry. No prefab asset was deleted or edited.

## Implemented geometry and coordinates

World origin remains the centre of the finished floor. North is +Z. Interior boundaries are X [-6,6], Z [-5,5], Y [0,3]. Organisational parents have identity transforms. Dimensions below are X/Y/Z in metres.

| Element | Centre | Dimensions |
|---|---|---|
| Floor | (0,-0.1,0) | (12.4,0.2,10.4) |
| Ceiling | (0,3.1,0) | (12.4,0.2,10.4) |
| North/south side segments | (+/-3.3,1.5,+/-5.1) | (5.4,3,0.2) |
| North/south headers | (0,2.6,+/-5.1) | (1.2,0.8,0.2) |
| East/west side segments | (+/-6.1,1.5,+/-2.9) | (0.2,3,4.6) |
| East/west headers | (+/-6.1,2.6,0) | (0.2,0.8,1.2) |
| Retained ExitDoor | (0,1.1,-5.1) | (1.2,2.2,0.15) |

All four centred openings are 1.2 m wide and 2.2 m high. Structural cubes have solid, enabled BoxColliders. Floor and ceiling extend under/over the perimeter walls; wall overlaps occur within solid corners.

### Furniture

Group positions: 01 southwest (-2.6,0,-2.2), 02 southeast (2.6,0,-2.2), 03 northwest (-2.6,0,2.2), 04 northeast (2.6,0,2.2).

Each group contains:

- Two opposing desks at local Z +/-0.35. Desktop size (2,0.1,0.7), centre Y 0.70, finished surface Y 0.75. Four legs per desk at desk-local X +/-0.9, Z +/-0.25, centre Y 0.325, size (0.1,0.65,0.1).
- Two chairs at local Z +/-1.05, facing their desks. Each complete chair, including collider geometry, occupies (0.6,1,0.6). Seat, back, base and support use primitives.
- Two monitor assemblies at local Z +/-0.15 and keyboards at local Z +/-0.5. Screen size (0.55,0.3,0.05), centre Y 1.0. Keyboard size (0.45,0.03,0.15), centre Y 0.765. Computer details have no colliders.
- One divider at (0,0.95,0), size (2,0.4,0.04), extending 0.4 m above the desktop.

Four cabinets stand at (+/-5.7,0.6,+/-2.2), with measured world dimensions (0.6,1.2,0.45), facing inward. Rotated cabinets use local scale (0.45,1.2,0.6) to preserve the approved world dimensions.

Final quantities: eight desks, eight chairs, eight monitors, eight keyboards, four dividers and four cabinets. All furniture was built from scratch; the existing Desk prefab asset remains unchanged.

### Temporary safety geometry

All objects are under `Structure/TEMP_DoorwaySafety` and use solid colliders. Barriers are orange placeholders.

| Object | Centre | Dimensions |
|---|---|---|
| TEMP_DoorwayBarrier_North | (0,1.1,5.1) | (1.2,2.2,0.2) |
| TEMP_DoorwayBarrier_East | (6.1,1.1,0) | (0.2,2.2,1.2) |
| TEMP_DoorwayBarrier_West | (-6.1,1.1,0) | (0.2,2.2,1.2) |
| TEMP_SouthSafety_Floor | (0,-0.1,-5.8) | (1.6,0.2,1.6) |
| TEMP_SouthSafety_EndBarrier | (0,1.1,-6.5) | (1.6,2.2,0.2) |
| TEMP_SouthSafety_SideBarrier_L | (-0.7,1.1,-5.8) | (0.2,2.2,1.6) |
| TEMP_SouthSafety_SideBarrier_R | (0.7,1.1,-5.8) | (0.2,2.2,1.6) |

The landing floor overlaps the room floor at the threshold and leaves 1.2 m between side barriers. ExitDoor remains a temporary functional test door, not the final Main Exit; no doorway is assigned to another room. Future cleanup must consider the landing's safety role before removing it.

### Player, retained objects and lighting

- Player: (0,0,-4), rotation (0,0,0), facing +Z. Controller remains radius 0.3, height 1.8, centre (0,0.9,0). Initial PlayerCamera height was 1.6; final approved local position is (0,1.4,0), near clip 0.05, FOV 60 (see finalisation below).
- AccessCardObject: (3,0.755,1.75), unchanged rotation and scale (0.1,0.01,0.2), on northeast south desk.
- TestDocument: (-3.15,0.85,-1.75), unchanged rotation and scale (0.5,0.2,0.4), on southwest north desk. Side placement avoids the keyboard.
- Four new white realtime point lights at (+/-2.6,2.7,+/-2.2), range 5, intensity 2, shadows disabled; InspectionModel excluded from their culling masks.
- Existing Directional Light, ambient settings and camera setup retained. No baking or final lighting polish.

## Hierarchy

```text
OfficeEnvironment
  OpenOfficeArea
    Structure
      Floor
      Ceiling
      Walls
        North / South / East / West
          two segments + Header
      ExitDoor (original instance)
      TEMP_DoorwaySafety
        TEMP_DoorwayBarrier_North / East / West
        TEMP_SouthSafetyLanding
          TEMP_SouthSafety_Floor
          TEMP_SouthSafety_EndBarrier
          TEMP_SouthSafety_SideBarrier_L / R
    Furniture
      WorkstationGroup_01..04
        Desk_South / North
        Chair_South / North
        Monitor_South / North
        Keyboard_South / North
        Divider
      FilingCabinets
        FilingCabinet_01..04
    Lighting
      InteriorLight_01..04
```

## Removed and preserved objects

Removed only the approved old scene instances: Floor; Wall_North, Wall_West, Wall_East, Wall_South_Left, Wall_South_Right; Desk_01, Desk_02, Desk_03 and their five geometry children each (24 old GameObjects total).

Retained all 11 original roots: Main Camera, Directional Light, OfficeEnvironment, Player, TestDocument, InteractionCanvas, EventSystem, AccessCardObject, InventoryCanvas, ItemInspectionStage and ItemInspectionController. Retained the original ExitDoor and all its components. No gameplay components were removed.

## AI assistance and Unity MCP workflow

Codex used repository inspection and live Unity MCP. In-memory C# through MCP execute_code created primitives/materials, performed dependency and bounds checks, and preserved existing instances. It did not create or modify project C# scripts. MCP scene-save and hierarchy queries confirmed the saved scene. No subagents, nested CLI processes, Play Mode or Git writes were used.

The initial diagnostic compilation rejected obsolete GetInstanceID calls; the diagnostic was corrected to compare Unity object references. An initial capsule query included the Player's own collider; verification was corrected to exclude that collider. Neither diagnostic issue required scene-layout changes.

## Actual Edit Mode verification

| Check | Result and evidence |
|---|---|
| Room | PASS: actual collider inner faces yield 12.000 x 10.000 x 3.000; floor top 0, ceiling underside 3 |
| Thickness | PASS: floor, ceiling and wall colliders measure 0.2 m thickness |
| Doorways | PASS: four actual gaps 1.200 wide x 2.200 high |
| Workstation envelopes | PASS: combined renderer and collider envelope per group is 2.000 x 1.150 x 2.700 |
| Central circulation | PASS: actual envelope separation 3.200 m north-south and 1.700 m east-west |
| Behind chairs | PASS: four outer-chair rear clearances 1.450 m; four inner chairs back onto 1.700 m aisle |
| Cabinet-side routes | PASS: four measured gaps 1.800 m |
| Chair envelopes | PASS: all eight combined renderer/collider envelopes 0.600 x 1.000 x 0.600 |
| Desktops | PASS: eight collider sizes 2.000 x 0.100 x 0.700; top Y 0.750 |
| Route sampling | PASS: 802 Edit Mode OverlapCapsule samples clear on central routes and full rectangles around all four groups; radius 0.3, endpoint heights 0.31/1.51, Player collider excluded |
| Retained desktop objects | PASS: collider and renderer bottoms meet desktop top at Y 0.750; footprints supported; no furniture renderer overlaps; sample rays from standing eye height hit each object's interaction collider within 3 m |
| Spawn | PASS: (0,0,-4), facing +Z |
| Safety | PASS: seven grouped BoxColliders at approved bounds; landing flush with floor and overlapping threshold |
| Preservation | PASS: all 11 roots retained; all 61 retained non-Transform serialized component snapshots match the pre-change snapshot |
| Rendering/inspection | PASS configuration: PC_RPAsset unchanged; SSAO and CollectibleOutline enabled with original materials, cyan and width 2; retained camera and inspection components match snapshots |
| Shader diagnostics | PASS query: ShaderUtil.ShaderHasError false for URP Lit, CollectibleOutlineMask and CollectibleOutlineComposite |
| Scene save | PASS: MCP saved Assets/Scenes/OfficeScene.unity; subsequent query reports isDirty false |
| Compilation | Editor reports compilation finished, is_compiling false, no pending domain reload; no project scripts changed. No separate forced compile/build performed |
| Console | Query returned zero errors and 23 warnings: 21 MCP TransformHandle serialization warnings and two MCP WebSocket-not-initialised warnings. Console was not cleared |

These are the original Codex Edit Mode/configuration/physics-query observations, not successful gameplay tests. Capsule samples do not simulate CharacterController.Move, corner sliding, input or gameplay timing. Subsequent user-performed testing is recorded separately below.

## Files and scope observations

Intended changes:

- Modified `Assets/Scenes/OfficeScene.unity`.
- Added `Assets/Materials/OpenOffice.meta` and six materials with their .meta files: Floor, Wall, Ceiling, Furniture, ComputerEquipment, TemporaryBarrier.
- Added this development log.

Existing-file hash comparison found only OfficeScene and `ProjectSettings/URPProjectSettings.asset` changed within the monitored scope. The latter unexpectedly gained `m_ProjectSettingFolderPath: URPDefaultResources`. No command targeted this setting; automatic Unity serialization is suspected but causation is unconfirmed. It was left untouched rather than reverted or edited without approval. This is an out-of-scope working-tree observation requiring user review, not an approved rendering-settings change.

Existing scripts, prefab assets, materials, shaders, package files and other monitored project settings retained their hashes. AGENTS.md was not edited. Git continued to warn that `C:\Users\hleab\.config\git\ignore` was unreadable; no fix was attempted.

## Known limitations and pending manual tests

No design/layout deviations were needed. The unexpected URP settings serialization was subsequently investigated and restored as documented below. Codex performed no Play Mode testing, automated test suite, executable build, Code Review Agent or QA Agent run. The following is the original manual checklist: the later user results establish only the scenarios explicitly reported below; all other subcases remain **Not Run**, including detailed full-inventory, wrong-item, outline-occlusion and inspection drag/input-restoration regressions.

1. Spawn grounded facing +Z; confirm movement, mouse look and camera rendering.
2. Walk behind all eight chairs and complete a circuit around every workstation; test diagonal turns, chair backs and cabinet corners.
3. Traverse both central aisles and all outer routes without snagging.
4. Push against all blocked openings, wall corners and safety barriers; confirm no escape or falling.
5. Check interior visibility and furniture quantities from normal player positions.
6. Approach and interact with TestDocument; collect the Access Card normally.
7. Verify collectible cyan outline visibility and depth occlusion behind furniture/walls.
8. Verify six-slot wheel selection, empty-slot cycling, selection highlight, pickup and full-inventory item preservation using a separately approved fixture if required.
9. Verify empty/wrong selection rejection at ExitDoor; selected card unlock, card retention and unchanged selection/highlight.
10. Walk through the unlocked doorway onto the landing; confirm side/end barriers and threshold support.
11. Verify feedback lifetime and clearing during inspection.
12. Verify F/Esc inspection, panel-edge/corner dragging, continued dragging outside the panel, original model proportions, no inspection outline and restoration of input/cursor states.
13. Inspect Console for new runtime errors/warnings.

Future review/QA and Git commits/pushes remain under the user's separate authorisation/control.

## Finalisation: user testing and approved camera adjustment

Date: 26 November 2026. Runtime evidence in this section is **user-reported manual Play Mode testing**, not independent Codex execution.

### Initial manual testing and experiments

The user approved the Open Office layout and reported that player movement, mouse look, inventory, item inspection and door unlocking worked correctly. Two issues were observed: camera wall clipping, and furniture appearing too small with the viewpoint feeling too high.

The user reported these camera experiments:

- Reducing the near clipping plane from 0.3 to 0.05 resolved the observed wall clipping.
- Increasing FOV from 60 to 70 made objects appear smaller and was rejected.
- Reducing height from 1.6 to 1.45 improved the visual experience.
- Height 1.4 was subsequently tested and approved.
- Final FOV remains 60. Room and furniture dimensions remain unchanged.

### User-reported regression results

All six tests below were reported **PASS** on 26 November 2026. No additional issues were reported.

| ID | User-performed manual scenario | Result |
|---|---|---|
| OO-M01 | Rotate against all four walls and corners; no wall clipping | PASS — user-reported manual Play Mode |
| OO-M02 | Move around desks/chairs; visual scale acceptable | PASS — user-reported manual Play Mode |
| OO-M03 | Pick up Access Card with adjusted camera height | PASS — user-reported manual Play Mode |
| OO-M04 | Open Access Card inspection using F | PASS — user-reported manual Play Mode |
| OO-M05 | Unlock ExitDoor with Access Card and walk onto temporary safety landing | PASS — user-reported manual Play Mode |
| OO-M06 | Observe no new runtime Console errors or warnings | PASS — user-reported manual Play Mode |

These reports do not establish every subcase in the original checklist. They do not imply fresh automated testing, build verification, detailed inventory-capacity testing, wrong-item testing, complete inspection-input testing or outline-occlusion testing.

### Codex Edit Mode camera application and preservation checks

Preconditions: branch `feature/office-level-blockout`; Unity MCP connected; OfficeScene active in idle Edit Mode. The scene initially had unsaved changes. Live PlayerCamera settings before this operation were local (0,1.6,0), near clip already 0.05, FOV 60.

Applied and saved local position (0,1.4,0), near clip 0.05, FOV 60. The historical near-clip experiment began at 0.3, but the live pre-finalisation value was already 0.05. No other camera setting was changed.

- Compared 640 serialized scene-component records before and after: the only changed record was Player/PlayerCamera Transform, with local Y changing from 1.6 to 1.4. Camera component values were already approved and remained unchanged.
- Thus room/furniture transforms and components, Player transform/scale, CharacterController, lighting, Inventory, interaction, inspection and Item Use configuration remained unchanged by this operation.
- No missing components were found during the snapshot scan. Existing serialized gameplay references remained unchanged.
- Confirmed the saved scene file contains camera local (0,1.4,0), near clip 0.05 and FOV 60. Live scene query reports no unsaved changes and 11 roots.
- Live Unity state: not playing, not compiling, no pending domain reload. No forced compilation, Play Mode or tests were run.
- Live renderer query: SSAO and CollectibleOutline remain enabled with the original cyan colour, width 2 and material references.
- Final Console query at this checkpoint returned zero errors and zero warnings; it was not cleared by Codex. Earlier implementation-checkpoint warnings remain recorded above as historical observations.

### URP settings investigation and actual outcome

The complete Git diff showed only the addition `m_ProjectSettingFolderPath: URPDefaultResources` in `ProjectSettings/URPProjectSettings.asset`.

Read the installed package source, `Editor/DefaultScene/UniversalProjectSettings.cs`: the serialized field has the initializer `URPDefaultResources`; its accessor controls an editor default-resource folder. Conversion code uses it for generated renderer/settings asset destinations. `Editor/AssetPostProcessors/MaterialPostprocessor.cs` calls `UniversalProjectSettings.Save()`, providing a Unity serialization path that can emit this default field during material processing. The precise earlier triggering event was not independently reproduced.

This default field is unrelated to the Open Office layout or its runtime rendering configuration. Removing its explicit serialized line preserves the package's initialized default. Under the user's targeted-restoration approval, removed only that line while preserving all remaining bytes. No Git restore/reset was used.

The file now has no Git diff. Follow-up checks while Unity remained connected confirmed the line did not immediately regenerate. No reload/restart or forced material reimport was performed; a future URP settings/material save may serialize the default field again. No existing scene or rendering configuration was changed to suppress that possibility.

### Final scope and remaining limitations

Expected final working-tree changes are OfficeScene, the six OpenOffice materials and their metadata, the OpenOffice folder metadata, and this log. URPProjectSettings is restored to the Git version. No C# scripts, prefabs, existing materials, shaders, AGENTS.md or other settings were intentionally changed.

Git diff checking reports Unity-serialized trailing spaces on empty YAML values in OfficeScene; these were left untouched to avoid unrelated scene formatting. The existing Git global-ignore permission warning also remains untouched. Code Review/QA subagents, automated tests and executable builds remain separately pending; unreported manual checklist cases are not marked passed. Commit/stage/push/merge/branch operations were not performed.
