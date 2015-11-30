// En savoir plus sur F# sur le site http://fsharp.org
// Voir le projet 'Didacticiel F#' pour obtenir de l'aide.

open System
open System.Windows.Forms
open FormGame

[<EntryPoint>]
let main argv =
    let frmAcceuil = new FormGame.FormAccueil()
    do Application.Run(frmAcceuil)
    let frmGame = new FormGame.FormMain(frmAcceuil.Game)
    do Application.Run(frmGame)
    printfn "sa"
    0 // retourne du code de sortie entier
