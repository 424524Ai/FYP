# QA Assessment — Initial Static Baseline

## Snapshot and authorization

| Field | Value |
|---|---|
| Date | 2026-11-20 |
| Agent | Project-local `qa-tester`, invoked once |
| Baseline commit | `e4af2f39bc6b6ad4b235978909d6e4505f3b326a` |
| Branch | `feature/item-use-system` |
| Working tree | No tracked modifications; untracked `docs/reviews/` before and after assessment |
| Code Review reference | [code-review-baseline.md](code-review-baseline.md) |
| Mode | Static assessment only |
| Report provenance | Originally returned in chat; subsequently saved with explicit user approval |

The commit and working-tree status remained unchanged during the assessment. The saved Code Review report was an **untracked supplemental artifact**, not part of the baseline commit. Saving this QA report does not advance the baseline commit.

Requirements came from `AGENTS.md`, the approved implemented scope, and historical user reports. Earlier README/GDD descriptions of the modal inventory were not treated as the current acceptance contract.

**Included:** movement/look, interaction/pickup, inventory, six-slot hotbar, collectible outlines, and 3D inspection.

**Excluded:** planned Item Use, generated directories, caches, logs, credentials, build/IDE artifacts, and third-party implementation.

The agent reused the Code Review dependency map and inspected additional source only for acceptance criteria, test coverage, and behavioral boundaries. It performed no Git commands, Unity operations, compilation, tests, fixes, or writes. Session permissions were inherited; a separate read-only sandbox was not enforced.

## Requirement coverage

Statuses apply to **this snapshot and assessment**:

- **Pass:** supported by actual evidence for the stated activity.
- **Fail:** an executed check demonstrated unmet criteria.
- **Blocked:** a prerequisite prevents acceptance.
- **Not Run:** execution was not performed.

Static inspection is not a gameplay pass.

| Test ID | Functional acceptance criteria | Required verification | Result | Evidence/limitation |
|---|---|---|---|---|
| ENV-01 | C# and shader imports compile without relevant errors; Console is clear of relevant failures | Approved Editor checks | **Not Run** | Compilation, imports, and Console access prohibited |
| ENV-02 | Supported default build starts the implemented gameplay | Build/executable verification | **Blocked** | CR-001; executable behavior not reproduced |
| MOV-01 | WASD movement, grounding, diagonal movement and collision behave correctly | Gameplay | **Not Run** | CharacterController/gravity dependencies identified statically |
| MOV-02 | Mouse yaw/pitch and vertical limits work; cursor capture is correct | Gameplay | **Not Run** | Source contains mouse polling, ±80° pitch clamp and cursor locking |
| INT-01 | Prompt and E interaction correspond to the correct crosshair target | Gameplay | **Not Run** | Camera-raycast and collider-local interaction dependency identified |
| INT-02 | Non-interactable, occluded and out-of-range objects cannot be picked up | Boundary/occlusion gameplay | **Not Run** | Nearest-hit raycast and configured 3-unit reach observed statically |
| INV-01 | Successful pickup adds one item and removes its world pickup | Gameplay | **Not Run** | Destruction follows successful insertion in source |
| INV-02 | Six items are retained; seventh pickup is rejected without losing held or world items | Seven-pickup fixture/gameplay | **Not Run** | Historical manual report only |
| INV-03 | Full-inventory feedback lasts approximately two seconds; repeated rejection remains safe | Timed gameplay | **Not Run** | Existing UI feedback path and historical manual report |
| HOT-01 | Six bottom-center slots remain visible with correct names/icons and one highlight | Visual gameplay | **Not Run** | Asset/source dependencies identified; historical manual report |
| HOT-02 | Wheel cycles both ways through empty slots and wraps first/last selection | Gameplay | **Not Run** | Selection logic inspected; historical manual report |
| OUT-01 | Exposed collectible has a visible cyan outline | Rendering | **Not Run** | Historical manual report; no fresh render |
| OUT-02 | Full coverage hides the outline; partial coverage does not reveal hidden silhouette; moving coverage reveals it naturally | Rendering | **Not Run** | Historical manual report only |
| OUT-03 | Successful pickup removes the world outline; inspection preview has no collectible outline | Integrated rendering/gameplay | **Not Run** | Isolation/destruction dependencies identified |
| INS-01 | F opens the selected occupied item; empty selection leaves gameplay unchanged | Gameplay | **Not Run** | Selection/model validation paths inspected |
| INS-02 | Model retains proportions and suitable framing/aspect/lighting while rotating | Visual gameplay | **Not Run** | Uniform scaling and rendering dependencies identified |
| INS-03 | Inspection blocks movement/look/E/wheel, releases cursor and hides interaction UI | Integrated input gameplay | **Not Run** | State capture/blocking paths inspected |
| INS-04 | Drag starts throughout InspectionPanel, including all edges/corners; outside starts are rejected | Pointer-boundary gameplay | **Not Run** | Current scene targets InspectionPanel; final user manual retest absent |
| INS-05 | A captured drag continues outside the panel until LMB release | Pointer gameplay | **Not Run** | Capture/release logic inspected; historical scripted report only |
| INS-06 | Esc restores prior controls, cursor, HUD and selection; repeated reopening is safe | Integrated gameplay | **Not Run** | Restoration paths inspected; historical reports only |
| INS-07 | Simultaneous F/E/wheel and Esc/E/wheel do not leak actions across mode transitions | Input edge-case gameplay | **Not Run** | Early Update/deferred restoration identify a regression boundary |
| INS-08 | Focus loss cancels dragging; disabling the controller restores prior state | Approved state-transition testing | **Not Run** | Lifecycle paths inspected; no fresh execution |
| INS-09 | Missing/invalid inspection models safely refuse opening without locking gameplay | Approved negative fixture | **Not Run** | Validation paths inspected; fixture not executed |
| COV-01 | Inventory project-owned automated test coverage within the inspected scope | Static filename/assembly/attribute inspection | **Pass — static inventory only** | No test assemblies or `[Test]`/`[UnityTest]` definitions discovered |

**COV-01 does not establish gameplay correctness. Zero automated tests were executed.** `TestInteractable.cs` is a gameplay logging component, not an automated test.

## Defects

No independent QA defect was confirmed through fresh behavioral execution.

### Related blocker: CR-001 — P1 High

The Code Review finding concerning the default build scene list is retained as **CR-001**, not duplicated as a QA defect. See the saved Code Review report for its technical analysis.

Executable reproduction remains pending. It blocks acceptance through the committed default build path unless the effective build configuration establishes an override.

### Historical inspection drag issue

The user previously reported restricted drag initiation. Current serialization assigns the full InspectionPanel as the drag region.

That reference change and historical scripted checks do **not** establish final manual acceptance. The issue is not marked freshly reproduced or verified resolved in this assessment.

## Regression results and incremental selection

No gameplay cases were rerun. The following checklist should guide future incremental QA:

| Change trigger | Select these baseline cases |
|---|---|
| Movement/look or input scheduling | MOV-01/02, INT-01/02, INS-03/06/07 |
| Inventory, pickup, item data or capacity | INV-01–03, HOT-01/02, INS-01/06, OUT-03 |
| Hotbar UI, wheel handling or Canvas layout | HOT-01/02, INV-03, INS-03/04/06 |
| Inspection controller, prefab, camera, RenderTexture or panel | INS-01–09, HOT-02, INT-01, OUT-03 |
| Outline shader, renderer, layer, pipeline or camera settings | OUT-01–03, INS-02, applicable import/Console checks |
| Build scenes or build profiles | ENV-02 and startup movement/HUD/pickup/inspection smoke checks |
| Future approved Item Use integration | Add approved use criteria and select regressions from actual affected dependencies |

Do not automatically repeat the entire baseline.

The reused dependency map connects:

- Player controls → PlayerCamera.
- Camera raycast → collider interaction → pickup → inventory.
- Inventory events → six hotbar slot views.
- Selected ItemData → inspection prefab → isolated camera/RenderTexture → UI.
- Inspection mode → input/cursor blocking and restoration.
- Outline registry → depth mask/composite → world-camera output.

## Verification evidence

| Evidence category | Assessment |
|---|---|
| Fresh automated test-runner results | None supplied or executed for this snapshot |
| Fresh scripted Unity MCP checks | None performed |
| Agent-observed gameplay | None |
| Historical hotbar manual tests | User reported all eight passed, including full-feedback timing and seventh-pickup preservation |
| Historical outline manual tests | User reported exposed, covered, partial and reveal cases passed |
| Historical inspection evidence | User reported working F/Esc and stable gameplay, but restricted dragging |
| Latest panel-wide drag evidence | Historical parent-reported scripted checks passed; no explicit final user manual retest supplied |
| Static evidence | Requirements, dependency map, focused source paths, current panel linkage and coverage inventory inspected |

Historical results were not tied to a verified execution of this exact baseline SHA and are **not promoted to current-snapshot passes**.

Unity Test Framework `1.8.0` is declared, but installing the framework does not establish project test coverage.

## Blocked and not-run checks

- Compilation, Console, shader imports, rendering, gameplay and build execution: **Not Run**, outside authorization.
- Default-build acceptance: **Blocked** by CR-001 pending effective build-path confirmation.
- Seven-pickup and invalid-model fixtures: availability not established; creation requires approval.
- Effective Editor-resolved URP version and Renderer Feature persistence: unresolved questions inherited from Code Review, not confirmed QA defects.
- Final panel-wide dragging acceptance: user manual retest still missing.
- Restoration with controls initially disabled: requires an approved component-state setup.
- Current resolution, input-device and visual execution evidence: missing, not inferred failures.

## Manual test handoff

This is a proposed later run, **not authorization to execute it**.

**Record first:** commit, tracked changes, Unity/resolved URP version, scene, platform, resolution, input devices and approved operations.

1. **Environment/startup — ENV-01/02**  
   Check compilation, imports and Console under approved operations. Confirm the effective startup scene. If build execution is approved, launch it and record CR-001’s outcome.

2. **Controls/interaction — MOV-01/02, INT-01/02**  
   Walk in each direction and diagonally into existing collision. Check vertical look limits. Aim at eligible, ineligible, distant and covered objects; verify prompt and E eligibility.

3. **Inventory/hotbar — INV-01–03, HOT-01/02**  
   Start empty and cycle through all six slots in both directions. Collect six items, checking displayed data and world removal. Reject a seventh repeatedly; verify item preservation and approximately two-second feedback.

4. **Outlines — OUT-01–03**  
   Check exposed, fully covered, partly covered and revealed objects. Confirm pickup removes the world outline and inspection has no cyan outline.

5. **Inspection entry — INS-01–03**  
   Check F on empty and occupied slots. Verify the selected model/name, framing, cursor release, visible hotbar and blocked movement/look/E/wheel.

6. **Dragging — INS-04/05**  
   Start just inside every panel edge and corner, including margins outside the RawImage. Start outside and move inside while held: no rotation. Start inside and move outside: rotation continues until release.

7. **Closing/transitions — INS-06/07**  
   Esc while moving the mouse/holding movement; confirm restoration without unwanted camera movement. Repeat opening/closing and test simultaneous transition inputs for unintended pickup or selection.

8. **Negative/lifecycle cases — INS-08/09**  
   With separately approved setup, check focus loss, controller disable, initially disabled controls and invalid inspection models.

Active testing changes runtime movement, selection, pickups, preview instances and cursor state. Editor/build operations may also generate artifacts. Approve fixture creation, captures and restoration separately. Do not save runtime scene changes or delete temporary objects without explicit approval.

## Assessment

The initial **static QA baseline and reusable checklist are established**. Current runtime readiness remains unverified.

CR-001 blocks acceptance through the committed default build path, and final manual acceptance of panel-wide dragging remains pending. No new behavioral defect or fresh gameplay pass is claimed.

The baseline commit and working-tree status remained unchanged during the assessment. No files were modified by the assessment, and no active testing was performed. This report was subsequently saved with explicit user approval.
