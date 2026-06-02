# Security Policy

## Supported Versions

| Version | Supported |
|---------|-----------|
| Latest  | Yes       |
| < 1.0   | No        |

## Reporting a Vulnerability

**Do not** report security vulnerabilities via public GitHub issues or pull requests.

Report vulnerabilities by emailing **security@fgoetinck.dev** or opening a [GitHub security advisory](https://github.com/fgoetinck/mister-moret/security/advisories/new).

Include:

- Clear description of the vulnerability
- Affected version(s)
- Steps to reproduce
- Relevant logs or proof-of-concept
- Potential impact assessment

## Response Timeline

- **Acknowledgment:** Within 7 business days
- **Investigation:** Within 14 business days
- **Resolution:** Coordinated fix and public disclosure

## Scope

This repository contains three NuGet libraries:

- **MisterMoret.Results** — a Result pattern library with no network or auth logic
- **MisterMoret.Http** — an HTTP client wrapper; vulnerabilities related to request forgery, header injection, or token handling are in scope
- **MisterMoret.Try** — an exception-to-result wrapper with no network or auth logic

Reports about `MisterMoret.Results` and `MisterMoret.Try` are unlikely to yield security findings given their scope, but all reports will be reviewed.

## Responsible Disclosure

- Avoid public disclosure until a patch has been released
- Allow reasonable time for investigation and coordination
- Act in good faith