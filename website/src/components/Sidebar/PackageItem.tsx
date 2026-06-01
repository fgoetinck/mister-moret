import type { PackageEntry, PackageId } from '../../types/PackageEntry.ts'
import styles from './Sidebar.module.css'

interface Props {
  pkg: PackageEntry
  active: boolean
  onSelect: (id: PackageId) => void
}

export default function PackageItem({ pkg, active, onSelect }: Props) {
  return (
    <button
      className={`${styles.pkgItem} ${active ? styles.active : ''}`}
      onClick={() => onSelect(pkg.id)}
    >
      <div className={styles.pkgName}>{pkg.name}</div>
      <div className={styles.pkgVer}>{pkg.version}</div>
      <span className={`${styles.badge} ${pkg.badge === 'stable' ? styles.stable : styles.beta}`}>
        {pkg.badge}
      </span>
    </button>
  )
}
