import styles from './AboutPanel.module.css'

export default function AboutPanel() {
  return (
    <main className={styles.main}>
      <div>
        <div className={styles.blockLabel}>// who made these</div>
        <div className={styles.panel}>
          <div className={styles.panelTitle}>Frédéric Goetinck-Moret</div>
          <p className={styles.text}>
            I am a .NET developer focused on building clean, reliable solutions for backend, web,
            and mobile applications. With a 20-year background running my own retail business, my approach
            to programming is strictly solution-oriented, treating every project as a puzzle to be solved
            with clean architecture and practical code.
          </p>
          <p className={styles.text}>
            Currently finishing my studies at Howest, I build and maintain these open-source NuGet packages
            to streamline workflows and share reusable components from my personal development projects.
          </p>
        </div>
      </div>
      <div>
        <div className={styles.blockLabel}>// why I built these packages</div>
        <div className={styles.panelRow}>
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
              But as I was building them I thought maybe others might find them useful too, so I made them public
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
      </div>
    </main>
  )
}
