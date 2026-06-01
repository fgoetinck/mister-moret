import { useState, useEffect } from 'react'
import type { PackageId } from '../types/PackageEntry.ts'

type ReadmeState =
  | { status: 'idle' }
  | { status: 'loading' }
  | { status: 'ok'; markdown: string }
  | { status: 'error' }

const cache = new Map<PackageId, string>()

export function useReadme(url: string, id: PackageId): ReadmeState {
  const [state, setState] = useState<ReadmeState>(
    cache.has(id) ? { status: 'ok', markdown: cache.get(id)! } : { status: 'idle' }
  )

  useEffect(() => {
    if (cache.has(id)) {
      setState({ status: 'ok', markdown: cache.get(id)! })
      return
    }
    setState({ status: 'loading' })
    fetch(url)
      .then(r => r.text())
      .then(md => { cache.set(id, md); setState({ status: 'ok', markdown: md }) })
      .catch(() => setState({ status: 'error' }))
  }, [url, id])

  return state
}
