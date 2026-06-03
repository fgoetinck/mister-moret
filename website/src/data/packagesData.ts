import type { BadgeKind, PackageEntry } from '../types/PackageEntry.ts'

function badge(version: string): BadgeKind {
  return version.includes('-') ? 'beta' : 'stable'
}

export const packages: readonly PackageEntry[] = [
  {
    id:           'results',
    name:         'MisterMoret.Results',
    version:      `v${__VERSIONS__.results}`,
    badge:        badge(__VERSIONS__.results),
    dotnetTarget: '.NET 8 – 10',
    license:      'MIT',
    githubUrl:    'https://github.com/fgoetinck/mister-moret/tree/main/src/MisterMoret.Results',
    nugetUrl:     'https://www.nuget.org/packages/MisterMoret.Results',
    readmeUrl:    'https://raw.githubusercontent.com/fgoetinck/mister-moret/main/src/MisterMoret.Results/README.md',
    changelogUrl: 'https://raw.githubusercontent.com/fgoetinck/mister-moret/main/src/MisterMoret.Results/CHANGELOG.md',
    chips:        ['C# 12', '.NET 8 – 10', 'Result<T>', 'HttpResult', 'railway-oriented', 'zero dependencies'],
  },
  {
    id:           'http',
    name:         'MisterMoret.Http',
    version:      `v${__VERSIONS__.http}`,
    badge:        badge(__VERSIONS__.http),
    dotnetTarget: '.NET 8 – 10',
    license:      'MIT',
    githubUrl:    'https://github.com/fgoetinck/mister-moret/tree/main/src/MisterMoret.Http',
    nugetUrl:     'https://www.nuget.org/packages/MisterMoret.Http',
    readmeUrl:    'https://raw.githubusercontent.com/fgoetinck/mister-moret/main/src/MisterMoret.Http/README.md',
    changelogUrl: 'https://raw.githubusercontent.com/fgoetinck/mister-moret/main/src/MisterMoret.Http/CHANGELOG.md',
    chips:        ['C# 12', '.NET 8 – 10', 'IHttpClientFactory', 'typed responses', 'cancellation support', 'DI ready'],
  },
  {
    id:           'try',
    name:         'MisterMoret.Try',
    version:      `v${__VERSIONS__.try}`,
    badge:        badge(__VERSIONS__.try),
    dotnetTarget: '.NET 8 – 10',
    license:      'MIT',
    githubUrl:    'https://github.com/fgoetinck/mister-moret/tree/main/src/MisterMoret.Try',
    nugetUrl:     'https://www.nuget.org/packages/MisterMoret.Try',
    readmeUrl:    'https://raw.githubusercontent.com/fgoetinck/mister-moret/main/src/MisterMoret.Try/README.md',
    changelogUrl: 'https://raw.githubusercontent.com/fgoetinck/mister-moret/main/src/MisterMoret.Try/CHANGELOG.md',
    chips:        ['C# 12', '.NET 8 – 10', 'exception wrapping', 'HTTP-aware', 'zero boilerplate'],
  },
]
