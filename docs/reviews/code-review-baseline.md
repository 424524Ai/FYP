# Code Review — Initial Static Baseline

## Review snapshot

| Field | Value |
|---|---|
| Date | 2026-11-20 |
| Agent | Project-local `code-reviewer`, invoked once |
| Branch | `feature/item-use-system` |
| Reviewed commit | `e4af2f39bc6b6ad4b235978909d6e4505f3b326a` |
| Working tree | Clean before and after review |
| Previous baseline | None |
| Review mode | Static filesystem inspection only |

The commit remained unchanged throughout the review. The reviewed working tree therefore corresponds to the recorded commit.

**Implemented scope:** movement, mouse look, interaction, pickup, inventory, six-slot hotbar, collectible outlines, and 3D item inspection, including relevant serialized assets and configuration.

**Item Use System was excluded as planned functionality**, regardless of the branch name.

No fixes, report files, Unity operations, compilation, automated tests, QA assessment, or Git mutations were performed during the review. This report was subsequently saved with explicit user approval.

## Findings

### CR-001 — P1 High — Default build excludes the implemented gameplay scene

**Locations:** [EditorBuildSettings.asset:7](../../ProjectSettings/EditorBuildSettings.asset#L7), [SampleScene.unity:135](../../Assets/Scenes/SampleScene.unity#L135)

**Reproduction scenario**

1. Build the project using the committed Editor Build Settings scene list, without an external scene-list override.
2. Launch the resulting executable.

**Static evidence**

The only enabled build scene is `Assets/Scenes/SampleScene.unity`. Its object inventory contains the template camera, directional light, and global volume.

`OfficeScene.unity`, containing the player, pickup, inventory HUD, hotbar, and inspection setup, is absent from that list.

**Expected behavior:** the supported game build starts in the scene containing the implemented gameplay.

**Inferred behavior and impact:** the default build starts in the template scene without the implemented gameplay. This blocks delivery and gameplay verification through that build path.

**Confidence:** high for the serialized configuration. Executable behavior is inferred, not reproduced. An independently configured build profile or build script could override the scene list; no such override was established within the reviewed evidence.

**Minimal recommendation:** configure OfficeScene as the enabled startup scene for the intended build path.

**Required verification:** during a separately approved build run, confirm the effective scene list and verify the executable starts with movement, HUD, pickup, hotbar, and inspection available.

**No additional actionable correctness defects were confirmed in the reviewed implementation.**

## Coverage and affected dependencies

### Source coverage

All 13 project-owned C# scripts were reviewed:

- `PlayerMovement`, `PlayerLook`, `PlayerInteraction`, `IInteractable`, `TestInteractable`.
- `Inventory`, `InventoryUI`, `InventorySlotUI`, `ItemPickup`, `ItemData`.
- `ItemInspectionController`.
- `CollectibleOutlineTarget`, `CollectibleOutlineRendererFeature`.

The review traced:

- Unity lifecycle ordering and event subscription cleanup.
- Inventory capacity, selection wrapping, and pickup rejection before destruction.
- Inspection prefab validation, model creation/destruction, proportional scaling, and input/cursor restoration.
- Drag initiation and capture.
- Outline registration, camera filtering, Render Graph resource dependencies, and depth-mask/composite logic.

These are source observations, not runtime test passes.

### Asset and configuration coverage

Reviewed:

- Both outline shaders and their materials.
- OfficeScene’s gameplay, UI, camera, pickup, and inspection references.
- `InventorySlot.prefab`, `AccessCardInspection.prefab`, and relevant `Desk.prefab` structure.
- `AccessCard.asset`, inspection-prefab linkage, and icon reference metadata.
- `ItemInspection.renderTexture`.
- PC/Mobile pipeline and renderer assets, URP global settings, and profile reference inventory.
- SampleScene’s object inventory and references.
- Input actions and their relationship to direct keyboard/mouse polling.
- Approved project settings covering Unity version, input, build scenes, graphics, quality, layers, physics, and URP.
- Package manifest and lockfile.
- `AGENTS.md`, `README.md`, `docs/GDD.md`, and both agent configurations.

Static integrity checks found:

- No absent local fileID targets in the scoped scan of both scenes and all three prefabs.
- No duplicate GUIDs among 33 scoped metadata files.
- Package-owned GUID resolution remains unverified.

### Concise system/dependency map for QA

| System | Dependency path |
|---|---|
| Movement/look | Player → CharacterController, PlayerMovement, PlayerLook → PlayerCamera |
| Interaction/pickup | PlayerInteraction → camera raycast → collider-local IInteractable → ItemPickup → Inventory |
| Hotbar | Inventory events → InventoryUI → six InventorySlotUI instances → icon/name/highlight |
| Inspection display | Selected inventory item → ItemData inspection prefab → ModelPivot → InspectionCamera → RenderTexture → RawImage |
| Inspection input | Early controller Update → block movement/look/E/wheel and interaction UI → Esc → LateUpdate restoration |
| Inspection dragging | InspectionPanel drag rectangle → Canvas-aware screen hit-test → captured LMB drag → model pivot rotation |
| Outline | CollectibleOutlineTarget registry → PC Renderer Feature → visible-depth mask → depth-aware composite |
| Rendering isolation | InspectionModel layer 8 → inspection camera includes it; world cameras exclude it |
| Default build startup | EditorBuildSettings → SampleScene; OfficeScene omitted |

### Exclusions

Generated Unity directories, caches, logs, credentials, IDE/build artifacts, third-party/package implementation, and vendored TextMesh Pro implementation were excluded. Artwork quality was not assessed. Unused profiles received reference/inventory checks rather than visual evaluation.

The agent inherited the session’s tool permissions; read-only behavior was maintained through filesystem-read operations rather than a separately enforced read-only sandbox.

## Verification evidence

| Category | Status |
|---|---|
| Static source/configuration review | Completed within the stated scope |
| Serialized-reference checks | Completed within the scoped local assets |
| C# compilation | Not performed; prohibited for this review |
| Shader compilation/import | Not performed |
| Unity Console inspection | Not performed |
| Automated tests | Not run |
| Unity MCP / Play Mode | Not used |
| Manual gameplay/rendering | Not performed |
| Build/executable verification | Not performed |

No current automated test results were supplied within the reviewed evidence. Historical verification claims were not treated as fresh passes.

## Unresolved questions requiring separate verification

### Renderer-feature serialization

[PC_Renderer.asset:47](../../Assets/Settings/PC_Renderer.asset#L47) ends `m_RendererFeatureMap` with a literal lowercase `l`.

The validity of that scalar in this Unity serialization format was not established within the allowed evidence. **This is not a confirmed corruption finding.** An approved Unity inspection should check import messages and persistence of both Renderer Features.

### Effective URP version

[manifest.json:10](../../Packages/manifest.json#L10) requests URP `17.7.0`, while [packages-lock.json:139](../../Packages/packages-lock.json#L139) records builtin URP `17.6.0` and matching dependencies.

Builtin package resolution may explain this. No compatibility failure is asserted; record the effective Editor-resolved version during approved verification.

### Documentation drift

README/GDD describe the earlier Tab/modal inventory and planned inspection behavior. `AGENTS.md` and the explicitly approved implemented scope were used as the current contract. Planned pause or item-use behavior was not reported as missing functionality.

## Baseline finding updates

This establishes the initial Code Review baseline:

- **New:** CR-001.
- **Resolved, recurring, unchanged:** not applicable.
- **Baseline anchor:** `e4af2f39bc6b6ad4b235978909d6e4505f3b326a`, clean working tree.

This report was originally returned in chat and was subsequently saved at `docs/reviews/code-review-baseline.md` with explicit user approval. Saving the report does not advance the reviewed baseline anchor.

## Assessment and QA handoff

**CR-001 is the blocking finding for the committed default build path.** This static review does not establish runtime acceptance.

For a separately approved QA run:

1. Confirm the effective build startup scene and reproduce CR-001.
2. Check compilation, Console, shader imports, Renderer Feature persistence, and resolved package versions.
3. Verify movement, collision, look, crosshair, and E interaction.
4. Verify six persistent slots, empty-slot selection, wrapping, and safe full-inventory rejection.
5. Verify occupied/empty-slot inspection, panel-edge/corner dragging, outside-start rejection, and captured dragging beyond boundaries.
6. Verify Esc restoration, simultaneous F/E/wheel input, focus loss, and controller disable during inspection.
7. Verify exposed/partial/hidden outlines, removal after pickup, and no outline in the inspection preview.
8. Verify model proportions, framing, aspect fitting, lighting, and camera-layer isolation.

These are proposed checks only. QA was not launched and no verification operations were performed as part of this static review.
