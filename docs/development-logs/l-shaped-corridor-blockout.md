# L-shaped corridor greybox

- Date: 26 November 2026 (Europe/Dublin).
- Branch: `feature/office-level-blockout`.
- Baseline HEAD: `0f9815ce5cb88e834027a2422aabffb0c09c189e`.
- Status: accepted for the current blockout stage following user-performed Manual QA Round 2 on 26 November 2026: 12 PASS / 0 FAIL / 0 NOT RUN. Round 1 remains 21 PASS / 1 FAIL / 0 NOT RUN. Lighting was excluded from Round 2 and remains deferred to Lighting & Atmosphere.
- Scope: corridor construction and translation of existing room roots only. No new gameplay, furniture, doors, locks, final art, scripts, prefab assets or material assets.

## Objective and approved planning

Separate the existing rooms with a navigable L-shaped corridor, improving spatial separation and corporate-office circulation while retaining all internal room layouts and gameplay. Open Office stays fixed. Meeting Room moves Z +2.2 m and Server Room moves X -2.2 m.

The initial plan specified 2 m clear corridors, 3 m clear height, 0.2 m structural thickness, a continuous 2 x 2 m northwest corner, four aligned open doorways, independent room doorway walls, four floor/ceiling threshold extensions, two temporary endpoint barriers and five temporary point lights. Existing Open Office north/west walls, headers and partition placeholders remain in place.

The supplemental review corrected the earlier North Corridor midpoint calculation: lights at X -3.1 and +3.1 are 6.2 m apart, so their midpoint offset is 3.1 m, not 2.6 m. At height 2.7 m, range 4 m has a floor-level horizontal range radius of 2.951 m. Required distances are 4.111 m at the centreline midpoint/eastern endpoint, 4.231 m at their corridor edges and 4.283 m at the far doorway thresholds. Only the two new North lights use range 4.3 m; other new lights retain range 4 m. This is geometric range coverage, not proof of rendered illumination.

The original 22 primitives enclosed the playable space mathematically but left several exterior wall/slab junctions as edge contacts. Twelve approved floor/ceiling perimeter strips provide full-footprint junctions without changing clear dimensions. The shortened west strips avoid overlap with Server Room threshold slabs. Final construction contains 34 primitives.

The user explicitly approved both plans, the additional strips and corrected light ranges before implementation. All coordinates were applied in world space, including children of the translated room roots.

## Pre-implementation baseline

- Read AGENTS.md. Approved branch and full HEAD matched; working tree was clean.
- Unity MCP connected to Unity 6000.6.0f1. OfficeScene was active, saved, idle in Edit Mode, not compiling and without pending domain reload.
- Captured **489 GameObjects, 1,478 components and 11 scene roots**, persistent GlobalObjectIds, parent relationships, world transforms, serialized local/component values and direct serialized reference targets.
- Captured **12,657 baseline records**, including every existing component and 32 explicitly required gameplay references; none of those required references was null.
- Captured SHA-256 hashes for **236 files** across Assets, ProjectSettings, Packages and docs.
- Baseline Console: **0 errors, 2 MCP WebSocket disconnect/receive warnings**. Console not cleared.
- Existing Git global-ignore permission warning was reported, not repaired. GitHub Issue/GDD updates and Git writes were prohibited; none were performed.

## Room translations and preserved interiors

| Room root | Final world position | Clear X bounds | Clear Z bounds | Clear height |
|---|---|---|---|---|
| OpenOfficeArea | (0,0,0), unchanged | [-6,6] | [-5,5] | 3 |
| MeetingRoom | (0,0,2.2) | [-4,4] | [7.4,13.4] | 3 |
| ServerRoom | (-2.2,0,0) | [-14.4,-8.4] | [-3,5] | 3 |

Only these two existing root positions changed. Existing children retain their local positions, rotations, scales, identities, components and references. Furniture, equipment and existing lights translate with their room roots. Existing Open Office, Player, camera, gameplay items, UI and inspection-stage world positions remain fixed.

All existing room dimensions are preserved: Open Office 12 x 10 x 3 m; Meeting Room 8 x 6 x 3 m; Server Room 6 x 8 x 3 m. Server Room retains all 15 racks and both 1.5 m aisles. There is no northern rack crossover or outer rear aisle; these remain intentionally absent.

## Constructed geometry

All positions are world-space centres, dimensions are world X/Y/Z, rotations are zero. Every primitive has one enabled, non-trigger BoxCollider. Floor tops are Y=0 and ceiling undersides Y=3. Table entries reference existing materials under `Assets/Materials/OpenOffice/`.

| Object | World position | Dimensions | Material |
|---|---|---|---|
| `Corridors/NorthCorridor/Structure/Floor` | (0, -0.1, 6.2) | (12.4, 0.2, 2) | Floor.mat |
| `Corridors/NorthCorridor/Structure/Ceiling` | (0, 3.1, 6.2) | (12.4, 0.2, 2) | Ceiling.mat |
| `Corridors/WestCorridor/Structure/Floor` | (-7.2, -0.1, 0) | (2, 0.2, 10.4) | Floor.mat |
| `Corridors/WestCorridor/Structure/Ceiling` | (-7.2, 3.1, 0) | (2, 0.2, 10.4) | Ceiling.mat |
| `Corridors/NorthwestCorner/Structure/Floor` | (-7.2, -0.1, 6.2) | (2, 0.2, 2) | Floor.mat |
| `Corridors/NorthwestCorner/Structure/Ceiling` | (-7.2, 3.1, 6.2) | (2, 0.2, 2) | Ceiling.mat |
| `Corridors/NorthCorridor/Structure/NorthWall_East` | (5.2, 1.5, 7.3) | (2, 3, 0.2) | Wall.mat |
| `Corridors/NorthwestCorner/Structure/NorthWall_West` | (-6.3, 1.5, 7.3) | (4.2, 3, 0.2) | Wall.mat |
| `Corridors/NorthwestCorner/Structure/WestWall` | (-8.3, 1.5, 6.1) | (0.2, 3, 2.2) | Wall.mat |
| `Corridors/WestCorridor/Structure/WestWall_South` | (-8.3, 1.5, -4.1) | (0.2, 3, 2.2) | Wall.mat |
| `MeetingRoom/Structure/Walls/South/WestSegment` | (-2.4, 1.5, 7.3) | (3.6, 3, 0.2) | Wall.mat |
| `MeetingRoom/Structure/Walls/South/EastSegment` | (2.4, 1.5, 7.3) | (3.6, 3, 0.2) | Wall.mat |
| `MeetingRoom/Structure/Walls/South/Header` | (0, 2.6, 7.3) | (1.2, 0.8, 0.2) | Wall.mat |
| `ServerRoom/Structure/Walls/East/SouthSegment` | (-8.3, 1.5, -1.8) | (0.2, 3, 2.4) | Wall.mat |
| `ServerRoom/Structure/Walls/East/NorthSegment` | (-8.3, 1.5, 2.8) | (0.2, 3, 4.4) | Wall.mat |
| `ServerRoom/Structure/Walls/East/Header` | (-8.3, 2.6, 0) | (0.2, 0.8, 1.2) | Wall.mat |
| `MeetingRoom/Structure/SouthThresholdFloor` | (0, -0.1, 7.3) | (8.4, 0.2, 0.2) | Floor.mat |
| `ServerRoom/Structure/EastThresholdFloor` | (-8.3, -0.1, 1) | (0.2, 0.2, 8.4) | Floor.mat |
| `MeetingRoom/Structure/SouthThresholdCeiling` | (0, 3.1, 7.3) | (8.4, 0.2, 0.2) | Ceiling.mat |
| `ServerRoom/Structure/EastThresholdCeiling` | (-8.3, 3.1, 1) | (0.2, 0.2, 8.4) | Ceiling.mat |
| `Corridors/TEMP_CorridorSafety/EastEndBarrier` | (6.3, 1.5, 6.3) | (0.2, 3, 2.2) | TemporaryBarrier.mat |
| `Corridors/TEMP_CorridorSafety/SouthEndBarrier` | (-7.3, 1.5, -5.3) | (2.2, 3, 0.2) | TemporaryBarrier.mat |
| `Corridors/NorthCorridor/Structure/NorthEastPerimeterFloor` | (5.2, -0.1, 7.3) | (2, 0.2, 0.2) | Floor.mat |
| `Corridors/NorthCorridor/Structure/NorthEastPerimeterCeiling` | (5.2, 3.1, 7.3) | (2, 0.2, 0.2) | Ceiling.mat |
| `Corridors/NorthwestCorner/Structure/NorthPerimeterFloor` | (-6.3, -0.1, 7.3) | (4.2, 0.2, 0.2) | Floor.mat |
| `Corridors/NorthwestCorner/Structure/NorthPerimeterCeiling` | (-6.3, 3.1, 7.3) | (4.2, 0.2, 0.2) | Ceiling.mat |
| `Corridors/NorthwestCorner/Structure/WestPerimeterFloor` | (-8.3, -0.1, 6.2) | (0.2, 0.2, 2) | Floor.mat |
| `Corridors/NorthwestCorner/Structure/WestPerimeterCeiling` | (-8.3, 3.1, 6.2) | (0.2, 0.2, 2) | Ceiling.mat |
| `Corridors/WestCorridor/Structure/SouthWestPerimeterFloor` | (-8.3, -0.1, -4.2) | (0.2, 0.2, 2) | Floor.mat |
| `Corridors/WestCorridor/Structure/SouthWestPerimeterCeiling` | (-8.3, 3.1, -4.2) | (0.2, 0.2, 2) | Ceiling.mat |
| `Corridors/TEMP_CorridorSafety/EastBarrierPerimeterFloor` | (6.3, -0.1, 6.3) | (0.2, 0.2, 2.2) | Floor.mat |
| `Corridors/TEMP_CorridorSafety/EastBarrierPerimeterCeiling` | (6.3, 3.1, 6.3) | (0.2, 0.2, 2.2) | Ceiling.mat |
| `Corridors/TEMP_CorridorSafety/SouthBarrierPerimeterFloor` | (-7.3, -0.1, -5.3) | (2.2, 0.2, 0.2) | Floor.mat |
| `Corridors/TEMP_CorridorSafety/SouthBarrierPerimeterCeiling` | (-7.3, 3.1, -5.3) | (2.2, 0.2, 0.2) | Ceiling.mat |

The three non-overlapping clear floor rectangles are:

| Section | Clear X bounds | Clear Z bounds | Clear area |
|---|---|---|---|
| North | [-6.2,6.2] | [5.2,7.2] | 24.8 m2 |
| West | [-8.2,-6.2] | [-5.2,5.2] | 20.8 m2 |
| Northwest corner | [-8.2,-6.2] | [5.2,7.2] | 4 m2 |
| Total | | | **49.6 m2**, excluding doorway passages |

Four openings remain clear, 1.2 m wide and 2.2 m high: Open Office north at Z=5.1 and Meeting Room south at Z=7.3, centred X=0; Open Office west at X=-6.1 and Server Room east at X=-8.3, centred Z=0. No door/lock/gameplay component was added.

## Hierarchy and instance scope

```text
OfficeEnvironment
  OpenOfficeArea [unchanged]
  MeetingRoom [root translated]
    Structure
      Walls
        South [new]
          WestSegment / EastSegment / Header
      SouthThresholdFloor / SouthThresholdCeiling
    [all existing children retained]
  ServerRoom [root translated]
    Structure
      Walls
        East [new]
          SouthSegment / NorthSegment / Header
      EastThresholdFloor / EastThresholdCeiling
    [all existing children retained]
  Corridors
    NorthCorridor
      Structure
        Floor / Ceiling / NorthWall_East
        NorthEastPerimeterFloor / NorthEastPerimeterCeiling
      Lighting
        TEMP_PointLight_01 / TEMP_PointLight_02
    WestCorridor
      Structure
        Floor / Ceiling / WestWall_South
        SouthWestPerimeterFloor / SouthWestPerimeterCeiling
      Lighting
        TEMP_PointLight_01 / TEMP_PointLight_02
    NorthwestCorner
      Structure
        Floor / Ceiling / NorthWall_West / WestWall
        NorthPerimeterFloor / NorthPerimeterCeiling
        WestPerimeterFloor / WestPerimeterCeiling
      Lighting
        TEMP_PointLight_01
    TEMP_CorridorSafety
      EastEndBarrier / SouthEndBarrier
      EastBarrierPerimeterFloor / EastBarrierPerimeterCeiling
      SouthBarrierPerimeterFloor / SouthBarrierPerimeterCeiling
```

Actual additions: **34 structural primitives + five lights + 13 organisational objects = 52 GameObjects**. Added components: 52 Transforms, 34 MeshFilters, 34 MeshRenderers, 34 BoxColliders, five Lights and five UniversalAdditionalLightData components: **164 total**.

Final scene counts: **541 GameObjects, 1,642 components, 11 scene roots**. No existing object or component was removed or reparented.

## Lighting and material configuration

| Light | World position | Range |
|---|---|---|
| North 01 | (-3.1,2.7,6.2) | 4.3 |
| North 02 | (3.1,2.7,6.2) | 4.3 |
| West 01 | (-7.2,2.7,-2.6) | 4 |
| West 02 | (-7.2,2.7,2.6) | 4 |
| Northwest corner | (-7.2,2.7,6.2) | 4 |

All five are white Point Lights, intensity 2, realtime, shadows disabled, culling mask -257 (InspectionModel layer 8 excluded), with URP additional-light-data components. Existing room lights retain every serialized setting and relative transform.

Reused exactly Floor.mat, Wall.mat, Ceiling.mat and TemporaryBarrier.mat. No material, shader, URP asset or renderer feature was modified. The active PC_RPAsset four-additional-lights-per-object limit remains unchanged; SSAO and CollectibleOutline remain configured as before.

## Actual Edit Mode verification

Verification used the constructed live Unity scene through Unity MCP, not only the planning model. Physics.SyncTransforms synchronized colliders after root translations and construction. No Play Mode or CharacterController.Move execution occurred.

### Geometry, configuration and collision

- **2,121 geometry/configuration assertions passed**, zero failures.
- Checked all 34 primitive world positions, rotations, collider and renderer bounds/alignment, material assignments, collider settings and four-component construction.
- Measured actual room bounds, 3 m room heights, both root translations, all four doorway widths/heights and 49.6 m2 clear floor area.
- Compared **1,770 pairs across 60 structural BoxColliders**; zero positive-volume penetrations beyond 0.0001 m.
- **24 footprint coverage checks** passed. These comprise 22 actual floor/ceiling contact checks plus two header-to-floor projection checks; headers are 2.2 m above the floor and are not claimed to contact it.
- Checked 15 rack roots, three row envelopes and both 1.5 m Server Room aisle gaps.
- **6,188 Physics.ComputePenetration checks** between new colliders and existing colliders found no penetration beyond 0.0001 m.
- **1,240 floor grid rays and 1,240 ceiling grid rays**, at 0.2 m grid spacing, confirmed corridor surface coverage.
- **1,572 horizontal enclosure rays**, at heights 0.05, 1.4 and 2.95 m, hit structural enclosure toward outer/inner corridor boundaries; intended room openings lead into enclosed rooms.
- **15 rack-front rays** hit their corresponding rack colliders from accessible aisle positions.
- **15 new-light assertions** confirmed positions, settings and URP components.
- The supplemental verification suite contains **10,270 checks**, including its penetration, ray and light checks above. These subsets must not be added to the suite total again.
- **Four endpoint capsule casts**, using both radii at both endpoints, hit the expected barriers first.

Geometry uses 0.0001 m dimensional/penetration tolerance, with 0.0002 tolerance for footprint-area comparisons and ray-hit height checks. Face contacts are accepted; positive-volume intrusion beyond tolerance is not.

### Static navigation queries

Executed **8,602 OverlapCapsule queries** over **4,301 sampled route positions on 32 routes**, split equally between the player radius 0.3 m and nominal radius 0.5999 m. Sample positions include repeated vertices/return visits; they are not 4,301 distinct coordinates. All queries passed.

Capsule height is 1.8 m with 0.01 m bottom clearance. The nominal radius represents a 1.2 m-diameter planning envelope with 0.0001 m radial contact allowance. Its exact-width doorway fit is not described as extra clearance. The player's own collider and InspectionModel layer are excluded; triggers are ignored. Straight sample spacing is at most 0.05 m. Two dead-end turn routes use 65 arc vertices each, with segment endpoint sampling counted in the totals.

At every route sample, a downward support ray hit Y=0 and an upward ceiling ray hit Y=3: **4,301 support rays + 4,301 ceiling rays**, zero failures.

| Route | Sample visits | Failures |
|---|---:|---:|
| NorthCorridor | 255 | 0 |
| WestCorridor | 215 | 0 |
| NorthwestTurn | 66 | 0 |
| OpenOfficeToMeeting | 87 | 0 |
| OpenOfficeToServer | 89 | 0 |
| MeetingToServer | 348 | 0 |
| OfficeCorridorLoopForward | 540 | 0 |
| OfficeCorridorLoopReverse | 540 | 0 |
| MeetingTableLoop | 420 | 0 |
| WestAisleEntryAndReturn | 252 | 0 |
| WestAisleDeadEndTurn | 128 | 0 |
| EastAisleEntryAndReturn | 202 | 0 |
| EastAisleDeadEndTurn | 128 | 0 |
| UPSApproach | 117 | 0 |
| CoolingApproach | 110 | 0 |
| CabinetApproach | 90 | 0 |
| MaintenanceApproach | 30 | 0 |
| ServerRack_01FrontApproach | 23 | 0 |
| ServerRack_02FrontApproach | 35 | 0 |
| ServerRack_03FrontApproach | 47 | 0 |
| ServerRack_04FrontApproach | 59 | 0 |
| ServerRack_09FrontApproach | 64 | 0 |
| ServerRack_10FrontApproach | 23 | 0 |
| ServerRack_11FrontApproach | 35 | 0 |
| ServerRack_12FrontApproach | 47 | 0 |
| ServerRack_13FrontApproach | 59 | 0 |
| ServerRack_14FrontApproach | 64 | 0 |
| ServerRack_05FrontApproach | 23 | 0 |
| ServerRack_06FrontApproach | 35 | 0 |
| ServerRack_07FrontApproach | 47 | 0 |
| ServerRack_08FrontApproach | 59 | 0 |
| ServerRack_15FrontApproach | 64 | 0 |

Total successful raycasts across navigation and supplemental suites: **12,669** (8,602 route support/ceiling rays + 2,480 grid rays + 1,572 enclosure rays + 15 rack-front rays). Capsule casts are the separate four-query total, not included in the ray count.

These checks establish sampled static clearance/support. They do not independently demonstrate dynamic sliding, diagonal CharacterController traversal, subjective comfort, rendered brightness, runtime performance or complete manual gameplay regression.

## Preservation and serialized-reference audit

Post-save comparison against all **12,657 baseline records** found zero missing records and zero unexpected differences, allowing only the two approved root translations. All 32 required gameplay references still target their original objects/assets. Existing GameObject identities, component identities, parents and all existing child local transforms remain unchanged. World transforms for the 284 existing objects in the two moved rooms changed only by the corresponding translation; other world transforms remain unchanged.

Only two existing serialized local-position values changed. Float32 storage of 2.2 m is compared with tolerance; it is not a design deviation. Existing component values and direct references match. Existing parent child lists receive the new children as authorised.

Player remains at (0,0,-4), radius 0.3, height 1.8, centre (0,0.9,0), skin width 0.08. PlayerCamera remains local (0,1.4,0), near 0.05, FOV 60. AccessCardObject and TestDocument are unmoved. Inventory, Hotbar, pickup, interaction, feedback, inspection and collectible outline component configurations remain intact.

Existing ExitDoor, Open Office east barrier and southern landing remain unchanged. All existing room lighting/material configuration is preserved. No existing scene root was changed or added.

Saved YAML audit: **1,976 blocks before, 2,192 after; 216 blocks added; zero removed**. Added blocks are 52 GameObjects plus 164 components. Exactly seven existing Transform blocks differ: the two room-root positions and five parent child lists (OfficeEnvironment, MeetingRoom Structure/Walls and ServerRoom Structure/Walls). No other existing scene blocks changed. Unity reordered some blocks during saving; the larger textual diff does not represent deletion/recreation of existing objects.

Protected-file hash audit after scene save found only OfficeScene changed among the captured 236 files. Earlier room logs, scripts, prefabs, materials, packages, settings and rendering assets remained unchanged.

## Editor, Console and tool observations

OfficeScene saved successfully and reported isDirty=false. Unity returned idle Edit Mode, compilation complete, no pending domain reload, no active tests and no asset update in progress. An automatic compile/domain reload completed around the save; no project C# script was edited and no Play Mode was entered.

Final Console checkpoint: **0 errors, 3 MCP transport warnings**. Two were present at baseline; one additional warning reported WebSocket not initialised. MCP reconnected/continued successfully and returned the saved snapshot and final Editor state. Console was never cleared.

One verification request exceeded the MCP 50,000-character code limit before execution. It was compacted into a data-driven in-memory query and executed successfully; no verification gap or project script change resulted.

The known `m_ProjectSettingFolderPath: URPDefaultResources` field did **not** regenerate at the final audit; URPProjectSettings.asset has no diff. Its recurrence remains possible in later Unity operations. No settings cleanup was performed in this task.

Git still reports inability to read the global ignore file under the user profile. The warning was not repaired. No subagents, GDD/Issue updates, staging, commit, push or other Git writes were performed.

Final `git diff --check` reports nine trailing-space lines in Unity-generated empty `m_Name: ` fields. They were left as serialized by Unity rather than rewriting the scene text. The complete existing-block diff confirms only the two approved root positions and five parent child lists changed; no unrelated scene records were modified.

## AI assistance and file scope

Codex read the workflow, captured preservation evidence, used Unity MCP to translate room roots and construct approved primitives/lights, queried the live geometry/references and static physics, audited scene YAML and protected hashes, and wrote this log.

Intentional files:
- Modified `Assets/Scenes/OfficeScene.unity`.
- Added `docs/development-logs/l-shaped-corridor-blockout.md`.

No prior development log was edited and no asset was added/deleted. No unapproved dimension or design adjustment was made.

## Original manual QA handoff checklist (historical)

All cases below are **Not Run by Codex**. No manual outcome or runtime profiling result is invented.

1. Assess corridor width, visual scale and spatial separation between rooms.
2. Cross Open Office north doorway forward, backward and diagonally.
3. Cross Meeting Room south doorway in the same ways, without interaction/unlocking.
4. Cross Open Office west doorway in the same ways.
5. Cross Server Room east doorway in the same ways.
6. Walk the complete North and West corridors and turn naturally through the northwest corner.
7. Complete the Open Office-North Corridor-West Corridor-Open Office loop in both directions.
8. Travel Meeting Room to Server Room and back through the corridors.
9. Check doorway thresholds, floor seams, wall corners and ceiling seams for visible gaps or snagging.
10. Test both corridor endpoint barriers; confirm no escape or fall.
11. Confirm unchanged Open Office east barrier and southern landing safety.
12. Walk around the Meeting Room table and behind chairs; confirm layout and furniture scale remain unchanged.
13. Traverse both Server Room 1.5 m aisles, turn at the dead ends and approach northernmost rack fronts.
14. Approach the UPS, cooling unit, cabinet and maintenance workstation.
15. Rotate the camera close to all new walls/corners; check clipping.
16. Assess North midpoint/eastern endpoint, West southern endpoint, corner and doorway illumination.
17. Check lighting transitions, shadowless leakage, sudden light-selection changes and subjective smoothness. Formal profiling remains separate.
18. Pick up Access Card and TestDocument; confirm Hotbar scrolling, empty-slot selection and selected state.
19. Test F inspection, full-panel drag rotation, Esc exit, input/cursor restoration and inspection lighting isolation.
20. Unlock ExitDoor with the selected Access Card; confirm feedback, item preservation and southern landing access.
21. Confirm collectible outlines respect opaque occlusion and inspection models remain outline-free.
22. Check runtime Console errors and warnings, distinguishing existing MCP transport warnings.

At this original handoff, user-performed manual QA was pending. Subsequent aggregate manual results and the endpoint correction are recorded below; original construction and verification evidence above remains historical.


## Endpoint alignment correction and deferred lighting issues

### Manual QA and diagnostic history

The user reported the original corridor manual QA result as **21 PASS, 1 FAIL, 0 NOT RUN**. These are user-performed Play Mode results, not tests executed by Codex or a QA subagent. The supplied aggregate is preserved; individual outcomes are not invented or mapped onto the historical checklist.

**D02 lighting findings remain unresolved/deferred.** The task refers to two remaining lighting issues and to user-performed lighting isolation and shadow-bias diagnostic tests. Detailed visual symptoms and individual diagnostic measurements/outcomes were not supplied in the current task or existing log, so no additional causal conclusion or numerical diagnostic result is asserted here.

The confirmed diagnostic residue was Directional Light's URP additional-light-data `m_UsePipelineSettings=false`, versus `true` in the saved implementation baseline. The user explicitly identified this as a residual manual diagnostic change and authorised restoring only that property. Full snapshot comparison before restoration confirmed it was the sole difference; the scene was dirty. Directional Light's Light component, including Bias, Normal Bias and Shadow Type, already matched the saved baseline.

Restored only `m_UsePipelineSettings=true`, verified zero remaining baseline differences, then saved and confirmed isDirty=false before proceeding. All lighting configuration matches the saved implementation. No corridor shadows were enabled, and no light position, range, intensity, bias, normal bias, shadow type or URP setting was otherwise changed.

The user approved deferring **all remaining lighting issues to the Lighting & Atmosphere development stage**. D02 remains a historical failure/deferred known issue; it is not reclassified as PASS. Endpoint geometry verification does not prove either lighting issue resolved. No new lighting correction or lighting diagnostic experiment was performed by Codex.

### Architectural decision and baseline

The user approved aligning both temporary endpoint outer faces with the existing Open Office exterior: east X=6.2 and south Z=-5.2. Wall thickness now extends inward. The arrangement remains temporary and can be revisited during future Manager's Office/Reception development; neither room was designed or implemented here.

Repeated baseline verification passed after the authorised restoration: branch `feature/office-level-blockout`, expected uncommitted OfficeScene and this log preserved, saved OfficeScene active, Unity MCP connected and idle in Edit Mode, no pending compile/domain reload. Captured all 541 GameObjects, 1,642 components, 11 roots, persistent identities, parents, transforms, serialized values/direct references and protected file hashes. All 32 required gameplay references remained assigned to original targets.

Actual pre-change collider and renderer bounds were inspected for all 16 affected objects and matched the original approved corridor construction. Open Office's measured exterior limits are X=6.2 and Z=-5.2.

### Exact incremental changes

All rotations, identities, names, parents, collider component values and material references are unchanged. The following are the only **16 Transform** changes:

| Object under OfficeEnvironment | Old world centre | New world centre | Old dimensions | New dimensions |
|---|---|---|---|---|
| `Corridors/NorthCorridor/Structure/Floor` | (0, -0.1, 6.2) | (-0.1, -0.1, 6.2) | (12.4, 0.2, 2) | (12.2, 0.2, 2) |
| `Corridors/NorthCorridor/Structure/Ceiling` | (0, 3.1, 6.2) | (-0.1, 3.1, 6.2) | (12.4, 0.2, 2) | (12.2, 0.2, 2) |
| `Corridors/NorthCorridor/Structure/NorthWall_East` | (5.2, 1.5, 7.3) | (5.1, 1.5, 7.3) | (2, 3, 0.2) | (1.8, 3, 0.2) |
| `Corridors/NorthCorridor/Structure/NorthEastPerimeterFloor` | (5.2, -0.1, 7.3) | (5.1, -0.1, 7.3) | (2, 0.2, 0.2) | (1.8, 0.2, 0.2) |
| `Corridors/NorthCorridor/Structure/NorthEastPerimeterCeiling` | (5.2, 3.1, 7.3) | (5.1, 3.1, 7.3) | (2, 0.2, 0.2) | (1.8, 0.2, 0.2) |
| `Corridors/TEMP_CorridorSafety/EastEndBarrier` | (6.3, 1.5, 6.3) | (6.1, 1.5, 6.3) | (0.2, 3, 2.2) | (0.2, 3, 2.2) |
| `Corridors/TEMP_CorridorSafety/EastBarrierPerimeterFloor` | (6.3, -0.1, 6.3) | (6.1, -0.1, 6.3) | (0.2, 0.2, 2.2) | (0.2, 0.2, 2.2) |
| `Corridors/TEMP_CorridorSafety/EastBarrierPerimeterCeiling` | (6.3, 3.1, 6.3) | (6.1, 3.1, 6.3) | (0.2, 0.2, 2.2) | (0.2, 0.2, 2.2) |
| `Corridors/WestCorridor/Structure/Floor` | (-7.2, -0.1, 0) | (-7.2, -0.1, 0.1) | (2, 0.2, 10.4) | (2, 0.2, 10.2) |
| `Corridors/WestCorridor/Structure/Ceiling` | (-7.2, 3.1, 0) | (-7.2, 3.1, 0.1) | (2, 0.2, 10.4) | (2, 0.2, 10.2) |
| `Corridors/WestCorridor/Structure/WestWall_South` | (-8.3, 1.5, -4.1) | (-8.3, 1.5, -4) | (0.2, 3, 2.2) | (0.2, 3, 2) |
| `Corridors/WestCorridor/Structure/SouthWestPerimeterFloor` | (-8.3, -0.1, -4.2) | (-8.3, -0.1, -4.1) | (0.2, 0.2, 2) | (0.2, 0.2, 1.8) |
| `Corridors/WestCorridor/Structure/SouthWestPerimeterCeiling` | (-8.3, 3.1, -4.2) | (-8.3, 3.1, -4.1) | (0.2, 0.2, 2) | (0.2, 0.2, 1.8) |
| `Corridors/TEMP_CorridorSafety/SouthEndBarrier` | (-7.3, 1.5, -5.3) | (-7.3, 1.5, -5.1) | (2.2, 3, 0.2) | (2.2, 3, 0.2) |
| `Corridors/TEMP_CorridorSafety/SouthBarrierPerimeterFloor` | (-7.3, -0.1, -5.3) | (-7.3, -0.1, -5.1) | (2.2, 0.2, 0.2) | (2.2, 0.2, 0.2) |
| `Corridors/TEMP_CorridorSafety/SouthBarrierPerimeterCeiling` | (-7.3, 3.1, -5.3) | (-7.3, 3.1, -5.1) | (2.2, 0.2, 0.2) | (2.2, 0.2, 0.2) |

No object, component or asset was added, removed or replaced. The existing endpoint perimeter floor/ceiling strips were retained and repositioned to support the inward barriers. Adjacent exterior wall segments and their supporting strips were shortened so their full-face junctions meet the new barrier positions without positive-volume overlap or duplicate exposed coplanar surfaces.

### Current measured configuration

| Section | Clear X bounds | Clear Z bounds | Clear dimensions | Area |
|---|---|---|---|---|
| North Corridor | [-6.2,6.0] | [5.2,7.2] | 12.2 x 2 m | 24.4 m2 |
| West Corridor | [-8.2,-6.2] | [-5.0,5.2] | 2 x 10.2 m | 20.4 m2 |
| Northwest Corner | [-8.2,-6.2] | [5.2,7.2] | 2 x 2 m | 4 m2 |
| Total | | | Clear height 3 m | **48.8 m2**, excluding doorways |

EastEndBarrier renderer and collider bounds: X [6.0,6.2], Y [0,3], Z [5.2,7.4]. SouthEndBarrier bounds: X [-8.4,-6.2], Y [0,3], Z [-5.2,-5.0]. No corridor structure protrudes beyond X=6.2 or Z=-5.2.

All three room positions/dimensions, four aligned 1.2 x 2.2 m doorways, the complete corridor loop, furniture/equipment and lighting remain unchanged. All 15 server racks and both 1.5 m rack aisles remain intact.

### Actual Edit Mode verification for this correction

Live Unity mesh/collider bounds and physics were checked after Physics.SyncTransforms. These are constructed-scene observations, not merely planning calculations and not runtime CharacterController behaviour.

- **12,793 geometry/configuration assertions passed**, zero failures. This includes 1,770 structural pair comparisons across 60 BoxColliders and **10,620 same-facing coplanar face-pair checks**; no unintended positive-volume structural overlap or same-facing positive-area coplanar duplication was found.
- Checked all 34 structural primitive positions, renderer/collider envelopes and alignment, materials and component settings; measured endpoint alignment, reduced corridor dimensions and 48.8 m2 area.
- All 24 footprint coverage checks passed (22 actual floor/ceiling contacts and two header-to-floor projections). All twelve perimeter strips and four doorway threshold extensions remain correctly connected.
- **6,188 Physics.ComputePenetration checks** between corridor/threshold construction and existing colliders passed.
- **1,220 floor-grid rays + 1,220 ceiling-grid rays** at 0.2 m spacing confirmed continuous corridor surfaces.
- **1,554 horizontal structural enclosure rays**, sampled at Y=0.05, 1.4 and 2.95, found no sampled external escape route.
- **15 rack-front rays** and 15 new-corridor-light configuration assertions passed. This supplemental suite totals **10,212 checks**, including its collision/ray/light subsets.
- **8,586 OverlapCapsule queries** passed at **4,293 sampled route visits on 32 routes**, using radius 0.3 and nominal radius 0.5999, height 1.8 and 0.01 bottom clearance. Maximum straight sampling spacing is 0.05 m. Counts include repeated vertices/return visits.
- The nominal radius retains 0.0001 m radial contact allowance at exact-width 1.2 m doorways. Own-player collider, InspectionModel and triggers are excluded as before.
- North endpoint route now ends at X=5.3; south endpoint route starts at Z=-4.3, allowing both capsule envelopes to approach the shifted barriers safely.
- All four doorways and approaches, both corridors, corner turn, loop in both directions, room-to-room connections, Meeting Room circulation, both server aisles/dead-end turns, 15 rack approaches and supporting equipment approaches passed.
- Every route visit also passed a support and ceiling ray: **4,293 + 4,293**. Total rays for this verification are **12,595** including route, grid, enclosure and rack-front checks.
- **Four endpoint capsule casts** hit the correct barriers first: approximately 0.4 m travel for player radius and 0.1001 m for nominal radius at each endpoint.

Dimensional/penetration/contact tolerance is 0.0001 m; footprint-area and ray-height checks use 0.0002 tolerance. Intentional opposite-facing face contacts are retained; same-facing overlapping surface area is rejected. Physics queries and bounds checks cannot establish visual lighting quality or dynamic movement comfort.

### Preservation, save and scope audit

Comparison of **14,006 existing snapshot records** found only the 16 authorised Transform records and their 16 world-transform records changed. The other **13,974 records** are unchanged. No missing identity, component, parent or serialized reference was found. All 32 required gameplay references match their original targets.

Scene YAML remains **2,192 blocks**, with the identical complete ID set. Exactly the 16 approved Transform blocks changed; the aggregate hash of every other saved scene block is identical. No child list, object identity, component configuration, room root or lighting record changed relative to the restored saved baseline.

Final checkpoint: OfficeScene is active and isDirty=false; Unity is idle in Edit Mode, not compiling and has no pending domain reload. Console reports **0 errors and 1 warning**, an MCP WebSocket-not-initialised transport warning; live MCP queries succeeded. Console was not cleared. The known URP serialization field did not regenerate and URPProjectSettings.asset has no diff. Git still reports the existing global-ignore permission warning; no repair was attempted. Final working tree contains only modified OfficeScene and the existing untracked corridor development log. No Git writes were performed.

Protected file hashes before/after geometry work found only OfficeScene changed. This log is the only additional intentional file edit. Previous room logs, gameplay scripts, prefabs, materials, packages, project settings, URP/renderer assets and gameplay configurations are preserved.

OfficeScene saved successfully with isDirty=false and 11 roots. Unity returned idle Edit Mode, compilation complete and no pending domain reload. An automatic save-time compile/reload completed; no project scripts were edited or active tests run. No Play Mode or subagents were used.

One read-only bounds serialization query failed because Newtonsoft recursively serialized Unity Vector3.normalized. It was corrected to explicit coordinate arrays and rerun successfully before geometry changes. A local read-only YAML audit regex was also corrected before the baseline hash was captured. Neither diagnostic failure changed project files or left a verification gap.

### Post-correction manual retest handoff and limitations (historical)

The original **21 PASS / 1 FAIL / 0 NOT RUN** remains historical. This correction has not received user-performed runtime confirmation. Pending checks:

1. Inspect east endpoint alignment with Open Office's exterior, including floor, wall, ceiling and corners.
2. Repeat at the south endpoint; check for gaps, exposed edges, flicker or visible seams.
3. Approach both barriers forward, backward and diagonally; confirm collision and no escape/fall.
4. Walk both shortened corridors and turn through the northwest corner.
5. Complete the Open Office-North-West-Open Office loop in both directions and travel between all rooms.
6. Recheck all four doorway thresholds, room navigation and server rack/equipment access.
7. Confirm camera rotation and movement near the adjusted endpoints feel correct.
8. Observe and record D02 lighting symptoms if desired, without treating geometry checks as lighting fixes. Lighting resolution remains deferred to Lighting & Atmosphere.
9. Smoke-test pickup, Hotbar, inspection/input restoration, Access Card unlocking, feedback, outlines and existing safety geometry.
10. Check runtime Console errors and warnings.

At this handoff, no individual QA result, lighting-resolution claim, runtime performance measurement or formal profiling result was inferred. Future endpoint changes may be approved when adjoining rooms are developed. Subsequent user results and stage acceptance are recorded below.


## Finalisation and Manual QA Round 2 - 26 November 2026

### User-performed manual Play Mode results

The user completed the following twelve tests after the approved endpoint alignment correction. These are **user-performed Play Mode observations**, not automated tests or runtime tests executed by Codex or a QA subagent.

| Test ID | Category | Acceptance criterion | User-reported result |
|---|---|---|---|
| A01 | Structural Alignment | North Corridor eastern endpoint aligns with the Open Office exterior wall | PASS |
| A02 | Structural Alignment | West Corridor southern endpoint aligns with the Open Office exterior wall | PASS |
| A03 | Structural Alignment | No visible geometric gaps between endpoint walls, floors and ceilings | PASS |
| B01 | Collision and Navigation | North Corridor endpoint barrier correctly blocks the player | PASS |
| B02 | Collision and Navigation | West Corridor endpoint barrier correctly blocks the player | PASS |
| B03 | Collision and Navigation | Both shortened corridor endpoints allow normal player approach and turning | PASS |
| B04 | Collision and Navigation | Clockwise navigation loop through the Open Office, Meeting Room, corridors and Server Room | PASS |
| B05 | Collision and Navigation | Reverse navigation loop without getting stuck or falling outside the level | PASS |
| C01 | Gameplay Regression | Item pickup and hotbar selection | PASS |
| C02 | Gameplay Regression | Entering and exiting item inspection | PASS |
| C03 | Gameplay Regression | Access Card successfully unlocks the temporary Exit Door | PASS |
| C04 | Gameplay Regression | Normal gameplay operation with no new gameplay errors observed | PASS |

Round 2 summary: **12 PASS / 0 FAIL / 0 NOT RUN. Additional Findings: None.** Lighting was explicitly excluded from this twelve-test round; the zero Not Run count applies only to the stated Round 2 scope.

The corridor blockout is **accepted for the current development stage, with deferred lighting issues**. Original Manual QA Round 1 remains **21 PASS / 1 FAIL / 0 NOT RUN**. Its failed D02 lighting test and existing diagnostic evidence are retained without reclassification. Neither lighting issue is claimed resolved.

All remaining lighting work is deferred by explicit user decision to the **Lighting & Atmosphere** development stage. This acceptance does not establish visual lighting correctness, formal performance profiling results or completion of lighting QA.

The endpoint arrangement remains temporary and may be revised during future Manager's Office and Reception development. Those rooms and any further design work are outside this finalisation.

### Fresh final read-only verification by Codex

- Read AGENTS.md; branch remains `feature/office-level-blockout`.
- Unity MCP connected and returned active, saved OfficeScene with isDirty=false and 11 roots.
- Unity is idle in Edit Mode, not compiling, with no pending domain reload, tests or asset update.
- Compared **14,006 captured snapshot records** against the completed endpoint-correction baseline: **zero unexpected differences**. Counts remain **541 GameObjects / 1,642 components**.
- All **32 required gameplay references** remain assigned to their original targets. Existing identities, parents, transforms, room layouts, gameplay systems and lighting components are unchanged after manual QA.
- Actual collider and renderer exterior limits remain east X=6.2 and south Z=-5.2.
- Actual floor dimensions remain North 12.2 x 2 m, West 2 x 10.2 m and corner 2 x 2 m; clear height 3 m and total clear area **48.8 m2**, excluding doorway passages.
- Protected-file hash inspection found no additional project changes beyond the existing corridor scene and this log. No unexpected project settings, URP assets, materials, scripts, prefabs or packages changed. URPProjectSettings.asset remains outside the modified-file scope.
- Current Console query returned **0 errors and 0 warnings**. Console was not cleared. Earlier transport-warning checkpoints remain historical evidence.
- No scene save or Unity state modification was needed during finalisation. No Play Mode, active tests, compilation request, subagents, GDD/Issue updates or Git writes were performed.
- The existing Git global-ignore permission warning persists and was not repaired. Previously documented Unity-generated empty-name trailing whitespace remains a serialization-format limitation; no unrelated scene text was rewritten.

The earlier construction/physics checks remain historical evidence; they were not rerun as new runtime tests. This finalisation edits **only this development log**.

### Final delivery scope and commit readiness

Complete working-tree scope:
- Modified `Assets/Scenes/OfficeScene.unity`.
- Added/untracked `docs/development-logs/l-shaped-corridor-blockout.md`.
- No deleted files or unexpected assets/settings.

The corridor changes are ready for the user's **manual Commit & Push**, with the lighting deferral explicitly accepted. Recommended commit message:

`feat: add L-shaped corridor greybox with aligned endpoints`

No staging, commit or push has been performed. Further work awaits user confirmation.



