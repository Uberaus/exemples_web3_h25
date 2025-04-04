import { useState } from "react";
import { MenuEntete } from "~/components/MenuEntete"
import { Accueil } from "~/pages/Accueil";
import { PageGet } from "~/pages/PageGet";
import { PagePost } from "~/pages/PagePost";

export function App() {
  const [page, setPage] = useState("");

  function afficherPageCourante() {
    switch (page) {
      case "":
        return <Accueil />
      case "fetch/get":
        return <PageGet />
      case "fetch/post":
        return <PagePost />
      default:
        return <h1>404 Not Found</h1>
    }
  }

  return (
    <>
      <MenuEntete changerPage={setPage} />
      {afficherPageCourante()}
    </>
  )
}
