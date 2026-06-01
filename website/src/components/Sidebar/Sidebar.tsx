import type { PackageId } from '../../types/PackageEntry.ts'
import { packages } from '../../data/packagesData.ts'
import PackageItem from './PackageItem.tsx'
import styles from './Sidebar.module.css'

interface Props {
  activePkg: PackageId
  onSelect: (id: PackageId) => void
}

export default function Sidebar({ activePkg, onSelect }: Props) {
  return (
    <aside className={styles.sidebar}>
      <div className={styles.section}>
        <div className={styles.label}>packages</div>
        {packages.map(pkg => (
          <PackageItem
            key={pkg.id}
            pkg={pkg}
            active={pkg.id === activePkg}
            onSelect={onSelect}
          />
        ))}
      </div>
    </aside>
  )
}
