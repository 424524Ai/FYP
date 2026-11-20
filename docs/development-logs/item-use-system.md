# Item Use System — Feature Development Log

## Date and purpose

- Date: 2026-11-20.
- Branch: `feature/item-use-system`, confirmed from branch metadata without Git commands.
- Purpose: use the selected Access Card to unlock the office exit and allow passage without consuming the card.
- Status: implementation, initial compilation checks, incremental Code Review and read-only QA assessment completed. User reports nine core manual Play Mode observations passed; remaining edge cases and regressions are still Not Run.

## Approved scope

- Reuse Inventory, the always-visible six-slot hotbar, and the existing E-key/IInteractable path.
- Add a null-safe, read-only SelectedItem property.
- Compare the selected ItemData asset with an explicitly assigned Access Card asset; do not search other slots.
- Wrong or empty selection displays exactly "Requires an access card".
- Successful unlocking displays "Door unlocked", hides the door renderer and disables its blocking collider.
- Preserve held items, selected slot and highlight; repeated direct interactions are harmless.
- Use separate two-second TextMesh Pro feedback that clears when inspection hides InteractionCanvas.
- No Tab inventory, door animation, victory screen, gameplay timer, new artwork, or unrelated rendering/Build Profile changes.

## Implementation details

| File/object | Purpose |
|---|---|
| Assets/Scripts/Inventory.cs | Null-safe SelectedItem getter; no changes to collection, capacity, events or selection mutation |
| Assets/Scripts/LockedDoorInteractable.cs | Selected-asset identity check, runtime unlock state, renderer/collider disabling, feedback |
| Assets/Scripts/InteractionFeedbackUI.cs | Reusable TMP feedback, two-second unscaled expiry, clear on disable |
| Assets/Scenes/OfficeScene.unity | Scene-only door and independent feedback object with serialized references |
| OfficeEnvironment/ExitDoor | Cube at (0, 1.2, -5), scale (1.8, 2.4, 0.15), opaque existing wall material and non-trigger BoxCollider |
| InteractionCanvas/InteractionMessage | TMP text at centered anchor plus (0, -85), 620 by 50 reference units; existing font/material reused |

The door references Player Inventory, Assets/Items/AccessCard.asset, its own renderer/collider, and the new feedback component. New scripts have brief purpose comments.

Feedback uses TMP component visibility rather than disabling its own GameObject, allowing expiry updates to continue. OnDisable clears both text and visibility, so entering inspection cannot leave a queued stale message. Inventory Full retains its existing independent text and controller.

No prefab, item asset, inspection, pickup, player-control, outline, shader, renderer, package, or Build Profile changes are part of the implementation.

## AI-assisted workflow

1. Read AGENTS.md and existing Inventory, ItemData, selection, pickup, interaction, hotbar and inspection code.
2. Use read-only Unity MCP to inspect OfficeScene, its south-wall opening, canvases and serialized references.
3. Propose a minimal implementation; user approves Option A, which hides the unlocked door and disables its collider.
4. Add the three scoped C# changes and use Unity MCP to configure the two production scene objects and save OfficeScene.
5. Request compilation and inspect Editor state/Console before invoking the incremental Code Review Agent.
6. Run Code Review first, then QA, under their approved read-only scopes. Parent saves reports; recommendations are not automatically applied.
7. Keep fresh compilation/configuration observations separate from unperformed runtime tests.

The initial baseline reference is e4af2f39bc6b6ad4b235978909d6e4505f3b326a. This is historical context, not a claim that the current changes are committed at that SHA. No Git operations were performed.

## Initial verification evidence

- Unity 6000.6.0f1, OfficeScene in Edit Mode; no Play Mode or test runner used.
- MCP refresh requested script compilation. Editor subsequently reported compilation finished with no pending domain reload.
- Both new MonoScript assets resolved to their compiled C# classes.
- Console query after compilation and setup returned no C# errors; one MCP WebSocket initialization warning was present.
- Scene saved after assigning the explicit ItemData, Inventory, renderer, collider and feedback references.
- No active interaction, feedback timing, inventory mutation or physical traversal was executed. These remain Not Run.
- QA's final read-only Console query observed zero errors and two MCP WebSocket initialization warnings. No shader changes or new shader errors were reported.
- A later parent final Console query observed zero errors and three MCP warnings: the same two WebSocket initialization warnings plus one GameObjectSerializer TransformHandle warning. This is a separate later observation, not a revision to the QA agent's query result; no runtime gameplay was executed.
- All four implementation files (three C# files and OfficeScene) retained their hashes across Code Review and QA; no review recommendations were applied.

## Incremental reviews

- Code Review report: docs/reviews/item-use-code-review.md. No actionable P0–P3 defects or blocking code findings identified within scope. Physical traversal at the floor boundary and all runtime behavior remain unverified risks/checks, not demonstrated passes.
- QA report: docs/reviews/item-use-qa.md. IUSE-C01/C02 configuration, IUSE-C03 initial empty selection and IUSE-E01 Console observations gained evidence. At the original agent-run checkpoint, all IUSE-01 through IUSE-08 runtime criteria were Not Run. Subsequent user-performed results are recorded separately below; wrong-item execution remains Not Run until a non-card fixture is available.
- Do not implement any recommendations automatically.
- Read-only test listings returned project-level entries only, not executable leaf test results. Zero automated tests were executed. No agents entered Play Mode or ran gameplay tests.

## Manual verification plan

This is the original manual verification plan. The subsequent results section records only the subcases actually reported by the user; all other subcases remain Not Run.

1. IUSE-01: select an empty hotbar slot; SelectedItem should be null. E on the locked door shows the exact required-card message and keeps geometry/collision enabled.
2. IUSE-02: with an approved non-card test fixture, select an incorrect item while holding the card elsewhere. E must not search for or use the unselected card.
3. IUSE-03: select the Access Card and target the door within existing interaction reach. E should unlock, hide geometry, disable blocking collision and show "Door unlocked".
4. IUSE-04: verify inventory count, card reference, selected slot/highlight and subsequent F inspection remain unchanged after success.
5. IUSE-05: repeated interaction must not consume items, restore collision, repeat unlock effects or throw exceptions.
6. IUSE-06: verify feedback lasts approximately two seconds, repeated rejection refreshes its lifetime, Inventory Full remains independent, and F inspection clears feedback without stale text returning on Esc.
7. IUSE-07: verify pickup, six-slot wheel wrapping, empty-slot selection, full-inventory protection, inspection dragging/F/Esc and world outlines.
8. IUSE-08: verify E range/opaque occlusion, locked blocking and actual walking through the unlocked doorway.

Wrong-item fixtures and any active runtime test setup require separate approval. No temporary test objects were created.

## Subsequent manual testing results

- Recorded date: 2026-11-20, using the current session date; the actual test date was not separately supplied by the user.
- Tester/method: user-performed manual Play Mode testing in OfficeScene, reported after the original read-only QA assessment.
- Evidence source: the user's nine explicit PASS observations in the conversation. No QA/Code Review agent, Unity operation, automated test or MCP check was rerun to document these results.
- Snapshot: tested commit, working-tree state, versions, resolution and input devices were not independently reverified. The historical baseline SHA is unchanged and is not asserted to identify the tested feature snapshot.

| Observation | Scenario | Result |
|---|---|---|
| IUSE-M01 | No Access Card: locked door cannot be unlocked | **PASS — user-reported manual** |
| IUSE-M02 | Access Card held but not selected: door cannot be unlocked | **PASS — user-reported manual** |
| IUSE-M03 | Access Card selected: E unlocks door | **PASS — user-reported manual** |
| IUSE-M04 | Door disappears and player walks through doorway | **PASS — user-reported manual** |
| IUSE-M05 | Access Card stays in Hotbar without consumption | **PASS — user-reported manual** |
| IUSE-M06 | Selected slot and highlight remain unchanged | **PASS — user-reported manual** |
| IUSE-M07 | F opens card inspection; Esc exits correctly | **PASS — user-reported manual** |
| IUSE-M08 | "Door unlocked" disappears after approximately two seconds | **PASS — user-reported manual** |
| IUSE-M09 | No new Console errors observed | **PASS — user-reported manual** |

Full provenance and mapping to original IUSE criteria are recorded in docs/reviews/item-use-qa.md. Original QA Agent findings and statuses remain intact as the earlier checkpoint; only these specific manual observations gain PASS evidence.

## Limitations and follow-up

- Core unlocking, doorway passage, card/selection retention, normal inspection/F/Esc, success-message lifetime and no observed new errors now have user-reported manual PASS evidence, not agent-observed or automated verification.
- Still **Not Run**: actual wrong-item selection; exact rejection-message verification; rejection expiry/refresh; repeated direct interactions; full-inventory regression and overlapping-message independence; stale-feedback inspection checks; complete wheel/pickup regression; drag-boundary regression; simultaneous inputs, focus loss and previously disabled control restoration; outline visibility/occlusion; E reach/obstruction boundaries; build/executable startup; and other unreported subcases.
- The exit is at the current floor boundary. The user confirms walking through the unlocked doorway, but extended exterior traversal/grounding was not reported. Exterior level content remains outside scope.
- The state is session-only: restarting gameplay restores the initial locked scene. Save/load and victory behavior are outside scope.
- Existing baseline reports remain historical evidence and are not rewritten by this feature.
- Scope monitoring detected that ProjectSettings/EditorBuildSettings.asset changed during the session. OfficeScene already reported build index 0 in the initial pre-configuration MCP query; a later read-only query lists OfficeScene at index 0 and SampleScene at index 1. No implementation command edited Build Settings or Build Profiles. The source of the disk change is unconfirmed; the file was neither reverted nor altered to enforce the historical baseline. Treat this as a separately observed configuration change, not an Item Use implementation edit or executable verification.
