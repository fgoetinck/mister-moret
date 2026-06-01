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
            zero external dependencies.
          </p>
        </div>
        <div className={styles.panel}>
          <p className={styles.text}>
            As I was making these packages, first I was only going to use them as GitHub packages for my own projects.
            But as I was building them I thought maybe others might find them useful to, so I made them public
            as open-source and published them to NuGet.
          </p>
        </div>
        <div className={styles.panel}>
          <p className={styles.text}>
            So, feel free to use them in your own projects! Let me know if you find them useful or if you have any
            suggestions for improvements.
          </p>
        </div>
      </div>
    </main>
  )
}
