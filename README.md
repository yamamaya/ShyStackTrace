# ShyStackTrace

A .NET utility library for generating "shy" stack traces to protect developer privacy and application security by sanitizing stack traces. ShyStackTrace removes sensitive information such as developer-identifiable file paths and internal function names that could expose confidential program details in production logs or error reports.

## Overview

ShyStackTrace is designed to address the security and privacy concerns that arise when stack traces are exposed in production environments, logs, or error reporting systems. By removing potentially sensitive information like absolute file paths containing developer names, machine names, and detailed function signatures, this utility helps protect both developer privacy and application internals from unintended disclosure.

## Security & Privacy Features

- **Developer Identity Protection**: Removes file paths that may contain developer usernames, machine names, or personal directory structures.
- **Path Sanitization**: Eliminates absolute paths that could reveal internal project structure or system configuration.
- **Function Name Filtering**: Strips detailed function signatures that might expose internal application logic.
- **Clean Error Reporting**: Provides safe stack traces suitable for production logging and external error reporting.
- **Zero Information Leakage**: Ensures no sensitive development environment details are exposed.

## Why Use ShyStackTrace?

### Privacy Concerns
Standard .NET stack traces often contain:
- Developer usernames in file paths (`C:\Users\JohnDoe\Projects\...`).
- Machine names and internal network paths.
- Detailed project directory structures.
- Full namespace and class hierarchies.

### Security Implications
Exposing detailed stack traces can reveal:
- Internal application architecture.
- Third-party library usage and versions.
- Code organization and naming conventions.
- Potential attack vectors for malicious actors.

## Installation

Copy the `ShyStackTrace.cs` file into your project and ensure it's in the `OaktreeLab.Utils.Debug` namespace.

## Usage

### Basic Usage

```csharp
using OaktreeLab.Utils.Debug;

try
{
    // Your code that might throw an exception
    throw new Exception("Something went wrong!");
}
catch (Exception ex)
{
    string cleanStackTrace = ShyStackTrace.GenerateShyStackTrace(ex);
    Console.WriteLine(cleanStackTrace);
}
```

### Example Output Comparison

**Before (Potentially Sensitive):**
```
at ShyStackTrace_test.Program.Main(String[] args) in C:\Users\Developer\Documents\Visual Studio 2022\Projects\ShyStackTrace\Program.cs:line 12
at ShyStackTrace_test.Class1.Test() in C:\Users\Developer\Documents\Visual Studio 2022\Projects\ShyStackTrace\Class1.cs:line 15
at ShyStackTrace_test.SubComponent.Class2.Test() in C:\Users\Developer\Documents\Visual Studio 2022\Projects\ShyStackTrace\SubComponent\Class2.cs:line 18
```

**After (ShyStackTrace):**
```
  in Program.cs:line 12
  in Class1.cs:line 15
  in SubComponent\Class2.cs:line 18
```

## How It Works

1. **Decombining Stack Trace**: Parses the stack trace to extract paths, method names, and line numbers.
2. **Determine minimal common path**: Identifies the minimal common path to retain relevant context without exposing sensitive information.
3. **Function Signature Sanitization**: Strips detailed method signatures while preserving essential debugging information.
4. **Safe Reconstruction**: Rebuilds stack traces with only essential, non-sensitive information.

## Production Safety

ShyStackTrace is specifically designed for production environments where:
- Stack traces may be logged to external systems.
- Error reports are sent to third-party services.
- Logs may be accessed by support teams or customers.
- Compliance requires protection of developer and system information.

## API Reference

### `ShyStackTrace.GenerateShyStackTrace(Exception ex)`

Generates a privacy-safe stack trace string from an exception.

**Parameters:**
- `ex` (Exception): The exception to process.

**Returns:**
- `string`: A sanitized stack trace with sensitive information removed.

**Security Behavior:**
- Returns empty string if input is null (fail-safe).
- Removes all absolute path information and sensitive details like class names and method names.
- Preserves line numbers for debugging while protecting file locations.

## Requirements

- .NET 8.0 or later
- Compatible with Windows, Linux, and macOS file systems
- No external dependencies

## License

This project is licensed under the BSD 2-Clause License. See the source file header for full license terms.

---

**Author**: yamamaya - Oaktree-Lab  
**Version**: 1.0.0  
**Target Framework**: .NET 8.0
