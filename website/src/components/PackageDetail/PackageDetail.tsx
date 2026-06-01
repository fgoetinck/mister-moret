import { useState } from 'react'
import type { PackageEntry } from '../../types/PackageEntry.ts'
import { useMarkdown } from '../../hooks/useMarkdown.ts'
import ReadmeRenderer from './ReadmeRenderer.tsx'
import styles from './PackageDetail.module.css'

interface Props {
  pkg: PackageEntry
}

function GitHubIcon() {
  return (
    <svg width="13" height="13" viewBox="0 0 16 16" fill="currentColor" aria-hidden="true">
      <path d="M8 0C3.58 0 0 3.58 0 8c0 3.54 2.29 6.53 5.47 7.59.4.07.55-.17.55-.38 0-.19-.01-.82-.01-1.49-2.01.37-2.53-.49-2.69-.94-.09-.23-.48-.94-.82-1.13-.28-.15-.68-.52-.01-.53.63-.01 1.08.58 1.23.82.72 1.21 1.87.87 2.33.66.07-.52.28-.87.51-1.07-1.78-.2-3.64-.89-3.64-3.95 0-.87.31-1.59.82-2.15-.08-.2-.36-1.02.08-2.12 0 0 .67-.21 2.2.82.64-.18 1.32-.27 2-.27.68 0 1.36.09 2 .27 1.53-1.04 2.2-.82 2.2-.82.44 1.1.16 1.92.08 2.12.51.56.82 1.27.82 2.15 0 3.07-1.87 3.75-3.65 3.95.29.25.54.73.54 1.48 0 1.07-.01 1.93-.01 2.2 0 .21.15.46.55.38A8.013 8.013 0 0016 8c0-4.42-3.58-8-8-8z" />
    </svg>
  )
}

function splitName(name: string): [string, string] {
  const lastDot = name.lastIndexOf('.')
  if (lastDot === -1) return ['', name]
  return [name.slice(0, lastDot + 1), name.slice(lastDot + 1)]
}

type DocTab = 'readme' | 'changelog'

function DocSkeleton({ styles }: { styles: Record<string, string> }) {
  return (
    <div className={styles.skeleton}>
      <div className={styles.skeletonH} style={{ width: '45%' }} />
      <div className={styles.skeletonLine} style={{ width: '100%' }} />
      <div className={styles.skeletonLine} style={{ width: '82%' }} />
      <div className={styles.skeletonLine} style={{ width: '68%' }} />
      <div className={styles.skeletonCode}>
        <div className={styles.codeLine} style={{ width: '65%' }} />
        <div className={`${styles.codeLine} ${styles.dim}`} style={{ width: '80%' }} />
        <div className={styles.codeLine} style={{ width: '55%' }} />
      </div>
      <div className={styles.skeletonLine} style={{ width: '90%' }} />
      <div className={styles.skeletonLine} style={{ width: '61%' }} />
    </div>
  )
}

export default function PackageDetail({ pkg }: Props) {
  const [docTab, setDocTab] = useState<DocTab>('readme')

  const readme    = useMarkdown(pkg.readmeUrl,    `${pkg.id}:readme`)
  const changelog = useMarkdown(pkg.changelogUrl, `${pkg.id}:changelog`)

  const [prefix, suffix] = splitName(pkg.name)
  const statusColor = pkg.badge === 'stable' ? 'var(--g)' : '#fad02c'

  const activeDoc = docTab === 'readme' ? readme : changelog

  return (
    <main className={styles.main}>
      <div className={styles.header}>
        <div>
          <div className={styles.title}>
            {prefix}<span className={styles.accent}>{suffix}</span>
          </div>
          <div className={styles.meta}>
            {pkg.version} · {pkg.license} · {pkg.dotnetTarget} ·{' '}
            <span style={{ color: statusColor }}>●</span> {pkg.badge}
          </div>
        </div>
        <a
          href={pkg.githubUrl}
          target="_blank"
          rel="noopener noreferrer"
          className={styles.ghBtn}
        >
          <GitHubIcon />
          View on GitHub
        </a>
      </div>

      <div className={styles.install}>
        <span className={styles.installPrompt}>$</span>
        {' '}dotnet add package {pkg.name}
      </div>

      <div>
        <div className={styles.docTabs}>
          <button
            className={`${styles.docTab} ${docTab === 'readme' ? styles.docTabActive : ''}`}
            onClick={() => setDocTab('readme')}
          >
            // readme
          </button>
          <button
            className={`${styles.docTab} ${docTab === 'changelog' ? styles.docTabActive : ''}`}
            onClick={() => setDocTab('changelog')}
          >
            // changelog
          </button>
        </div>
        <div className={styles.readmeContainer}>
          {(activeDoc.status === 'idle' || activeDoc.status === 'loading') && (
            <DocSkeleton styles={styles} />
          )}
          {activeDoc.status === 'error' && (
            <p className={styles.errorState}>Failed to load {docTab}.</p>
          )}
          {activeDoc.status === 'ok' && (
            <ReadmeRenderer markdown={activeDoc.markdown} />
          )}
        </div>
      </div>

      <div className={styles.chips}>
        {pkg.chips.map(chip => (
          <span key={chip} className={styles.chip}>{chip}</span>
        ))}
      </div>
    </main>
  )
}
