namespace FormGame

open System.Drawing

type Joueur = {
    Nom : string
    Numero : string
    Position : string option

}

type Game = {
    Local : string
    Visiteur : string
    Joueurs : Joueur list
}

type Bloque =
| Kill
| Touch
| Erreur

type Frappe =
| Kill
| Bloc of Bloque
| Erreur

type Reception =
| Ace
| One
| Two
| Three
| Four

type PlayerStat = {
    Bloc : Bloque list
    Frappe : Frappe list
    Dig : int
}


type StatGame(g0:Game) =
    
    //variable privee
    member this.Frappe(num:int,frappe:Frappe,ligne:List<Point> option) =
        printfn ""