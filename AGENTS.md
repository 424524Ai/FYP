# Escape the Office — Agent Development Workflow

## Project context

- Unity 6000.6.0f1, Universal Render Pipeline (URP), C#, and the new Input System.
- First-person office escape room puzzle game.
- Preserve the existing player movement, interaction, item pickup, and inventory systems.
- Unity MCP is available to inspect and modify the running Unity Editor. Use it to verify live scene objects and component references when relevant.

## Planning and approval

1. Before implementing a feature, inspect the relevant scripts and Unity objects.
2. Propose a clear plan describing the intended behavior, files and scene objects affected, integration with existing systems, and verification steps.
3. Wait for explicit user approval before implementing the feature. Approval applies only to the agreed scope.
4. Ask for clarification before making significant gameplay or architecture decisions. If implementation requires a significant change to the approved plan, explain the change and wait for approval.

## Implementation rules

- After approval, implement the agreed scope using C# and Unity MCP as appropriate.
- Prefer small, focused changes. Avoid unrelated refactoring and unnecessary dependencies.
- Reuse existing systems where practical and preserve existing scene and asset references, including serialized references and asset GUIDs.
- Keep `main` stable. Do not implement features directly on `main`.
- Preserve user changes. Never discard or overwrite unrelated work.

## Delivery workflow and Git permissions

Follow this sequence for feature development:

GitHub Issue -> feature branch -> implementation -> testing -> commit -> pull request -> merge.

- Establish the GitHub Issue and feature branch before implementation, subject to the user's instructions and permissions.
- Never commit, push, merge, delete branches, or discard user changes without explicit approval for those actions. Feature implementation approval alone does not authorize them.
- If a required workflow step is unavailable or prohibited by the user, report the limitation and ask how to proceed rather than bypassing it.
- Explicit task restrictions, including instructions to perform no Git operations, take precedence over the normal workflow.

## Verification and review

- Verify C# compilation and inspect the Unity Console for errors and relevant warnings.
- Run available tests relevant to the change and perform gameplay checks appropriate to the feature.
- Review the changes for correctness, regressions, scope, and preservation of scene and asset references.
- Clearly report checks that could not be completed and why.
- Do not claim a feature works until it has been tested. Distinguish compilation, automated tests, and gameplay verification in the report.

## Completion report

Summarize:

- Modified files and their purpose.
- Unity scene, GameObject, component, and prefab changes.
- Compilation, Console inspection, tests, gameplay checks, and code review results.
- Checks not completed, remaining limitations, and any required follow-up.

## Next planned feature: Six-Slot Hotbar

This is planned scope, not authorization to implement. Follow the inspection, planning, and explicit approval process above before starting.

- Provide six fixed slots, always visible at the bottom center of the screen.
- Mouse wheel input cycles through all six slots, including empty slots.
- Give the selected slot a visible highlight and preserve its selection during normal gameplay.
- Switching slots must not disable player movement or mouse look.
- Reuse existing `Inventory`, `ItemData`, and `ItemPickup` functionality where practical.
- Handle a full inventory safely without losing items.
- Keep 3D item inspection and item usage outside this feature; they are separate future features.
