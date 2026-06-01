import styles from './AboutPanel.module.css'

function GitHubIcon() {
  return (
    <svg width="13" height="13" viewBox="0 0 16 16" fill="currentColor" aria-hidden="true">
      <path d="M8 0C3.58 0 0 3.58 0 8c0 3.54 2.29 6.53 5.47 7.59.4.07.55-.17.55-.38 0-.19-.01-.82-.01-1.49-2.01.37-2.53-.49-2.69-.94-.09-.23-.48-.94-.82-1.13-.28-.15-.68-.52-.01-.53.63-.01 1.08.58 1.23.82.72 1.21 1.87.87 2.33.66.07-.52.28-.87.51-1.07-1.78-.2-3.64-.89-3.64-3.95 0-.87.31-1.59.82-2.15-.08-.2-.36-1.02.08-2.12 0 0 .67-.21 2.2.82.64-.18 1.32-.27 2-.27.68 0 1.36.09 2 .27 1.53-1.04 2.2-.82 2.2-.82.44 1.1.16 1.92.08 2.12.51.56.82 1.27.82 2.15 0 3.07-1.87 3.75-3.65 3.95.29.25.54.73.54 1.48 0 1.07-.01 1.93-.01 2.2 0 .21.15.46.55.38A8.013 8.013 0 0016 8c0-4.42-3.58-8-8-8z" />
    </svg>
  )
}

function NuGetIcon() {
  return (
    <svg width="13" height="13" viewBox="0 0 24 24" fill="currentColor" aria-hidden="true">
      <circle cx="17.5" cy="17.5" r="4.5" />
      <path d="M10.5 3h-7A.5.5 0 003 3.5v7a.5.5 0 00.5.5h7a.5.5 0 00.5-.5v-7A.5.5 0 0010.5 3z" />
      <path d="M10.5 13h-7a.5.5 0 00-.5.5v7a.5.5 0 00.5.5h7a.5.5 0 00.5-.5v-7a.5.5 0 00-.5-.5z" opacity=".4" />
      <path d="M20.5 3h-7a.5.5 0 00-.5.5v7a.5.5 0 00.5.5h7a.5.5 0 00.5-.5v-7A.5.5 0 0020.5 3z" opacity=".4" />
    </svg>
  )
}

function PortfolioIcon() {
  return (
    <svg width="13" height="13" viewBox="0 0 16 16" fill="currentColor" aria-hidden="true">
      <path d="M8 0a8 8 0 100 16A8 8 0 008 0zm0 1.5a6.5 6.5 0 110 13 6.5 6.5 0 010-13zm0 1a5.5 5.5 0 100 11A5.5 5.5 0 008 2.5zm1.5 2a1.5 1.5 0 110 3 1.5 1.5 0 010-3zm-4 1a1 1 0 110 2 1 1 0 010-2zm4 4a3.5 3.5 0 013.5 3.5H3A3.5 3.5 0 016.5 9.5h3z" />
    </svg>
  )
}

function MailIcon() {
  return (
    <svg width="13" height="13" viewBox="0 0 16 16" fill="currentColor" aria-hidden="true">
      <path d="M1.5 2A1.5 1.5 0 000 3.5v9A1.5 1.5 0 001.5 14h13a1.5 1.5 0 001.5-1.5v-9A1.5 1.5 0 0014.5 2h-13zm0 1h13c.17 0 .33.04.47.1L8 8.98 1.03 3.1A.5.5 0 011.5 3zm-1 1.48l6.5 5.4a.75.75 0 00.95 0l6.5-5.4c.03.1.05.2.05.32v8a.5.5 0 01-.5.5h-13a.5.5 0 01-.5-.5v-8c0-.12.02-.23.05-.32z" />
    </svg>
  )
}

export default function AboutPanel() {
  return (
    <main className={styles.main}>
      <div>
        <div className={styles.blockLabel}>// who made these</div>
        <div className={styles.panel}>
          <div className={styles.panelTitle}>Frédéric Goetinck-Moret</div>
          <p className={`${styles.text} ${styles.textFirst}`}>
            I am a .NET developer focused on building clean, reliable solutions for backend, web,
            and mobile applications. With a 20-year background running my own retail business, my approach
            to programming is strictly solution-oriented, treating every project as a puzzle to be solved
            with clean architecture and practical code.
          </p>
          <p className={`${styles.text} ${styles.textLast}`}>
            Currently finishing my studies at Howest, I build and maintain these open-source NuGet packages
            to streamline workflows and share reusable components from my personal development projects.
          </p>
        </div>
      </div>
      <div>
        <div className={styles.blockLabel}>// why I built these packages</div>
        <div className={styles.panelRow}>
          <div className={styles.panel}>
            <p className={`${styles.text} ${styles.textFirst} ${styles.textLast}`}>
              Every project I start ends up needing the same patterns: a result type, a typed
              HTTP client, and a clean way to wrap exceptions. Instead of copy-pasting them
              across repos I packaged them properly — with full target framework coverage and
              zero external dependencies.
            </p>
          </div>
          <div className={styles.panel}>
            <p className={`${styles.text} ${styles.textFirst} ${styles.textLast}`}>
              As I was making these packages, first I was only going to use them as GitHub packages for my own projects.
              But as I was building them I thought maybe others might find them useful too, so I made them public
              as open-source and published them to NuGet.
            </p>
          </div>
          <div className={styles.panel}>
            <p className={`${styles.text} ${styles.textFirst} ${styles.textLast}`}>
              So, feel free to use them in your own projects! Let me know if you find them useful or if you have any
              suggestions for improvements.
            </p>
          </div>
        </div>
      </div>
      <div>
        <div className={styles.blockLabel}>// links</div>
        <div className={styles.linkRow}>
          <a href="https://github.com/fgoetinck/mister-moret" target="_blank" rel="noopener noreferrer" className={styles.linkBtn}>
            <GitHubIcon />
            github.com/fgoetinck/mister-moret
          </a>
          <a href="https://www.nuget.org/profiles/fgoetinck" target="_blank" rel="noopener noreferrer" className={styles.linkBtn}>
            <NuGetIcon />
            nuget.org/profiles/fgoetinck
          </a>
          <a href="https://fgoetinck.dev" target="_blank" rel="noopener noreferrer" className={styles.linkBtn}>
            <PortfolioIcon />
            fgoetinck.dev
          </a>
          <a href="mailto:hello@fgoetinck.dev" className={styles.linkBtn}>
            <MailIcon />
            hello@fgoetinck.dev
          </a>
        </div>
      </div>
    </main>
  )
}
