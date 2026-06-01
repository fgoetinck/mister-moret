import styles from './TopBar.module.css'

export default function TopBar() {
  return (
    <div className={styles.topbar}>
      <div className={`${styles.dot} ${styles.red}`} />
      <div className={`${styles.dot} ${styles.yellow}`} />
      <div className={`${styles.dot} ${styles.green}`} />
      <span className={styles.brand}>mistermoret.fgoetinck.dev</span>
    </div>
  )
}
