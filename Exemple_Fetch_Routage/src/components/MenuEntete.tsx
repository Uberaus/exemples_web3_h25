type Props = {
  changerPage: (page: string) => void;
};

export function MenuEntete({ changerPage }: Props) {
  return (
    <nav>
      <button onClick={() => changerPage("ComposantRecuperationDonnees")}>
        GET
      </button>
      <button onClick={() => changerPage("ComposantEnvoiDonnees")}>POST</button>
    </nav>
  );
}
