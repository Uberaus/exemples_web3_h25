type Props = {
  changerPage: (page: string) => void;
};

export function MenuEntete({ changerPage }: Props) {
  return (
    <nav>
      <button onClick={() => changerPage("fetch/get")}>GET</button>
      <button onClick={() => changerPage("fetch/post")}>POST</button>
    </nav>
  );
}
