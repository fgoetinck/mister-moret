export type PackageId = 'results' | 'http' | 'try'
export type BadgeKind = 'stable' | 'beta'
export type ActiveTab = 'packages' | 'about'

export interface PackageEntry {
  id: PackageId
  name: string
  version: string
  badge: BadgeKind
  dotnetTarget: string
  license: string
  githubUrl: string
  nugetUrl: string
  readmeUrl: string
  chips: string[]
}
