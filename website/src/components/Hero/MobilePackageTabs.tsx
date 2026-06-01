import type { PackageId } from '../../types/PackageEntry.ts'
import { packages } from '../../data/packagesData.ts'
import styles from './Hero.module.css'

interface Props {
  activePkg: PackageId
  onPkgChange: (id: PackageId) => void
}

export default function MobilePackageTabs({ activePkg, onPkgChange }: Props) {
  return (
    <div className={styles.mobileTabs}>
      {packages.map(pkg => (
        <button
          key={pkg.id}
          className={`${styles.mobileTab} ${activePkg === pkg.id ? styles.mobileTabActive : ''}`}
          onClick={() => onPkgChange(pkg.id)}
        >
          <span className={styles.mobileTabName}>{pkg.name}</span>
          <span className={styles.mobileTabMeta}>{pkg.version}</span>
          <span className={`${styles.mobileTabBadge} ${pkg.badge === 'stable' ? styles.badgeStable : styles.badgeBeta}`}>
            {pkg.badge}
          </span>
        </button>
      ))}
    </div>
  )
}
