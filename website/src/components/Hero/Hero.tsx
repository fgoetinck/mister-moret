import type { ReactNode } from 'react'
import styles from './Hero.module.css'

interface Props {
  children?: ReactNode
}

export default function Hero({ children }: Props) {
  return (
    <div className={styles.hero}>
      <div className={styles.tag}>// open source · nuget · .net</div>
      <div className={styles.title}>
        MisterMoret.<span className={styles.accent}>Packages</span>
      </div>
      <p className={styles.sub}>
        Small, focused NuGet packages built to remove boilerplate from everyday .NET development — by Frederic Goetinck-Moret.
      </p>
      {children}
    </div>
  )
}
