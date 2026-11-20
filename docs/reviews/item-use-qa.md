# QA Assessment - Incremental

## Snapshot and authorization

- **Date:** 2026-11-20.
- **Scope:** One authorized post-Code Review assessment of the Item Use System and affected inventory, interaction, feedback and inspection dependencies.
- **Requirements:** Approved criteria relayed by parent; `docs/development-logs/item-use-system.md`.
- **References:** `item-use-code-review.md`, `qa-baseline.md`, and `qa-unity-mcp-verification.md`. No full baseline repeated.
- **Snapshot:** Current scoped filesystem contents and connected Editor; branch `feature/item-use-system` supplied by parent. No Git operations performed. Historical baseline SHA `e4af2f39bc6b6ad4b235978909d6e4505f3b326a` is not a verified commit for the current feature changes.
- **Environment:** Unity 6000.6.0f1, WindowsEditor, `FYP@24584046b956cde1`, saved OfficeScene in Edit Mode. Editor idle, not compiling, no pending domain reload.
- **Approved operations:** Read-only source/document inspection, documented Unity resources and query actions. No active testing, compilation, Play Mode, fixtures, captures, edits or report saving by the QA agent.
- **Enforcement:** Inherited permissions exposed mutating tools; a separate read-only sandbox was not enforced. Only the approved reads were invoked.

The QA agent returned this report to the parent, which saved it with explicit user approval.

## Requirement coverage

This matrix preserves the original read-only QA Agent assessment. Subsequent user-performed Play Mode results are recorded separately in the manual-testing addendum below; they update only the explicitly observed subcases, not every part of a grouped test case.

“Pass” below applies only to the explicitly identified configuration or observation.

| Test ID | Requirement/scenario | Method | Result | Evidence |
|---|---|---|---|---|
| IUSE-C01 | Door references canonical card, Player Inventory, own renderer/collider and feedback; initially locked | Live component resource and scene serialization | **Pass — configuration only** | ExitDoor component reports `IsUnlocked:false`; required item `Assets/Items/AccessCard.asset`; assigned dependencies present |
| IUSE-C02 | Independent two-second TMP feedback under InteractionCanvas | Live resource and scene serialization | **Pass — configuration only** | InteractionMessage references its own TMP component, duration `2`; parent InteractionCanvas; Inventory Full has a separate reference |
| IUSE-C03 | Empty inventory exposes null selected item safely | Live Edit Mode property query | **Pass — observed initial state only** | Player Inventory: `Items:[]`, `SelectedIndex:0`, `SelectedItem:null` |
| IUSE-01 | Empty selection rejects use, retains lock/geometry/collision and shows exactly `Requires an access card` | Gameplay required; static path inspected | **Not Run** | Selected-item comparison and exact string exist; no interaction executed |
| IUSE-02 | Wrong selected item rejects use despite card in another slot | Approved wrong-item fixture and gameplay required | **Not Run** | Source compares selected asset only; no fixture executed |
| IUSE-03 | Selected canonical card unlocks, shows `Door unlocked`, hides renderer and disables blocking collider | Gameplay required | **Not Run** | Static path and references support expected behavior |
| IUSE-04 | Inventory/card, selected slot/highlight and subsequent inspection preserved | Before/after gameplay comparison required | **Not Run** | Code Review found no inventory mutation; runtime retention unverified |
| IUSE-05 | Repeated direct interactions after unlock are harmless | Separately approved direct-call harness required | **Not Run** | Static early return exists; ordinary E cannot retarget the disabled door collider |
| IUSE-06 | Feedback expires in approximately two seconds, refreshes on rejection, remains independent and clears across F/Esc | Timed integrated gameplay required | **Not Run** | Unscaled deadline, clear-on-disable and inspection Canvas hiding inspected |
| IUSE-07 | Existing pickup, six-slot wheel/highlight, capacity protection, inspection and world outlines preserved | Focused gameplay regression run required | **Not Run** | Relevant dependency paths and historical baseline reused |
| IUSE-08 | Existing E reach/occlusion, locked blocking and unlocked passage work | Collision/raycast gameplay required | **Not Run** | Enabled non-trigger door collider observed; interaction raycast remains three metres |
| IUSE-E01 | Current Console has no errors | Read-only Console query | **Pass — Console observation only** | Zero errors; two MCP WebSocket initialization warnings; `truncated:false`, no next cursor |

## Defects

No independent behavioral QA defect was reproduced. No new QA defect ID is assigned.

The incremental Code Review reports **no actionable P0–P3 findings**. Its static conclusions are not gameplay passes.

**Related historical finding CR-001:** Current build-list query contains enabled OfficeScene at index 0 and SampleScene at index 1. The historical OfficeScene omission therefore does not describe this queried configuration. The separately observed EditorBuildSettings change has unconfirmed provenance. Executable startup was not tested; CR-001 is not declared runtime-resolved.

The doorway sits at the existing floor boundary. Safe passage and behavior beyond the threshold remain unverified risks, not reproduced defects. Exterior level content must not be inferred or added through QA.

## Regression results

No runtime baseline cases were rerun.

Selected affected regressions for the next approved run:

- **INT-01/02, MOV-01/02:** E targeting, range, occlusion and door collision/passage.
- **INV-01–03, HOT-01/02:** Pickup, six slots, selection/highlight, wrapping through empty slots and full-inventory preservation.
- **INS-01, INS-03–07:** Selected-card inspection, interaction blocking, panel dragging, F/Esc restoration and simultaneous input boundaries.
- **OUT-01–03:** Focused world-outline and pickup/inspection isolation smoke checks.

Unrelated baseline negative fixtures and broad rendering/package/build checks were omitted because this is an incremental assessment. Historical results were not promoted to current passes.

## Verification evidence

- **Automated test-runner results:** None; **zero tests executed**. Read-only EditMode and PlayMode listings each returned only a project-level `EscapeTheOffice` entry. No executable leaf test cases or passing results were established.
- **Scripted Unity MCP checks:** Editor state, active scene, hierarchy/name queries, scoped component resources, Console, build list and test listings. Initial component-resource reads returned transient “not found” responses; rediscovery and subsequent reads succeeded.
- **Agent-observed gameplay:** None.
- **User-reported manual results:** No new Item Use manual results supplied. Historical baseline reports remain historical.
- **Static assessment:** Scoped scripts, relevant inspection/feedback dependencies, serialized scene references and prior review reports inspected.
- **Compilation:** Parent reports compilation completed and both new classes loaded. This assessment observed idle compilation state but did not request or independently run compilation.

## Blocked and not-run checks

- All runtime acceptance and regression cases: **Not Run**, outside this assessment’s authorization.
- Wrong-item case: **Blocked for execution** until an approved non-card ItemData/pickup fixture is available. The inspected `Assets/Items` folder contains only AccessCard; TestDocument is not an inventory-item fixture.
- Full-inventory feedback independence requires an approved seven-pickup setup.
- Direct repeated-call verification requires an explicitly approved harness or scripted invocation; repeated E alone does not establish it.
- Feedback timing, readability/layout, input transitions, physical passage and runtime Console behavior remain unverified.
- Build/executable startup remains **Not Run**.

## Manual test handoff

Proposed later run only; execution requires separate approval.

**Setup and evidence:** Record current snapshot, Unity version, scene, resolution and input devices. Begin from the saved locked scene. Approve Play Mode, input, fixture creation/direct-call operations and any screenshots or recordings separately. Record exact messages, timings, inventory/selection before and after, collider/renderer state and runtime Console results.

1. **IUSE-01:** With empty inventory, approach within three metres and press E on the door. Expect exact required-card message, continued visible door and blocked movement.
2. **IUSE-02:** Collect the card and an approved wrong item. Select the wrong item while the card occupies another slot; press E. Expect rejection. Repeat with an empty selected slot while holding the card.
3. **IUSE-03/04:** Select the canonical card. Record count, card identity, slot and highlight; press E. Expect exact success text, hidden door and disabled collision with recorded inventory/selection unchanged. Press F afterward; expect normal card inspection.
4. **IUSE-05:** Under approved direct-call setup, invoke interaction repeatedly after unlocking. Expect no additional effects, consumption, relocking or exceptions.
5. **IUSE-06:** Time rejection and success feedback. Repeat rejection before expiry; expect approximately two seconds from the latest interaction. Enter inspection while feedback is visible, then close with Esc both before and after its former expiry; expect no stale text. Verify Inventory Full separately and overlapping feedback when the approved fixture permits.
6. **IUSE-07:** Verify six slots, wheel wrapping through empty slots, pickup/world removal, full-capacity protection, highlight retention, F/Esc, panel-edge dragging and input blocking/restoration. Smoke-test collectible outline visibility and inspection isolation.
7. **IUSE-08:** Test E outside reach and behind opaque obstruction. Confirm locked collision, then cautiously cross the unlocked threshold and record floor-boundary behavior without extending the level.

**Side effects and restoration:** Testing changes runtime position, inventory, selection, door/UI state and inspection previews. Exit Play Mode without saving runtime scene changes; confirm the saved scene remains locked and references intact. Fixture creation and cleanup need explicit approval. No temporary fixtures were created during this assessment.

## Assessment

The incremental **static and configuration assessment is complete**, with no confirmed new QA defect. Required functional acceptance remains **Not Run**.

Correct/wrong selection, retention, repeated calls, feedback lifecycle, inspection compatibility and physical passage still require approved runtime evidence. The feature is **not yet ready for functional acceptance**.

## Parent final checkpoint after the QA report

The parent confirmed the four implementation-file hashes were unchanged across both reviews and OfficeScene remained saved in Edit Mode. A later read-only Console query returned zero errors and three MCP warnings: two WebSocket initialization warnings and one GameObjectSerializer TransformHandle warning. This later parent observation is distinct from the agent's two-warning query above. No code fixes or active tests followed the review.

## Subsequent user-reported manual Play Mode testing

- **Recorded date:** 2026-11-20, using the current session date; the user did not separately supply the actual test date.
- **Tester and method:** User-performed manual Play Mode testing in OfficeScene, reported in the conversation after the original QA assessment.
- **Provenance:** These are user-reported gameplay observations, not QA-agent execution, automated tests, or a fresh MCP inspection. No agents or tests were rerun to record them.
- **Snapshot limitation:** The tested commit, working-tree contents, Editor/package versions, resolution and input devices were not independently reverified. The historical baseline SHA remains `e4af2f39bc6b6ad4b235978909d6e4505f3b326a`; this addendum does not attribute the implementation or manual run to that commit.

| Observation ID | Related QA case/subcase | User-reported manual observation | Result |
|---|---|---|---|
| IUSE-M01 | IUSE-01, rejection without a card | Without an Access Card, the locked door cannot be unlocked | **PASS — user-reported manual Play Mode** |
| IUSE-M02 | IUSE-02, card held but not selected | With an Access Card in Inventory but not selected, the door cannot be unlocked | **PASS — user-reported manual Play Mode** |
| IUSE-M03 | IUSE-03, selected-card use | With the Access Card selected, pressing E successfully unlocks the door | **PASS — user-reported manual Play Mode** |
| IUSE-M04 | IUSE-03/08, visible opening and passage | Door disappears and the player can walk through the doorway | **PASS — user-reported manual Play Mode** |
| IUSE-M05 | IUSE-04, card retention | Access Card remains in the Hotbar and is not consumed | **PASS — user-reported manual Play Mode** |
| IUSE-M06 | IUSE-04/07, selection retention | Selected Hotbar slot and highlight remain unchanged | **PASS — user-reported manual Play Mode** |
| IUSE-M07 | IUSE-04/07, normal inspection path | F still opens Access Card inspection and Esc exits correctly | **PASS — user-reported manual Play Mode** |
| IUSE-M08 | IUSE-06, success-message lifetime | "Door unlocked" disappears after approximately two seconds | **PASS — user-reported manual Play Mode** |
| IUSE-M09 | IUSE-E01, runtime Console observation | No new Console errors were observed | **PASS — user-reported manual Play Mode** |

### Remaining Not Run cases

The nine observations above do not establish a pass for the following:

- **IUSE-01:** exact rejection text, rejection-message timing, and detailed locked-collider state were not explicitly reported.
- **IUSE-02:** selection of an actual incorrect, non-card ItemData while the card is held elsewhere remains **Not Run**; no approved wrong-item fixture has been verified. Card-held-but-unselected rejection is covered by IUSE-M02 only.
- **IUSE-05:** repeated direct calls after unlocking and duplicate-effect/idempotence checks remain **Not Run**.
- **IUSE-06:** rejection-feedback expiry/refresh, simultaneous feedback independence from Inventory Full, and stale-feedback clearing when entering/leaving inspection remain **Not Run**.
- **IUSE-07:** full-inventory regression, complete wheel/empty-slot wrapping, pickup regressions, panel-edge/corner drag regression, simultaneous inputs, focus loss, previously disabled control restoration, and outline visibility/occlusion remain **Not Run**.
- **IUSE-08:** out-of-range/opaque-obstruction rejection and extended behavior beyond the floor boundary remain **Not Run**. Walking through the unlocked doorway itself is covered by IUSE-M04.
- Build/executable startup and CR-001 runtime resolution remain **Not Run/unverified**.

No new automated results or warning-free Console claim is inferred. Previously recorded MCP warnings remain historical observations; the user reported no new errors, not the absence of every warning.

### Updated acceptance summary

The user reports **PASS for all nine specified core interaction and post-unlock observations**. This adds manual evidence for selection-dependent unlocking, passage, retained card/selection, normal F/Esc inspection, success-feedback timing and absence of observed new errors.

The original static/configuration findings and their evidence are preserved. Broader edge cases and regressions listed above remain **Not Run**; no blanket full-suite pass or independently reverified runtime result is claimed.
