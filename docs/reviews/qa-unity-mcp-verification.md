# Supplementary QA — Read-Only Unity Verification

## Scope and provenance

One authorized supplementary read-only verification was performed by the project-local `qa-tester` agent on 2026-11-20. Unity MCP was accessible. No baseline assessment was repeated.

Baseline reference: `e4af2f39bc6b6ad4b235978909d6e4505f3b326a`.

The current Git snapshot was intentionally not rechecked. Live observations describe the connected Unity Editor, not a newly verified commit snapshot.

This report was originally returned in chat and was subsequently saved with explicit user approval. Saving it does not advance the reviewed baseline SHA.

## Static baseline evidence

The supplementary verification reused:

- [Code Review baseline](code-review-baseline.md): source/configuration coverage, system/dependency map, and CR-001.
- [QA baseline](qa-baseline.md): acceptance criteria, test IDs, historical evidence, and outstanding verification.

CR-001 is the existing Code Review finding concerning OfficeScene's omission from the committed default build scene list. It is not a new QA defect.

Historical manual and scripted results in the baselines were not promoted to fresh gameplay passes. No full static baseline review was repeated; only minimal package manifest/lockfile context was compared with live package metadata.

## Live Unity MCP observations

Only documented non-mutating resource reads and query actions were used.

| Area | Live observation |
|---|---|
| Connection | Unity MCP connected to `FYP@24584046b956cde1` |
| Editor | Unity **6000.6.0f1**, Windows Editor / StandaloneWindows64, idle in **Edit Mode**, not compiling |
| Scene | **OfficeScene**, loaded and saved; no unsaved changes before or after inspection |
| Console | Initial and final queries returned **0 errors and warnings**, without truncation; Console was not cleared |
| Build scenes | Queried list contains only enabled **SampleScene**, index 0. OfficeScene is absent and reports build index −1 |
| Inventory/pickup | Player Inventory exists, with empty items and selection 0. AccessCardObject references Player Inventory and `AccessCard.asset` |
| Hotbar | InventoryUI references the Player controls, InventoryPanel, SlotContainer, full-message object and InventorySlot prefab |
| Inspection | Controller references inventory, hotbar, controls, interaction canvas, panel, camera, model pivot, viewport and name. **dragViewport targets InspectionPanel** |
| Preview rendering | RawImage references `ItemInspection.renderTexture`. InspectionCamera is disabled and has culling mask 256 |
| Preview prefab | AccessCardInspection hierarchy contains only a Transform root and MeshFilter/MeshRenderer child |
| Outline | Effective PC quality pipeline uses PC_RPAsset and PC_Renderer. **CollectibleOutline is enabled**, with both materials, cyan color and width 2 |
| URP version | Editor package query reports **17.6.0, BuiltIn**, agreeing with the lockfile; manifest requests 17.7.0 |

Console queries covered combined errors/warnings with count/page size 100, `truncated:false`, and no next cursor.

The package observation establishes the resolved version, **not compatibility or shader correctness**. Idle/noncompiling Editor state and an empty Console do not establish successful compilation.

## Evidence added to QA cases

Results distinguish configuration/query observations from functional gameplay acceptance.

| QA case | New evidence and result | Remaining verification |
|---|---|---|
| ENV-01 | **Pass — Console observation only:** current errors/warnings query returned zero. Resolved URP version identified | Compilation, shader-import success and compatibility remain **Not Run** |
| ENV-02 | **Fail — queried build-list configuration:** OfficeScene is absent, corroborating CR-001 | Executable startup remains **Blocked/unverified**; effective Build Profile overrides not established |
| INV-01 | Inventory and pickup references observed live | Gameplay remains **Not Run** |
| HOT-01/02 | Hotbar dependencies observed live; SlotContainer empty in Edit Mode | Six-slot generation, highlighting and cycling remain **Not Run** |
| INS-01–06 | Controller, full-panel drag region and preview dependencies observed live | Inspection gameplay remains **Not Run** |
| INS-02, OUT-03 | Visual-only inspection prefab structure and camera mask observed | Proportions, framing, lighting and absence of preview outline remain **Not Run** |
| OUT-01–03 | **Pass — configuration only:** enabled outline feature and assigned materials confirmed | Rendering acceptance remains **Not Run** |

The empty SlotContainer in Edit Mode is not classified as a defect; runtime slot generation was not executed.

This supplement adds live dependency/configuration evidence without promoting gameplay cases to Pass. No baseline-relative implementation change is established because the current Git snapshot was intentionally not inspected.

## Unresolved questions and capability limitations

- **CR-001 / Build Profile override:** no override was established. The available `manage_build` description did not clearly document a read-only profile-list operation, so it was not invoked. An override is neither assumed present nor declared nonexistent. CR-001 is supported for the queried build list; executable behavior is not confirmed.
- **ItemData icon/inspection-prefab assignments:** **Blocked by query capability**. Available asset info exposed identity only, and the component query rejected ScriptableObject assets.
- **InventorySlot prefab field assignments:** **Blocked by query capability**. Hierarchy/component types were available, but prefab hierarchy IDs were not readable through scene component resources.
- **InspectionCamera target RenderTexture:** not exposed in returned Camera properties; remains unverified. The RawImage texture assignment was separately observed.
- **Rendering and package behavior:** resolved metadata does not establish runtime compatibility or shader-import success.
- **Tool enforcement:** session permissions still exposed mutating tools; this run invoked only approved reads.

These capability gaps are not project defects. No independent behavioral QA defect was reproduced. CR-001 retains its Code Review identity.

## Unverified gameplay behavior

- Build/executable startup: **Blocked/unverified** through the queried default build path.
- Pickup success, item preservation, full-inventory rejection, six-slot generation and wheel selection: **Not Run**.
- Exposed, partially covered, fully hidden and revealed outline behavior: **Not Run**.
- Inspection entry, model appearance/proportions, lighting, preview outline isolation and drag behavior: **Not Run**.
- Panel-wide edge/corner initiation, outside-start rejection, captured dragging beyond boundaries and release: **Not Run**.
- F/Esc, movement/look/E/wheel blocking, cursor/HUD/selection restoration: **Not Run**.

## Verification evidence categories

- **Automated test-runner results:** none; zero tests executed.
- **Scripted Unity MCP checks:** documented resource reads and query actions only; Editor, scene, Console, build settings, pipeline, package and focused reference observations are recorded above.
- **Agent-observed gameplay:** none.
- **User-reported manual results:** historical baseline evidence only; no new manual results supplied.
- **Static assessment:** existing baseline reports reused, with minimal package manifest/lockfile comparison.

## Manual test handoff

A separately approved active run is still needed to:

1. Confirm the intended build/profile scene list, build and launch; expect OfficeScene gameplay at startup.
2. Verify six visible slots, empty-slot wheel cycling/wrapping, pickup and seventh-item rejection.
3. Open an occupied item with F; verify model, name, lighting, blocked controls and no preview outline.
4. Start dragging inside every panel edge/corner; expect rotation. Start outside then enter while held; expect no rotation. Drag outside after capture; expect rotation until release.
5. Press Esc; expect controls, cursor, HUD and selection restored.
6. Verify exposed, partially covered and fully covered collectible outlines.

These are proposed checks only, not authorization to execute them.

## Assessment

Supplementary read-only verification is complete. **CR-001 remains supported for the queried build-settings configuration**, with executable confirmation pending. Live outline configuration and Editor-resolved URP version are established.

Gameplay acceptance, final panel-wide dragging verification and unresolved asset-property checks remain pending.

No Unity state changes, compilation, tests, Play Mode, Git operations, nested CLI processes or fixes occurred during the verification. No baseline report was modified. This supplementary report was subsequently saved with explicit approval.
