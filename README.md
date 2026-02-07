# Mini ETL — Studio CLI Contract v1.0

Lightweight CSV processing engine for filtering, transformation and aggregation.

Mini ETL is part of the Automation CLI Toolkit and follows the unified Studio CLI Contract v1.0 architecture.

---

## Overview

Mini ETL is a command-line data processor designed for automation pipelines, scripting environments and structured data workflows.

It provides:

- Deterministic CSV processing
- Structured filter expressions
- Transformation rules
- Aggregation rules
- Unified exit codes
- Automation-safe behavior

This tool is built for stability, clarity and reproducibility.

---

## Features

- CSV input and output
- Filter expressions
- Transform rules
- Aggregation rules
- Async processing pipeline
- Deterministic Exit Codes
- Studio CLI Contract v1.0 compliant architecture

---

## Installation

Download the compiled executable from the Releases section.

Or build locally:

dotnet publish -c Release

Executable will be located in:

MiniEtl.Cli/bin/Release/net8.0/publish/

---

## Usage

Basic processing:

mini-etl --input input.csv --output output.csv

Filter rows:

mini-etl --input data.csv --output result.csv --filter "Price > 100"

Transform values:

mini-etl --input data.csv --output result.csv --transform "Name=ToUpper(Name)"

Aggregation:

mini-etl --input data.csv --output result.csv --aggregate "Sum(Amount)"

Show help:

mini-etl --help

Show version:

mini-etl --version

---

## CLI Options

| Option | Description |
|--------|------------|
| --input <file> | Input CSV file |
| --output <file> | Output CSV file |
| --filter "expr" | Filter expression |
| --transform "expr" | Transform rules |
| --aggregate "expr" | Aggregation rules |
| --version | Show version |
| --help | Show help |

---

## Exit Codes

Mini ETL follows deterministic exit code mapping.

| Code | Meaning |
|------|---------|
| 0 | Success |
| 1 | Invalid Arguments |
| 10 | File Not Found |
| 20 | Processing Error |
| 99 | Unhandled Exception |

This guarantees safe integration in automation pipelines.

---

## Architecture

Mini ETL follows Studio CLI Contract v1.0:

- Async `Main` orchestrator
- Descriptor-based CLI metadata
- `ProcessorFactory`
- `IDataProcessor`
- `ProcessingOptions`
- `ExitCodeMapper`
- Deterministic error mapping

This structure allows consistent cloning for additional CLI tools inside Automation CLI Toolkit.

---

## Versioning

Contract: Studio CLI Contract v1.0  
Tool version: see `--version`

---

## Status

Production-ready CLI module.  
Frozen as Studio CLI Template reference implementation.

---
