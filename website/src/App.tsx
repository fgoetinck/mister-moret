import { useState } from 'react'
import type { PackageId, ActiveTab } from './types/PackageEntry.ts'
import { packages } from './data/packagesData.ts'
import Nav from './components/Nav/Nav.tsx'
import Hero from './components/Hero/Hero.tsx'
import MobilePackageTabs from './components/Hero/MobilePackageTabs.tsx'
import Sidebar from './components/Sidebar/Sidebar.tsx'
import PackageDetail from './components/PackageDetail/PackageDetail.tsx'
import AboutPanel from './components/AboutPanel/AboutPanel.tsx'
import Footer from './components/Footer/Footer.tsx'
import styles from './App.module.css'

export default function App() {
  const [activePkg, setActivePkg] = useState<PackageId>('results')
  const [activeTab, setActiveTab] = useState<ActiveTab>('packages')

  const selectedPkg = packages.find(p => p.id === activePkg)!

  return (
    <>
      <Nav activeTab={activeTab} onTabChange={setActiveTab} />
      <Hero>
        {activeTab === 'packages' && (
          <MobilePackageTabs activePkg={activePkg} onPkgChange={setActivePkg} />
        )}
      </Hero>
      <div className={styles.layout}>
        {activeTab === 'packages' && (
          <Sidebar activePkg={activePkg} onSelect={setActivePkg} />
        )}
        {activeTab === 'packages' ? (
          <PackageDetail pkg={selectedPkg} />
        ) : (
          <AboutPanel />
        )}
      </div>
      <Footer />
    </>
  )
}
