import type { ActiveTab } from '../../types/PackageEntry.ts'
import styles from './Nav.module.css'

interface Props {
  activeTab: ActiveTab
  onTabChange: (tab: ActiveTab) => void
}

export default function Nav({ activeTab, onTabChange }: Props) {
  return (
    <nav className={styles.nav}>
      <button
        className={`${styles.item} ${activeTab === 'packages' ? styles.active : ''}`}
        onClick={() => onTabChange('packages')}
      >
        packages
      </button>
      <button
        className={`${styles.item} ${activeTab === 'about' ? styles.active : ''}`}
        onClick={() => onTabChange('about')}
      >
        about
      </button>
      <div className={styles.sep} />
      <a
        href="https://fgoetinck.dev"
        target="_blank"
        rel="noopener noreferrer"
        className={styles.portfolio}
      >
        ↗ fgoetinck.dev
      </a>
    </nav>
  )
}
