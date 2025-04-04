import { useState } from "react";
import { ComposantRecuperationDonnees } from "./components/ComposantRecuperationDonnees"
import { MenuEntete } from "./components/MenuEntete"
import { ComposantEnvoiDonnees } from "./components/ComposantEnvoiDonnees";


function App() {
  const [page, setPage] = useState("");

  return (
    <>
      <MenuEntete changerPage={setPage} />
      {page === "ComposantRecuperationDonnees" && <ComposantRecuperationDonnees />}
      {page === "ComposantEnvoiDonnees" && <ComposantEnvoiDonnees />}
    </>
  )
}

export default App
