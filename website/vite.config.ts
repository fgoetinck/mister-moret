import { readFileSync } from 'node:fs'
import { resolve } from 'node:path'
import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

function readCsprojVersion(csprojPath: string): string {
  const xml = readFileSync(csprojPath, 'utf-8')
  return xml.match(/<Version>([\d.\w-]+)<\/Version>/)?.[1] ?? 'unknown'
}

const srcDir = resolve(import.meta.dirname, '../src')

// https://vite.dev/config/
export default defineConfig({
  base: '/',
  plugins: [react()],
  define: {
    __VERSIONS__: JSON.stringify({
      results: readCsprojVersion(resolve(srcDir, 'MisterMoret.Results/MisterMoret.Results.csproj')),
      http:    readCsprojVersion(resolve(srcDir, 'MisterMoret.Http/MisterMoret.Http.csproj')),
      try:     readCsprojVersion(resolve(srcDir, 'MisterMoret.Try/MisterMoret.Try.csproj')),
    }),
  },
})
