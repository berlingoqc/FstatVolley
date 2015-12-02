namespace FormGame

open System.Drawing
open System.Windows.Forms

type Bloque =
| Kill
| Touch
| Erreur 

type Erreur =
| Double
| Filet
| Traverser
| Over

type Frappe =
| Kill
| Bloc of Bloque
| Erreur of Erreur

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
    mutable Frappe : (Frappe * (Point list) * float) list
    mutable Dig : int list
    mutable Service : Service list
    mutable Reception : Reception list
}
type Joueur = {
    Nom : string
    Numero : string
    mutable Stat : PlayerStat 
    mutable Position : string option
}

type Game = {
    Local : string
    Visiteur : string
    Joueurs : Joueur list
}

type TerrainVolley() as terrain =
    inherit Panel()

    let pen = new Pen(brush=Brushes.Black, width = 5.f)

    member this.DrawTerrain() =
        let lenX = 536.0f
        let points = ([|new PointF(0.0f,0.0f);new PointF(lenX,0.0f);
                        new PointF(lenX,0.0f);new PointF(lenX,lenX/2.f);
                        new PointF(0.0f,0.0f);new PointF(0.0f,lenX/2.f);
                        new PointF(0.0f,lenX/2.0f);new PointF(lenX,lenX/2.f);
                        new PointF(lenX/2.f,0.0f);new PointF(lenX/2.0f,lenX/2.f);
                        new PointF(lenX/2.f-lenX/6.f,0.0f);new PointF(lenX/2.f-lenX/6.f,lenX/2.f);
                        new PointF(lenX/2.f+lenX/6.f,0.0f);new PointF(lenX/2.f+lenX/6.f,lenX/2.f)|])
        terrain.Paint.Add(
            fun e ->
                points
                |> List.ofArray
                |> this.Draw e.Graphics pen)

    member this.Draw (g:Graphics) (pen:Pen) (points:PointF list) =
        for i in 0..2..(points.Length-1) do
            g.DrawLine (pen, points.[i],points.[i+1])

    member this.Clear() =
        terrain.Paint.Add((fun e -> e.Graphics.Clear(this.BackColor)))

        this.DrawTerrain()
               
    member this.DrawPoint(p:Point) =
        let b = new SolidBrush(Color.Red)
        terrain.Paint.Add(
            fun e -> e.Graphics.FillEllipse(b,p.X,p.Y,5,5))
        terrain.Invalidate()
    
    member this.DrawLine(p:Point list) =
        terrain.Paint.Add(
            (fun x -> x.Graphics.DrawLine(pen,p.Head,p.Item(p.Length-1))))
        terrain.Invalidate()
    



type StatGame(g0:Game) =
    
    let game = g0

    //Fin de chaque set on entre tuple score des deux equipes (local*visiteur)
    let mutable set = List.empty<int*int>

    //Score des deux equipes
    let mutable sV = 0
    let mutable sL = 0

    let roasseur = 1

    //Vrai si service possesion service
    let mutable service = true
    //Proprieté pour le nom des deux equipes
    member this.Local = game.Local
    member this.Visiteur = game.Visiteur
   

    member this.Joueurs num =
        let j = game.Joueurs 
                |> List.find(fun x -> x.Nom=num)
        game.Joueurs |> List.findIndex(fun x -> x=j) 
    
    member this.Court() =
        game.Joueurs
        |> List.filter (fun x -> x.Position <> None)

    member this.Banc() =
        game.Joueurs 
        |> List.filter (fun x -> x.Position = None)   
            
    //variable privee
    member this.AjouterFrappe(num:string,frappe:Frappe,temps:float,ligne:List<Point>) =
        let i = this.Joueurs num
        game.Joueurs.[i].Stat.Frappe <- List.append game.Joueurs.[i].Stat.Frappe [(frappe,ligne,temps)]                                
        printfn "%A" game.Joueurs.[i]
