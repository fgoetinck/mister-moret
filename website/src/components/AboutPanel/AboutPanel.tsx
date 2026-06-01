import styles from './AboutPanel.module.css'

export default function AboutPanel() {
  return (
    <main className={styles.main}>
      <div>
        <div className={styles.blockLabel}>// who made these</div>
        <div className={styles.panel}>
          <div className={styles.panelTitle}>Frederic Goetinck-Moret</div>
          <p className={styles.text}>
            .NET developer based in Belgium. I build backend systems and APIs with C# and
            ASP.NET Core, and occasionally ship open-source tooling to solve the same
            problems I keep running into.
          </p>
        </div>
      </div>
      <div>
        <div className={styles.blockLabel}>// why I built these packages</div>
        <div className={styles.panel}>
          <p className={styles.text}>
            Every project I start ends up needing the same patterns: a result type, a typed
            HTTP client, and a clean way to wrap exceptions. Instead of copy-pasting them
            across repos I packaged them properly — with full target framework coverage,
            zero external dependencies, and docs that actually explain the why.
          </p>
        </div>
      </div>
      <div className={styles.stats}>
        <div className={styles.stat}>
          <div className={styles.statNum}>3</div>
          <div className={styles.statLbl}>packages</div>
        </div>
        <div className={styles.stat}>
          <div className={styles.statNum}>MIT</div>
          <div className={styles.statLbl}>license</div>
        </div>
        <div className={styles.stat}>
          <div className={styles.statNum}>.NET 8</div>
          <div className={styles.statLbl}>target</div>
        </div>
      </div>
    </main>
  )
}
