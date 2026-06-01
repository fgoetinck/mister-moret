import { useState, useEffect } from 'react'

type MarkdownState =
  | { status: 'idle' }
  | { status: 'loading' }
  | { status: 'ok'; markdown: string }
  | { status: 'error' }

const cache = new Map<string, string>()

export function useMarkdown(url: string, key: string): MarkdownState {
  const [state, setState] = useState<MarkdownState>(
    cache.has(key) ? { status: 'ok', markdown: cache.get(key)! } : { status: 'idle' }
  )

  useEffect(() => {
    if (cache.has(key)) {
      setState({ status: 'ok', markdown: cache.get(key)! })
      return
    }
    setState({ status: 'loading' })
    fetch(url)
      .then(r => r.text())
      .then(md => { cache.set(key, md); setState({ status: 'ok', markdown: md }) })
      .catch(() => setState({ status: 'error' }))
  }, [url, key])

  return state
}
