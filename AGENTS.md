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

## Code Review and QA agents

- Code Review focuses on code correctness, maintainability, architecture, and regressions.
- QA focuses on functional requirements, tests, edge cases, and gameplay verification.
- Perform one explicitly approved initial baseline review per agent. Afterward, use incremental reviews based on approved changes and affected dependencies; do not automatically repeat a full-project review.
- Both agents remain read-only by default. Do not edit files, scenes, assets, or settings, implement fixes, or perform mutating tool actions.
- Read-only instructions do not replace sandbox enforcement. Use a read-only parent session and disable mutating external tools when enforcing read-only reviews.
- Git inspection still requires explicit user approval. Review or agent-configuration approval alone does not authorize Git operations or a baseline review.
- Report evidence and verification gaps. A read-only reviewer must not launch Unity, enter Play Mode, request compilation, or run checks that write project state without separate approval. Mark those checks as not performed and hand them off to an approved QA run.
- Return Markdown reports to the parent agent. Saving reports requires explicit approval; the read-only agents do not save reports themselves.

## Project status

- Six-slot hotbar: completed.
- Collectible outline: completed.
- 3D item inspection: completed.

The completed hotbar retains six fixed slots at the bottom center, mouse-wheel cycling through empty slots, a persistent selection highlight, safe full-inventory handling, and existing pickup functionality.

## Next planned feature: Item Use System

This is planned scope, not authorization to implement. Follow the inspection, planning, and explicit approval process above before starting.

- Inspect the existing inventory, hotbar, inspection, pickup, and interaction systems before proposing item-use behavior.
- Agree on the gameplay requirements and implementation scope before making changes.
- Preserve the completed hotbar, collectible outline, and item inspection functionality.
