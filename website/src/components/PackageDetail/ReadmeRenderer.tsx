import Markdown from 'react-markdown'

interface Props {
  markdown: string
}

export default function ReadmeRenderer({ markdown }: Props) {
  return <Markdown>{markdown}</Markdown>
}
