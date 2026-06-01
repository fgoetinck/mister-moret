import styles from './TopBar.module.css'

export default function TopBar() {
  return (
    <div className={styles.topbar}>
      <span className={styles.brand}>
        <span className={styles.brandMark}>&gt;MM</span>
        <span className={styles.brandCursor}>_</span>
      </span>
    </div>
  )
}
