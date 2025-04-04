import { useState, useEffect } from "react";

type Donnees = {
  userId: number;
  id: number;
  title: string;
  body: string;
};

export function ComposantRecuperationDonnees() {
  const [donnees, setDonnees] = useState<Donnees[]>([]);
  const [erreur, setErreur] = useState("");
  const [chargement, setChargement] = useState(true);

  useEffect(() => {
    const controleur = new AbortController();

    async function recupererDonnees() {
      try {
        setChargement(true);
        const reponse = await fetch(
          "https://jsonplaceholder.typicode.com/posts",
          { signal: controleur.signal }
        );
        if (!reponse.ok) {
          throw new Error("Erreur lors de la récupération des données");
        }
        const resultat = await reponse.json();
        setDonnees(resultat);
      } catch (erreur) {
        if (!(erreur instanceof Error)) {
          setErreur("Une erreur est survenue");
          return;
        }

        if (erreur.name === "AbortError") {
          return;
        }

        setErreur(erreur.message);
      } finally {
        setChargement(false);
      }
    }

    recupererDonnees();

    return () => {
      // Annule la requête si le composant est démonté
      controleur.abort();
    };
  }, []);

  if (chargement) {
    return <p>Chargement...</p>;
  }
  if (erreur) {
    return <p>Erreur: {erreur}</p>;
  }

  return (
    <ul>
      {donnees.map((element) => (
        <li key={element.id}>{element.title}</li>
      ))}
    </ul>
  );
}
