---
name: jira-issue-management
description: 'Provides access to jira issues through the acli executable'
license: MIT
compatibility: Requires PowerShell and acli.exe installed and available in PATH. Designed for Windows environments.'
metadata:
  author: Eduardo Arroyo Ramirez
  version: 1.0
---

# Jira Issue Management

## Overview

This skill enables the agent to interact with Jira work items to enrich the context and keep them up to date.

The skill supports:

- Reading a Jira issue
- Listing issue comments
- Changing issue status
- Creating comments
- Adding labels/flags
- Removing labels/flags

Issue creation and deletion are strictly forbidden as well as any destructive operation not explicitly defined in this skill.

---

## Prerrequisites

- acli.exe is installed and it's path included in PATH environment variable

---

# Activation Rules

Activate this skill when the user prompt:

- Contains the word `jira`
- Contains the word `issue`
- References a Jira issue key matching patterns like:
  - `KAN-001`
  - `[A-Z]+-[0-9]+`

Examples:

- `Resume la issue KAN-001`
- `Añade un comentario a la issue ABC-55`
- `Pon KAN-003 en curso`
- `Lista los comentarios de KAN-001`
- `Marca la issue como bloqueada`

---

# Environment Assumptions

- Operating System: Windows
- Shell: PowerShell
- Executable available in PATH:
  - `acli`

All commands must be executed using PowerShell.

The assistant must capture:
- stdout
- stderr
- exit code

---

# Security Rules

## Forbidden Operations

The assistant MUST NEVER:

- Delete issues
- Create issues
- Execute destructive Jira operations not explicitly defined in this skill

If the user requests forbidden operations, refuse politely.

Example:

> "La creación y eliminación de issues está prohibida por la política de esta skill."

---

# Supported Operations

---

# 1. Read Issue

## Command

```powershell
acli jira workitem view <ISSUE_KEY> -f "*all"
```

## Example
```powershell
acli jira workitem view KAN-001 -f "*all"
```

## Behavior
* Execute directly without confirmation.
* Summarize the issue clearly for the user.

## Validation

Before execution:

* Validate issue key format.

Accepted pattern:
```regex
^[A-Z]+-[0-9]+$
```

# 2. List Issue Comments

## Command
acli jira workitem comment list --key <ISSUE_KEY>

## Example
acli jira workitem comment list --key KAN-001

## Behavior
Execute directly without confirmation.
Present comments chronologically.

## Validation
Validate issue key format before execution.

# 3. Change Issue Status

## Allowed States

Only these states are allowed:

* Tareas por hacer
* En curso
* Finalizada

## Command
acli jira workitem transition -k <ISSUE_KEY> -s "<STATE>"

## Examples
acli jira workitem transition -k KAN-001 -s "En curso"
acli jira workitem transition -k KAN-001 -s "Finalizada"
Mandatory Confirmation

Before execution, ALWAYS ask for confirmation.

Example:

> "Voy a cambiar el estado de la issue KAN-001 a 'En curso'. ¿Confirmas la operación?"

Do not execute until explicit confirmation is received.

## Validation

Before execution:

* Validate issue key format.
* Validate state belongs to allowed states.
* Reject empty state values.

# 4. Create Comment

## Command
```powershell
acli jira workitem comment create -k <ISSUE_KEY> -b "<COMMENT>"
```

## Example
```powershell
acli jira workitem comment create -k KAN-001 -b "Este es el cuerpo del comentario"
```

## Mandatory Confirmation

Before execution, ALWAYS ask for confirmation.

Example:

> "Voy a publicar el siguiente comentario en KAN-001: 
> 'El despliegue ha sido completado correctamente.'
> ¿Deseas continuar?"

## Validation

Before execution:

* Validate issue key format.
* Comment body MUST NOT be empty.
* Trim whitespace.
* Reject comments containing only spaces or line breaks.

# 5. Add Label / Flag
## Command
```powershell
acli jira workitem edit -k <ISSUE_KEY> --labels "<LABEL>"
```

## Example
```powershell
acli jira workitem edit -k KAN-001 --labels "blocked"
```

## Mandatory Confirmation

Before execution, ALWAYS ask for confirmation.

Example:

> "Voy a añadir la etiqueta 'blocked' a la issue KAN-001. 
> ¿Confirmas?"

## Validation

Before execution:

* Validate issue key format.
* Label MUST NOT be empty.

# 6. Remove Label / Flag
## Command
```powershell
acli jira workitem edit -k <ISSUE_KEY> --remove-labels "<LABEL>"
```

## Example
```powershell
acli jira workitem edit -k KAN-001 --remove-labels "blocked"
```

## Mandatory Confirmation

Before execution, ALWAYS ask for confirmation.

## Validation

Before execution:

* Validate issue key format.
* Label MUST NOT be empty.


# Error Handling

If command fails:
* Capture:
    * executed command
    * stderr
    * exit code (if available)
* Return a user-friendly error message.

## Error Format

Example:
```
No se pudo completar la operación sobre Jira.

Comando ejecutado:
acli jira workitem transition -k KAN-001 -s "En curso"

Error devuelto:
Transition not allowed from current status

Código de salida:
1
```

# Output Formatting Guidelines
## For issue details

Provide:
* Key
* Title
* Status
* Assignee
* Labels
* Summary/description

Use concise summaries when possible.

## For comments

Display:

* Author
* Date
* Content

Maintain chronological order.

## For successful updates

Use explicit confirmations.

Examples:

```
El estado de la issue KAN-001 se cambió correctamente a "En curso".
```

```
El comentario fue publicado correctamente en KAN-001.
```

```
La etiqueta "blocked" fue añadida correctamente a KAN-001.
```

# PowerShell Execution Guidance

Commands should be executed exactly as specified.

Avoid:

* modifying command syntax
* adding unsupported parameters
* using shell aliases

Always quote:

* states
* labels
* comment bodies

# Examples

Read Issue

User:
```
Resume la issue KAN-001
```

Execution:
```powershell
acli jira workitem view KAN-001 -f "*all"
```

## List Comments

User:
```
Lista los comentarios de KAN-001
```

Execution:
```powershell
acli jira workitem comment list --key KAN-001
```

## Change Status

User:

```
Pon KAN-001 en curso
```

Assistant:

```
Voy a cambiar el estado de la issue KAN-001 a "En curso". ¿Confirmas la operación?
```

Execution after confirmation:

```
acli jira workitem transition -k KAN-001 -s "En curso"
```

## Add Comment

User:

```
Añade un comentario a KAN-001 indicando que el despliegue terminó
```

Assistant:

```
Voy a publicar el siguiente comentario en KAN-001:

"El despliegue terminó correctamente."
¿Deseas continuar?
```

Execution after confirmation:

```
acli jira workitem comment create -k KAN-001 -b "El despliegue terminó correctamente."
```

# Final Behavioral Rules

The assistant should:

* Be conservative with write operations
* Always request confirmation before modifying Jira data
* Never invent issue states
* Never execute unsupported Jira commands
* Explain failures clearly and completely
* Keep responses concise and operational