namespace FormGame

open System.Drawing

type Bloque =
| Kill
| Touch
| Erreur 

type Frappe =
| Kill
| Bloc of Bloque
| Erreur

type Service =
| Ace
| One
| Two
| Three
| Four

type Reception =
| Ace
| One
| Two
| Three
| Four

type PlayerStat = {
    mutable Bloc : Bloque list
    mutable Frappe : Frappe * (Point list) list
    mutable Dig : int list
    mutable Service : Service list
    mutable Reception : Reception list
}

type Joueur = {
    Nom : string
    Numero : string
    Stat : PlayerStat option
    Position : string option
}

type Game = {
    Local : string
    Visiteur : string
    Joueurs : Joueur list
}


type StatGame(g0:Game) =
    
    //variable privee
    member this.Frappe(num:int,frappe:Frappe,ligne:List<Point> option) =
        printfn ""